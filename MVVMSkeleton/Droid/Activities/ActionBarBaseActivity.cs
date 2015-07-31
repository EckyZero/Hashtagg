using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using Shared.Common;

namespace Droid
{
    public class ActionBarBaseActivity : ActionBarActivity, IBaseActivity
    {
        protected ILogger _logger;

        protected DispatcherService _dispatchService;

        protected ExtendedNavigationService _navigationService;

        protected ExtendedDialogService _dialogService;

        protected HudService _hudService;

        protected BrowserService _browserService;

        protected Geolocator _geoLocator;

        protected ConnectivityService _connectivityService;

		protected PhoneService _phoneService;

		protected MapService _mapService;

		protected EmailService _emailService;

		protected AndroidTwitterHelper _twitterHelper;

		protected AndroidFacebookHelper _facebookHelper;

        public virtual void GoBack()
        {
            this.OnBackPressed();
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

            _connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>() as ConnectivityService;

			_phoneService  = IocContainer.GetContainer().Resolve<IPhoneService>() as PhoneService;

			_mapService  = IocContainer.GetContainer().Resolve<IMapService>() as MapService;

			_emailService  = IocContainer.GetContainer().Resolve<IEmailService>() as EmailService;

			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> () as AndroidTwitterHelper;

			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> () as AndroidFacebookHelper;
        }

        private void RegServices()
        {
            _navigationService.RegisterActivity(this);
            _dialogService.RegisterActivity(this);
            _hudService.RegisterActivity(this);
            _browserService.RegisterActivity(this);
            _geoLocator.RegisterActivity(this);
			_connectivityService.RegisterActivity(this);
			_phoneService.RegisterActivity(this);
			_mapService.RegisterActivity(this);
			_twitterHelper.RegisterActivity (this);
			_facebookHelper.RegisterActivity (this);
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