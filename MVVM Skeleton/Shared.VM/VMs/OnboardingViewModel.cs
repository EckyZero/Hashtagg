using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

namespace Shared.VM
{
	public class OnboardingViewModel : SharedViewModelBase
	{
		#region Variables

		private IFacebookHelper _facebookHelper;
		private ITwitterHelper _twitterHelper;

		private bool _isFacebookSelected = false;
		private bool _isTwitterSelected = false;

		#endregion

		#region Properties

		public Action<HomeViewModel> RequestHomePage { get; set; }
		public Action<bool> CanExecute { get; set; }

		public RelayCommand FacebookCommand { get; private set; }
		public RelayCommand TwitterCommand { get; private set; }
		public RelayCommand GoCommand { get; private set; }

		public bool IsFacebookSelected { 
			get { return _isFacebookSelected; }
			set { Set (() => IsFacebookSelected, ref _isFacebookSelected, value); }
		}

		public bool IsTwitterSelected { 
			get { return _isTwitterSelected; }
			set { Set (() => IsTwitterSelected, ref _isTwitterSelected, value); }
		}

		#endregion

		#region Methods

		public OnboardingViewModel () : base ()
		{
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		protected override void InitCommands ()
		{
			FacebookCommand = new RelayCommand (FacebookCommandExecute);
			TwitterCommand = new RelayCommand (TwitterCommandExecute);
			GoCommand = new RelayCommand (GoCommandExecute);
		}

		private async void FacebookCommandExecute ()
		{
			if(await _facebookHelper.AccountExists()) {
				_facebookHelper.DeleteAccount ();
				IsFacebookSelected = false;
			} else {
				_facebookHelper.Authenticate (async () => {
					IsFacebookSelected = true;
					await RequestCanExecute ();
				});
			}
			await RequestCanExecute ();
		}

		private async void TwitterCommandExecute ()
		{
			if(await _twitterHelper.AccountExists()) {
				_twitterHelper.DeleteAccount ();
				IsTwitterSelected = false;
				await RequestCanExecute ();
			} else {
				_twitterHelper.Authenticate (async () => {
					IsTwitterSelected = true;
					await RequestCanExecute ();
				});
			}
		}

		private void GoCommandExecute ()
		{
			var viewModel = new HomeViewModel ();

			viewModel.RefreshCommand.Execute (null);

			if(RequestHomePage != null) {
				RequestHomePage (viewModel);
			}
		}

		private async Task RequestCanExecute ()
		{
			var enabled = await _facebookHelper.AccountExists ();
			enabled |= await _twitterHelper.AccountExists ();

			if(CanExecute != null){
				CanExecute (enabled);
			}
		}

		#endregion
	}
}

