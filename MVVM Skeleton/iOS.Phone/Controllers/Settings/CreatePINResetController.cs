using System;
using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
{
	public partial class CreatePINResetController : UIViewController
	{
		private CreatePINResetViewModel _viewModel;

		public CreatePINResetController (IntPtr handle) : base (handle) 
		{
			_viewModel = new CreatePINResetViewModel ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_inputAccessoryView.RemoveFromSuperview ();
			_pinControl.InputAccessoryView = _inputAccessoryView;

			InitBindings ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			_pinControl.Text = String.Empty;

			NavigationController.SetNavigationBarHidden (true, true);
			_containerView.ResizeToFitKeyboard ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			_pinControl.BecomeFirstResponder ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			_pinControl.ResignFirstResponder ();
		}
			
		private void InitBindings ()
		{
			_pinControl.TextChanged+= (object sender, EventArgs e) => {
				if (!_pinControl.Text.Equals(_viewModel.PIN))
				{
					_viewModel.PIN = _pinControl.Text;
				}
			};

			_viewModel.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => {
				if (e.PropertyName.Equals("PIN"))
				{
					if (!_pinControl.Text.Equals(_viewModel.PIN))
					{
						_pinControl.Text = _viewModel.PIN;
					}
				}
			};

			_pinControl.SetCommand ("DidFinish", _viewModel.EnterPINCommand);

			_cancelButton.SetCommand ("TouchUpInside", _viewModel.CancelCommand);

			_viewModel.RequestConfirmPINReset += _viewModel_RequestConfirmPINReset;

			_viewModel.RequestSettingsPage+= _viewModel_RequestSettingsPage;
		}

		void _viewModel_RequestConfirmPINReset (object sender, string e)
		{			
			UIStoryboard storyboard = UIStoryboard.FromName ("SettingsStoryboard", null);
			var controller = storyboard.InstantiateViewController ("ConfirmPINResetController") as ConfirmPINResetController;
			controller.PIN = e;
			NavigationController.PushViewController (controller, true);
		}

		void _viewModel_RequestSettingsPage (object sender, EventArgs e)
		{
			NavigationController.PopToRootViewController (true);
		}
	}
}
