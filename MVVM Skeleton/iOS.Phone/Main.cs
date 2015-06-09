
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using Xamarin;
using System.Threading.Tasks;
using System.Threading;
using Shared.Common;
using Shared.VM;
using Shared.Bootstrapper;
using iOS;
using Shared.Api;

namespace iOS.Phone
{
	public class Application
    {
        private static ILogger _logger;

		private static ViewModelLocator _locator;

		private static ViewModelStore _store;

		public static ViewModelLocator VMLocator { get { return _locator; } }

		public static ViewModelStore VMStore { get { return _store; } }

		static Application()
		{
			Init ();
		}

		// This is the main entry point of the application.
		static void Main (string[] args)
        {
            _logger = new Logger();

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                _logger.Log(e.ExceptionObject as Exception, LogType.ERROR);
            };

		    try
		    {
                UIApplication.Main(args, null, "AppDelegate");
		    }
		    catch (Exception e)
		    {
                _logger.Log(e, LogType.ERROR);
		        throw;
		    }
		}

		private static void Init()
		{
			IocBootstrapper.RegisterTypes(IocContainer.GetContainer());
			AutoMapperBootstrapper.MapTypes ();

			_locator = new ViewModelLocator();
			_store = new ViewModelStore ();

			IocContainer.GetContainer ().RegisterInstance<ISocialService> (new iOSSocialService ());
			IocContainer.GetContainer ().RegisterInstance<ILogger> (new Logger ());
			IocContainer.GetContainer().RegisterInstance<ISecureDatabase>(new iOSSecureDatabase());
            IocContainer.GetContainer().RegisterType<IHttpClientHelper, HttpClientHelper>();
			IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService> (ConfigureNav());
			IocContainer.GetContainer().RegisterType<IExtendedDialogService, ExtendedDialogService>();
			IocContainer.GetContainer().RegisterInstance<IHudService> (new HudService());
			IocContainer.GetContainer().RegisterType<IBrowserService, iOSBrowserService> ();
			IocContainer.GetContainer().RegisterInstance<IPhoneService> (new PhoneService());
			IocContainer.GetContainer().RegisterInstance<IMapService> (new MapService());
            IocContainer.GetContainer().RegisterInstance<IGeolocator>(new Geolocator());
            IocContainer.GetContainer().RegisterInstance<IConnectivityService>(new ConnectivityService());
			IocContainer.GetContainer().RegisterInstance<IDispatcherService>( new DispatcherService(new NSObject()));
			IocContainer.GetContainer().RegisterInstance<IEmailService> (new EmailService());
		}

		private static ExtendedNavigationService ConfigureNav()
		{
			var nav = new ExtendedNavigationService ();

			return nav;
		}
	}
}
