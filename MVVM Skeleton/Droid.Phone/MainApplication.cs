using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Microsoft.Practices.Unity;
using Shared.Bootstrapper;
using Shared.Common;
using Shared.VM;
using Xamarin;

namespace Droid.Phone
{
    #if DEBUG
    [Application(Debuggable=true)]
    #else
    [Application(Debuggable = false)]
    #endif
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        private int _sessionDepth = 0;

        private static ViewModelLocator _locator;

        private static ViewModelStore _store;

        public static ViewModelLocator VMLocator
        {
            get { return _locator; }
        }

        public static ViewModelStore VMStore
        {
            get { return _store; }
        }

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                Xamarin.Insights.Report(args.Exception as Exception, ReportSeverity.Error);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Xamarin.Insights.Report(e.ExceptionObject as Exception,ReportSeverity.Error);
            };
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Init();

            RegisterActivityLifecycleCallbacks(this);
        }

        private void Init()
        {
            Insights.Initialize(Settings.XamarinInsightsApiKey, Application.Context);

            IocBootstrapper.RegisterTypes(IocContainer.GetContainer());
            AutoMapperBootstrapper.MapTypes();

            _locator = new ViewModelLocator();
            _store = new ViewModelStore();

            IocContainer.GetContainer().RegisterType<ISecureDatabase, AndroidSecureDatabase>();
            IocContainer.GetContainer().RegisterType<IHttpClientHelper, HttpClientHelper>();
            IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService>(ConfigureNav());
            IocContainer.GetContainer().RegisterInstance<IExtendedDialogService>(new ExtendedDialogService());
            IocContainer.GetContainer().RegisterInstance<IHudService>(new HudService());
            IocContainer.GetContainer().RegisterInstance<IBrowserService>(new BrowserService());
            IocContainer.GetContainer().RegisterInstance<IGeolocator>(new Geolocator());
            IocContainer.GetContainer().RegisterInstance<IDispatcherService>(new DispatcherService());
        }

        private static ExtendedNavigationService ConfigureNav()
        {
            var nav = new ExtendedNavigationService();

            //nav.Configure(ViewModelLocator.HOME_KEY, typeof (HomeActivity));

			return nav;
		}

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            activity.RequestedOrientation = ScreenOrientation.Portrait;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            if (_sessionDepth == 0)
            {
                // resuming from background
            }
            _sessionDepth++;
        }

        public void OnActivityStopped(Activity activity)
        {
            if (_sessionDepth > 0)
            {
                _sessionDepth--;
            }
            if (_sessionDepth == 0)
            {
                // going to background
            }
        }
    }
}