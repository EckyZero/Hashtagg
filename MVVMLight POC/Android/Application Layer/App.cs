using Demo.Shared;
using Microsoft.Practices.Unity;
using Xamarin;
using Demo.Shared.Helpers;
using GalaSoft.MvvmLight.Views;
using Demo.Shared.ViewModel;
using Demo.Android.Controllers;
using GalaSoft.MvvmLight.Ioc;

namespace Demo.Android
{
    public static class App
    {
		private static ViewModelLocator _locator;

		public static ViewModelLocator Locator
		{
			get
			{
				return _locator;
			}
		}

		static App()
		{
			Init ();

			RegisterIocTypes();
		}

        private static void RegisterIocTypes()
        {
            //TODO replace with password from secure location
            Shared.Helpers.App.IocContainer.RegisterType<ISecureDatabase, AndroidSecureDatabase>(
                new InjectionConstructor("password"));
        }

		private static void Init()
		{
			var nav = new NavigationService();

			nav.Configure(ViewModelLocator.SEASON_VIEW_KEY,typeof(SeasonActivity));

			nav.Configure(ViewModelLocator.TEAM_VIEW_KEY, typeof(TeamActivity));

			SimpleIoc.Default.Register<INavigationService>(() => nav);
			SimpleIoc.Default.Register<IDialogService, DialogService>();

			_locator = new ViewModelLocator();
		}
    }
}