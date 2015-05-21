using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.BL;
using Microsoft.Practices.Unity;
using Shared.BL.Interfaces;
using Shared.Common;
using Shared.Common.Utils;

namespace Droid.OS_Services
{
    public class AppUpgradeService: BaseService, IAppUpgradeService
    {
        public AppVersion ApplicationVersion { get{ return _applicationVersion;} }

        private AppVersion _applicationVersion = new AppVersion(Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionCode);

        private TimeSpan _upgradeRequirementsTimeout = TimeSpan.FromMinutes(Settings.UpgradeRequirementsTimeout);

        private DateTime _upgradeRequirementsLastChecked;

        private IAppUpgradeBL _appUpgradeBL;

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
                _logger.Track("Android user was recommended to upgrade, and chose to do so");

                var packageName = Application.Context.PackageName;

                _activity.RunOnUiThread(() =>
                {
                    try
                    {
                        _activity.StartActivity((Intent.ParseUri("market://details?id=" + packageName, 0)));
                    }
                    catch (ActivityNotFoundException e)
                    {
                        _activity.StartActivity(
                            (Intent.ParseUri("https://play.google.com/store/apps/details?id=" + packageName, 0)));
                    }
                });

            }
            else
            {
                _logger.Track("Android user was recommended to upgrade, and chose to not upgrade");
            }
        }

        public void RequiredUpgradeAction()
		{
			var packageName = Application.Context.PackageName;
            _logger.Track("Android user was required to upgrade");

			_activity.RunOnUiThread (() => {
				try {
					_activity.StartActivity ((Intent.ParseUri ("market://details?id=" + packageName, 0)));
				} catch (ActivityNotFoundException e) {
					_activity.StartActivity (
						(Intent.ParseUri ("https://play.google.com/store/apps/details?id=" + packageName, 0)));
			    }
			});

		}

        public async void OnAppStart()
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

			const string preferenceKey = "current_version";

			var previousVersion = prefs.GetString(preferenceKey, null);

            if (!string.IsNullOrEmpty(previousVersion) && new AppVersion(previousVersion) != ApplicationVersion)
            {
                AndroidSecureDatabase.DeleteDatabase();
            }

            ISharedPreferencesEditor editor = prefs.Edit();

			editor.PutString(preferenceKey, ApplicationVersion.ToString());

            editor.Apply();
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

        public async Task UpdateAppUpgradeState()
        {
            //We get the appUpgradeBL when we need it, but not before. This class constructor is called early before the majority of the app is alive
            if(_appUpgradeBL == null)
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

                _upgradeRequirementsLastChecked = DateTime.Now;

                //This is android implementation
                var minReqVersion = appUpgradeRequirements.AndroidMinimumRequiredVersion != null
                    ? new AppVersion(appUpgradeRequirements.AndroidMinimumRequiredVersion)
                    : null;

                var minRecVersion = appUpgradeRequirements.AndroidMinimumRecommendedVersion != null
                    ? new AppVersion(appUpgradeRequirements.AndroidMinimumRecommendedVersion)
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

    }
}