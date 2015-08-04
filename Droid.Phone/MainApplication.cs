using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using Org.Apache.Http.Conn;
using Xamarin;
using Shared.Common;
using Shared.VM;
using Shared.Bootstrapper;
using Droid;
using Shared.Service;

namespace Droid.Phone
{
    #if DEBUG
    [Application(Debuggable=true)]
    #else
    [Application(Debuggable = false)]
    #endif
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
		#region Variables

		private ILogger _logger;
        private int _sessionDepth = 0;
		private ILifecycleService _lifecycleService = IocContainer.GetContainer ().Resolve<ILifecycleService> ();

        private static ViewModelLocator _locator;
        private static ViewModelStore _store;

		#endregion

		#region Properties

        public static ViewModelLocator VMLocator
        {
            get { return _locator; }
        }

        public static ViewModelStore VMStore
        {
            get { return _store; }
        }

		#endregion

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            _logger = new Logger();

            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                args.Handled = false;
                _logger.Log(args.Exception as Exception, LogType.ERROR);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                _logger.Log(e.ExceptionObject as Exception, LogType.ERROR);
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
            IocBootstrapper.RegisterTypes(IocContainer.GetContainer());
            AutoMapperBootstrapper.MapTypes();

            _locator = new ViewModelLocator();
            _store = new ViewModelStore();

			IocContainer.GetContainer().RegisterInstance<ILogger> (new Logger ());
			IocContainer.GetContainer().RegisterInstance<ISecureDatabase>(new AndroidSecureDatabase());
            IocContainer.GetContainer().RegisterType<IHttpClientHelper, HttpClientHelper>();
            IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService>(ConfigureNav());
            IocContainer.GetContainer().RegisterInstance<IExtendedDialogService>(new ExtendedDialogService());
            IocContainer.GetContainer().RegisterInstance<IHudService>(new HudService());
            IocContainer.GetContainer().RegisterInstance<IConnectivityService>(new ConnectivityService());
            IocContainer.GetContainer().RegisterInstance<IBrowserService>(new BrowserService());
            IocContainer.GetContainer().RegisterInstance<IGeolocator>(new Geolocator());
            IocContainer.GetContainer().RegisterInstance<IDispatcherService>(new DispatcherService());
			IocContainer.GetContainer().RegisterInstance<IPhoneService> (new PhoneService());
			IocContainer.GetContainer().RegisterInstance<IMapService> (new MapService());
			IocContainer.GetContainer().RegisterInstance<IEmailService> (new EmailService());
			IocContainer.GetContainer ().RegisterInstance<ITwitterHelper> (new AndroidTwitterHelper ());
			IocContainer.GetContainer ().RegisterInstance<IFacebookHelper> (new AndroidFacebookHelper ());

			_lifecycleService.OnStart ();
        }

        private static ExtendedNavigationService ConfigureNav()
        {
            var nav = new ExtendedNavigationService();

            nav.Configure(ViewModelLocator.HOME_KEY, typeof(HamburgerMenuActivity));

			return nav;
		}

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
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
				_lifecycleService.OnResume ();
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
				_lifecycleService.OnPause ();
            }
        }
    }
}
