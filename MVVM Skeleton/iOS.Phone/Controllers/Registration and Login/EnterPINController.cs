using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using Shared.VM;
using Shared.BL;

namespace iOS.Phone
{
	partial class EnterPINController : UIViewController
	{
		private IEnterPINViewModel _viewModel;

		public EnterPINController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer ().Resolve<IEnterPINViewModel> ();
			Application.VMStore.EnterPINVM = _viewModel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitBindings ();

			Member member = IocContainer.GetContainer ().Resolve<IMemberBL> ().GetCurrentMember ();
			if (member != null && string.IsNullOrWhiteSpace (member.PinCode) && member.PinFailedAttempts >= 5) {

				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("LoginController") as LoginController;

				NavigationController.PushViewController (controller, false);
			}

			_inputAccessoryView.RemoveFromSuperview ();
			_pinControl.InputAccessoryView = _inputAccessoryView;

			EmailButton.Text = member.Username;

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
			this.SetBinding (
				() => _pinControl.Text,
				() => Application.VMStore.EnterPINVM.PIN,
				BindingMode.TwoWay
			).UpdateSourceTrigger ("TextChanged");

			_pinControl.SetCommand ("DidFinish", Application.VMStore.EnterPINVM.EnterPINCommand);

			_forgotPinButton.SetCommand ("TouchUpInside", Application.VMStore.EnterPINVM.ForgotPINCommand);

			_usePasswordButton.SetCommand ("TouchUpInside", Application.VMStore.EnterPINVM.UsePasswordCommand);

			_tourButton.SetCommand ("TouchUpInside", Application.VMStore.EnterPINVM.TakeTourCommand);

			Application.VMStore.EnterPINVM.Incorrect += HandleIncorrect;

            Application.VMStore.EnterPINVM.OnPINSuccess += EnterPINVM_OnPINSuccess;
		}

        private async void EnterPINVM_OnPINSuccess(object sender, EventArgs e)
        {
            await DismissViewControllerAsync(false);
        }

		private void HandleIncorrect(object sender, EventArgs args){
			_pinControl.Shake ();

		}
	}
}
