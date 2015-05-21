using System;

using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
{
	public partial class ConfirmPINResetController  : UIViewController
	{
		private ConfirmPINResetViewModel _viewModel;

		public string PIN
		{
			set
			{
				_viewModel.PIN = value;
			}
		}

		public ConfirmPINResetController (IntPtr handle) : base (handle)
		{
			_viewModel = new ConfirmPINResetViewModel();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			_inputAccessoryView.RemoveFromSuperview ();
			_pinControl.InputAccessoryView = _inputAccessoryView;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

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

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			Dispose (false);
		}

		private void InitBindings ()
		{
			_pinControl.TextChanged+= (object sender, EventArgs e) => {
				if (!_pinControl.Text.Equals(_viewModel.ConfirmPIN))
				{
					_viewModel.ConfirmPIN = _pinControl.Text;
				}
			};

			_viewModel.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) => {
				if (e.PropertyName.Equals("ConfirmPIN"))
				{
					if (!_pinControl.Text.Equals(_viewModel.ConfirmPIN))
					{
						_pinControl.Text = _viewModel.ConfirmPIN;
					}
				}
			};

			_pinControl.SetCommand ("DidFinish", _viewModel.ComparePINCommand);

			_startOverButton.SetCommand ("TouchUpInside", _viewModel.StartOverCommand);

			_viewModel.Incorrect += HandleIncorrect;

			_viewModel.RequestCreatePINReset += _viewModel_RequestCreatePINReset;

			_viewModel.RequestSettingsPage+= _viewModel_RequestSettingsPage;
		}

		private void HandleIncorrect(object sender, EventArgs args)
		{
			_pinControl.Shake ();

		}

		private void _viewModel_RequestCreatePINReset (object sender, EventArgs e)
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("SettingsStoryboard", null);
			var controller = storyboard.InstantiateViewController ("CreatePINResetController") as CreatePINResetController;
			NavigationController.PushViewController (controller, true);
		}

		void _viewModel_RequestSettingsPage (object sender, EventArgs e)
		{
			NavigationController.PopToRootViewController (true);
		}
	}
}
