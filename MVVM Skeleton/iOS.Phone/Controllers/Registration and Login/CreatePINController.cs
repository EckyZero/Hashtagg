using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace iOS.Phone
{
	partial class CreatePINController : UIViewController
	{
		private ICreatePINViewModel _viewModel;

		public CreatePINController (IntPtr handle) : base (handle) 
		{
			_viewModel = IocContainer.GetContainer ().Resolve<ICreatePINViewModel> ();
			Application.VMStore.CreatePINVM = _viewModel;
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
			this.SetBinding (
				() => _pinControl.Text,
				() => Application.VMStore.CreatePINVM.PIN,
				BindingMode.TwoWay
			).UpdateSourceTrigger ("TextChanged");

			_pinControl.SetCommand ("DidFinish", Application.VMStore.CreatePINVM.EnterPINCommand);

			_cancelButton.SetCommand ("TouchUpInside", Application.VMStore.CreatePINVM.CancelCommand);
		}
	}
}
