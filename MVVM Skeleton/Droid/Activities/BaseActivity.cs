
using Android.App;
using Android.Locations;
using Android.OS;
using Droid.Activities;
using Microsoft.Practices.Unity;
using Shared.Common;

namespace Droid
{

    public abstract class BaseActivity : Activity, IBaseActivity
    {
        protected DispatcherService _dispatchService;

        protected ExtendedNavigationService _navigationService;

        protected ExtendedDialogService _dialogService;

        protected HudService _hudService;

        protected BrowserService _browserService;

        protected Geolocator _geoLocator;

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
        }

        private void RegServices()
        {
            _navigationService.RegisterActivity(this);
            _dialogService.RegisterActivity(this);
            _hudService.RegisterActivity(this);
            _browserService.RegisterActivity(this);
            _geoLocator.RegisterActivity(this);
        }

        public void OnLocationChanged(Location location)
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

