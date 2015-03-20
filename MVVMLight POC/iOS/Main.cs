using MonoTouch.UIKit;
using Demo.Shared.ViewModel;
using Demo.iOS.Controllers;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using Demo.Shared;
using Demo.Shared.Models;

namespace Demo.iOS
{
    public class Application
    {
		private static ViewModelLocator _locator;

		public static ViewModelLocator Locator
		{
			get
			{
				return _locator;
			}
		}

		static Application()
		{
			Init ();
		}

        // This is the main entry point of the application.
        private static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

		private static void Init()
		{
			var nav = new NavigationService();

			nav.Configure(
				ViewModelLocator.SEASON_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					return storyboard.InstantiateViewController ("SeasonViewController") as SeasonViewController;
				}
			);

			nav.Configure(
				ViewModelLocator.TEAM_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var vc = storyboard.InstantiateViewController ("TeamViewController") as TeamViewController;
					vc.SeasonId = (int)x;
					return vc;
				}
			);

			SimpleIoc.Default.Register<INavigationService>(() => nav);
			SimpleIoc.Default.Register<IDialogService, DialogService>();

			_locator = new ViewModelLocator();
		}
    }
}