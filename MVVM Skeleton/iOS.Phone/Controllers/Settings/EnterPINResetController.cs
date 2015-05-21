using System;
using UIKit;
using Shared.VM;
using Shared.BL;
using Shared.Common;
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight.Helpers;
using Foundation;

namespace iOS.Phone
{
	public partial class EnterPINResetController : UIViewController
	{
		private EnterPINResetViewModel _viewModel;

		public EnterPINResetController (IntPtr handle) : base (handle)
		{
			_viewModel = new EnterPINResetViewModel ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitBindings ();

			_inputAccessoryView.RemoveFromSuperview ();
			_pinControl.InputAccessoryView = _inputAccessoryView;

			Title = " ";

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if(NavigationController != null)
			{
				NavigationController.SetNavigationBarHidden (true, true);
			}
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, KeyboardWillShow);
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
			NSNotificationCenter.DefaultCenter.RemoveObserver (this);
		}

		private void KeyboardWillShow(NSNotification notification)
		{
			var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);
			Device.KeyboardHeight = keyboardFrame.Height;

			_containerView.ResizeToFitKeyboard ();
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

			_viewModel.Incorrect += HandleIncorrect;

			_viewModel.RequestCreatePINReset += _viewModel_RequestCreatePINReset;

			_viewModel.RequestSettingsPage+= _viewModel_RequestSettingsPage;
		}

		private void _viewModel_RequestCreatePINReset (object sender, EventArgs e)
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("SettingsStoryboard", null);
			var controller = storyboard.InstantiateViewController ("CreatePINResetController") as CreatePINResetController;
			NavigationController.PushViewController (controller, true);
		}

		private void _viewModel_RequestSettingsPage (object sender, EventArgs e)
		{
			NavigationController.PopToRootViewController (true);
		}

		private void HandleIncorrect(object sender, EventArgs args){
			_pinControl.Shake ();
		}
	}
}
