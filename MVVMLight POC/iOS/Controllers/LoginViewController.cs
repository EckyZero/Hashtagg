using System;
using Demo.iOS.Utils;
using Demo.Shared.Helpers;
using MonoTouch.UIKit;
using Xamarin;
using GalaSoft.MvvmLight.Helpers;
using Demo.Shared;

namespace Demo.iOS.Controllers
{
    partial class LoginViewController : UIViewController
    {
		private LoginViewModel _viewModel;

		public LoginViewController(IntPtr handle) : base(handle)
        {
			_viewModel = Application.Locator.Login;
        }

		public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			Title = _viewModel.Title;

			InitBindings();
        }

		private void InitBindings()
		{
			Binding<string,string> emailBinding = this.SetBinding (() => EmailField.Text).UpdateSourceTrigger ("ValueChanged");

			LoginButton.SetCommand("TouchUpInside", _viewModel.LoginCommand, emailBinding);
		}
    }
}