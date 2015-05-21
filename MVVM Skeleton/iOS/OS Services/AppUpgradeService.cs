using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Shared.BL;
using Microsoft.Practices.Unity;
using Shared.Common;
using Shared.Common.Utils;
using Foundation;
using Shared.BL.Interfaces;
using Shared.Common.Logging;
using UIKit;

namespace iOS
{
	public class AppUpgradeService:BaseService, IAppUpgradeService
    {
        public AppVersion ApplicationVersion { get{ return _applicationVersion;} }
        private AppVersion _applicationVersion = new AppVersion(NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString());

        private TimeSpan _upgradeRequirementsTimeout = TimeSpan.FromMinutes(Settings.UpgradeRequirementsTimeout);

        private DateTime _upgradeRequirementsLastChecked;

        private IAppUpgradeBL _appUpgradeBL;

		private string _appUpgradeURL = string.Empty;

        private object lockObj = new object();

        public AppUpgradeState CurrentAppUpgradeState
        {
            get
            {
                if (UpgradeRequirementsNeedsUpdate)
                {
                    BackgroundUpdateAppUpgradeState(this, new EventArgs());
                }
				return _currentUpgradeState;
            }
        }

        private AppUpgradeState _currentUpgradeState;


        private bool UpgradeRequirementsNeedsUpdate
        {
            get { return _upgradeRequirementsLastChecked < DateTime.Now - _upgradeRequirementsTimeout; }
        }


        public AppUpgradeService()
        {
            _currentUpgradeState = AppUpgradeState.Good;

            _upgradeRequirementsLastChecked = new DateTime(1970, 1, 1, 0, 0, 0);
        }

		public void RecommendedUpgradeAction(bool userChoice)
        {
		    if (userChoice)
		    {
                _logger.Track("iOS user was recommended to upgrade, and chose to do so");
		        UIApplication.SharedApplication.OpenUrl(new NSUrl(_appUpgradeURL));
		    }
		    else
		    {
                _logger.Track("iOS user was recommended to upgrade, and chose not to do so");
		    }
		}

        public void RequiredUpgradeAction()
        {
            _logger.Track("iOS user was required to upgrade");
			UIApplication.SharedApplication.OpenUrl(new NSUrl(_appUpgradeURL));
        }

        public async Task UpdateAppUpgradeState()
        {
            //We get the appUpgradeBL when we need it, but not before. This class constructor is called early before the majority of the app is alive
            if (_appUpgradeBL == null)
                _appUpgradeBL = IocContainer.GetContainer().Resolve<IAppUpgradeBL>();

            var response = await _appUpgradeBL.GetAppUpgradeRequirements();

            if (response.ResponseType != BLResponseType.SUCCESS)
            {
                return;
            }

            var appUpgradeRequirements = response.Result;

            lock (lockObj)
            {
                _currentUpgradeState = AppUpgradeState.Good;

                if (appUpgradeRequirements == null)
                    return;

                _appUpgradeURL = appUpgradeRequirements.IOSUpgradeUrl ?? string.Empty;

                _upgradeRequirementsLastChecked = DateTime.Now;

                //This is android implementation
                var minReqVersion = appUpgradeRequirements.IOSMinimumRequiredVersion != null
                    ? new AppVersion(appUpgradeRequirements.IOSMinimumRequiredVersion)
                    : null;

                var minRecVersion = appUpgradeRequirements.IOSMinimumRecommendedVersion != null
                    ? new AppVersion(appUpgradeRequirements.IOSMinimumRecommendedVersion)
                    : null;

                if (minReqVersion == null)
                    return;

                if (minReqVersion > ApplicationVersion)
                {
                    _currentUpgradeState = AppUpgradeState.RequiredUpgrade;
                    return;
                }

                if (minRecVersion == null)
                    return;

                //Not currently used, unless recomended version handeling is enabled in view models 
                if (minRecVersion > ApplicationVersion)
                {
                    _currentUpgradeState = AppUpgradeState.RecommendedUpgrade;
                    return;
                }
                return;    
            }
            
        }


        public void OnAppStart()
        {
			const string preferenceKey = "current_version";

			var previousVersion = NSUserDefaults.StandardUserDefaults.StringForKey(preferenceKey);

			if(!string.IsNullOrEmpty(previousVersion) && new AppVersion(previousVersion) != ApplicationVersion)
			{
				iOSSecureDatabase.DeleteDatabase();
			}				

			NSUserDefaults.StandardUserDefaults.SetString(ApplicationVersion.ToString(), preferenceKey);
        }

        public void BackgroundUpdateAppUpgradeState(object sender, EventArgs args)
        {
            try
            {
                Task.Run(() => UpdateAppUpgradeState());
            }
            catch (Exception e)
            {
                _logger.Log(e);
            }
            
        }
    }
}