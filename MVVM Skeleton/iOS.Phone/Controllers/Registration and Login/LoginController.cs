using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using System.Diagnostics;
using CoreGraphics;
using Shared.VM;
using Microsoft.Practices.Unity;
using Shared.BL;

namespace iOS.Phone
{
	partial class LoginController : UIViewController
	{
		public ILoginViewModel _viewModel;

		private bool _keyboardIsShown = false;

		public LoginController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer().Resolve<ILoginViewModel> ();
			Application.VMStore.LoginVM = _viewModel;
			Application.VMStore.LoginVM.CanExecute += OnCanExecute;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.SetNavigationBarHidden (false, true);
			SubscribeToNotifications ();
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			UnsubscribeFromNotifications ();
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			PasswordControl.ResignFirstResponder ();
			UsernameControl.ResignFirstResponder ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			bool isPinFlow = NavigationController.GetType () == typeof(EnterPINNavigationController);

			if (_viewModel.IsLockedOut && isPinFlow) {
				NavigationItem.HidesBackButton = true;
			}
			PopulateStaticData ();
			InitBindings ();

			Application.VMStore.LoginVM.PostInit ();

			UsernameControl.ShouldNotAnimateOnEditing = false;
			UsernameControl.KeyboardType = UIKeyboardType.EmailAddress;

			LoginButton.SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (LoginButton.Bounds), UIControlState.Disabled);
			LoginButton.SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (LoginButton.Bounds), UIControlState.Normal);
			LoginButton.Enabled = false;
		}

		private void SubscribeToNotifications()
		{
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillShowNotification, KeyboardWillShow);
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, KeyboardWillHide);
		}

		private void UnsubscribeFromNotifications()
		{
			NSNotificationCenter.DefaultCenter.RemoveObserver (this);
		}

		private void KeyboardWillShow(NSNotification notification)
		{
			ScrollView.ScrollEnabled = true;
			var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);
			var scrollPoint = new CGPoint (0, LoginButton.Frame.Y + LoginButton.Frame.Height - keyboardFrame.Height + 10);
			Device.KeyboardHeight = keyboardFrame.Height;

			ScrollView.SetContentOffset (scrollPoint, true);

			_keyboardIsShown = true;
		}

		private void KeyboardWillHide(NSNotification notification)
		{
			var scrollPoint = new CGPoint (0, 0);
			ScrollView.SetContentOffset (scrollPoint, true);
			ScrollView.ScrollEnabled = false;

			_keyboardIsShown = false;
		}


		private void InitBindings()
		{
			this.SetBinding (
				() => UsernameControl.Text,
				() => Application.VMStore.LoginVM.Username,
				BindingMode.TwoWay 
			).UpdateSourceTrigger ("TextChanged");
				
			UsernameControl.ShouldNotAnimateOnEditing = true;

			this.SetBinding (
				() => PasswordControl.Text,
				() => Application.VMStore.LoginVM.Password
			).UpdateSourceTrigger ("TextChanged");

			if(NavigationController.GetType() == typeof(EnterPINNavigationController))
			{
				LoginButton.SetCommand ("TouchUpInside", Application.VMStore.LoginVM.DimissLoginCommand);
			}
			else
			{
				LoginButton.SetCommand("TouchUpInside", Application.VMStore.LoginVM.LoginCommand);
			}
			ForgotUsernameButton.SetCommand("TouchUpInside", Application.VMStore.LoginVM.ForgotUsernameCommand);
			ForgotPasswordButton.SetCommand("TouchUpInside", Application.VMStore.LoginVM.ForgotPasswordCommand);
			PasswordControl.DetailDisclosureHidden = false;
			PasswordControl.SecureTextEntry = true;
			PasswordControl.SetCommand ("DetailTapped", Application.VMStore.LoginVM.PasswordTipCommand);
		}

		private void PopulateStaticData()
		{
			UsernameControl.PlaceholderText = Application.VMStore.LoginVM.UsernamePlaceholder;
			PasswordControl.PlaceholderText = Application.VMStore.LoginVM.EnterPasswordPlaceholder;

		}

        private void OnCanExecute(object sender, CanExecuteEventArgs args)
		{
            LoginButton.Enabled = args.CanExecute;
		}
			
	}
}
