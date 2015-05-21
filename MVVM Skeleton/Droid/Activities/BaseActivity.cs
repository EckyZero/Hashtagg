﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Locations;
using Droid.Activities;
using Droid.OS_Services;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.BL;
using Shared.Common.Logging;

namespace Droid
{

    public abstract class BaseActivity : Activity, IBaseActivity
    {
        protected ILogger _logger;

        protected DispatcherService _dispatchService;

        protected ExtendedNavigationService _navigationService;

        protected ExtendedDialogService _dialogService;

        protected HudService _hudService;

        protected BrowserService _browserService;

        protected Geolocator _geoLocator;

		protected AppUpgradeService _appUpgradeService;

        protected ConnectivityService _connectivityService;

		protected PhoneService _phoneService;

		protected MapService _mapService;

		protected EmailService _emailService;

        public virtual void Dismiss()
        {
            GoBack();
        }

        public virtual void GoBack()
        {
            this.OnBackPressed();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnResume()
        {
            base.OnResume();

            RegServices();

            if (string.IsNullOrEmpty(ActivityKey))
            {
                ActivityKey = NextPageKey;
                NextPageKey = null;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            InitServices();
            RegServices();
        }

        public string ActivityKey { get; private set; }

        public string NextPageKey { get; set; }


        private void InitServices()
        {
            _dispatchService = IocContainer.GetContainer().Resolve<IDispatcherService>() as DispatcherService;

            _navigationService = IocContainer.GetContainer().Resolve<IExtendedNavigationService>() as ExtendedNavigationService;

            _dialogService = IocContainer.GetContainer().Resolve<IExtendedDialogService>() as ExtendedDialogService;

            _hudService = IocContainer.GetContainer().Resolve<IHudService>() as HudService;

            _browserService = IocContainer.GetContainer().Resolve<IBrowserService>() as BrowserService;

            _geoLocator = IocContainer.GetContainer().Resolve<IGeolocator>() as Geolocator;

            _appUpgradeService = IocContainer.GetContainer().Resolve<IAppUpgradeService>() as AppUpgradeService;

            _connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>() as ConnectivityService;

			_phoneService  = IocContainer.GetContainer().Resolve<IPhoneService>() as PhoneService;

			_mapService  = IocContainer.GetContainer().Resolve<IMapService>() as MapService;

			_emailService  = IocContainer.GetContainer().Resolve<IEmailService>() as EmailService;
			//TODO: why are we casting above?
        }

        private void RegServices()
        {
            _navigationService.RegisterActivity(this);
            _dialogService.RegisterActivity(this);
            _hudService.RegisterActivity(this);
            _browserService.RegisterActivity(this);
            _geoLocator.RegisterActivity(this);
            _appUpgradeService.RegisterActivity(this);
			_connectivityService.RegisterActivity(this);
			_phoneService.RegisterActivity(this);
			_mapService.RegisterActivity(this);
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            _geoLocator.LocationChanged(location);
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
    }
}

