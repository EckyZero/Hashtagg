using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using Demo.Shared.ViewModel;
using Demo.Shared.Helpers;
using Xamarin;

namespace Demo.Shared
{
	public class LoginViewModel : DemoViewModelBase
	{
		public RelayCommand<string> LoginCommand { get; private set; }

		public override string Title { get { return Strings.AppConstants.AppTitle; } }
			
		public LoginViewModel ():base(){}

		protected override void InitCommands ()
		{
			LoginCommand = new RelayCommand<string> (LoginCommandExecute);
		}

		private void LoginCommandExecute(string email)
		{
			if (!String.IsNullOrWhiteSpace (email)) {
				var nav = ServiceLocator.Current.GetInstance<INavigationService> ();
				nav.NavigateTo (ViewModelLocator.SEASON_VIEW_KEY, null);

				Insights.Identify(email, Insights.Traits.Email, email);
			} 
			else {
				var dialog = ServiceLocator.Current.GetInstance<IDialogService> ();
				dialog.ShowMessageBox(Strings.AppConstants.EnterEmail, "");
			}
		}
	}
}

