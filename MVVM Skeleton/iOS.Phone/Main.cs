
using Foundation;
using Microsoft.Practices.Unity;
using Shared.Bootstrapper;
using Shared.Common;
using Shared.VM;
using UIKit;
using Xamarin;

namespace iOS.Phone
{
	public class Application
	{
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
			Insights.Initialize(Settings.XamarinInsightsApiKey);
			UIApplication.Main (args, null, "AppDelegate");
		}

		private static void Init()
		{
			IocBootstrapper.RegisterTypes(IocContainer.GetContainer());
			AutoMapperBootstrapper.MapTypes ();

			_locator = new ViewModelLocator();
			_store = new ViewModelStore ();

			IocContainer.GetContainer().RegisterType<IHttpClientHelper, HttpClientHelper>();
			IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService> (ConfigureNav());
			IocContainer.GetContainer().RegisterType<IExtendedDialogService, ExtendedDialogService>();
			IocContainer.GetContainer().RegisterInstance<IHudService> (new HudService());
			IocContainer.GetContainer().RegisterType<IBrowserService, iOSBrowserService> ();
			IocContainer.GetContainer().RegisterInstance<IGeolocator> (new Geolocator ());
			IocContainer.GetContainer().RegisterInstance<IDispatcherService>( new DispatcherService(new NSObject()));
		}

		private static ExtendedNavigationService ConfigureNav()
		{
			var nav = new ExtendedNavigationService ();

            //nav.Configure (
            //    ViewModelLocator.HOME_KEY,
            //    x => {
            //        UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
            //        var controller = storyboard.InstantiateViewController ("HomeController") as HomeController;

            //        return controller;
            //    }
            //);

			return nav;
		}
	}
}
