using System;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using System.Threading.Tasks;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace iOS.Phone
{
	public partial class RegisterController : UIViewController
	{
		private IRegistrationViewModel _viewModel;
		private RegisterTableController _tableController;

		public RegisterController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer().Resolve<IRegistrationViewModel> ();
			Application.VMStore.RegistrationVM = _viewModel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			_tableController = ChildViewControllers [0] as RegisterTableController;
			_tableController.TableView.Scrolled += HandleScrolled;

			// TODO: Move colors and fonts to a centralized location
			var disableAttributes = new UITextAttributes () {
				Font = UIFont.FromName ("FuturaStd-Bold", 10),
				TextColor = SharedColors.DarkBlue.ToUIColor()
			};
			var enableAttributes = new UITextAttributes () {
				Font = UIFont.FromName ("FuturaStd-Bold", 10),
				TextColor = SharedColors.White.ToUIColor()
			};
			NextButton.SetTitleTextAttributes (disableAttributes, UIControlState.Disabled);
			NextButton.SetTitleTextAttributes (enableAttributes, UIControlState.Normal);

			NextButton.Enabled = false;
			Application.VMStore.RegistrationVM.CanExecute += OnCanExecute;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NavigationController.SetNavigationBarHidden (false, true);
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
		}

        private void OnCanExecute(object sender, CanExecuteEventArgs args)
		{
			var canExecuteArgs = args;
			NextButton.Enabled = canExecuteArgs.CanExecute;
		}

		void HandleScrolled (object sender, EventArgs args)
		{
			_tableController.TableView.AddParrallax (_headerView);
		}

		private void InitBindings()
		{
			NextButton.SetCommand("Clicked", Application.VMStore.RegistrationVM.RegisterCommand);
		}
	}
}
