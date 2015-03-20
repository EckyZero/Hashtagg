using Demo.iOS.Utils;
using Demo.Shared.Helpers;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin;
using Demo.Shared.ViewModel;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Demo.Shared;

namespace Demo.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        private UIWindow window;

		public LoginViewModel Vm
		{
			get
			{
				return Application.Locator.Login;
			}
		}

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            App.Initialize();

            UIStoryboard storyboard = UIStoryboard.FromName("MainStoryboard", null);
            var viewController =
                storyboard.InstantiateViewController("MainNavigationController") as UINavigationController;

			var nav = (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>();
			nav.Initialize(viewController);

            viewController.NavigationBar.BarTintColor = UIColor.Clear.FromHex(Colors.ThemePrimary);
            viewController.NavigationBar.TintColor = UIColor.Clear.FromHex(Colors.ThemeSecondary);
            viewController.NavigationBar.TitleTextAttributes = new UIStringAttributes
            {
                ForegroundColor = UIColor.Clear.FromHex(Colors.ThemeSecondary)
            };

            window = new UIWindow(UIScreen.MainScreen.Bounds);
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();

#if DEBUG
            Calabash.Start();
#endif

            return true;
        }
    }
}