using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace iOS.Phone
{
	partial class ConfirmPINController : UIViewController
	{
		private IConfirmPINViewModel _viewModel;

		public ConfirmPINController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer ().Resolve<IConfirmPINViewModel> ();

			// TODO: This should be passed by navigation! What if the GC has already consumed this VM?
			_viewModel.PIN = Application.VMStore.CreatePINVM.PIN;

			Application.VMStore.ConfirmPINVM = _viewModel;
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
			this.SetBinding (
				() => _pinControl.Text,
				() => Application.VMStore.ConfirmPINVM.ConfirmPIN,
				BindingMode.TwoWay
			).UpdateSourceTrigger ("TextChanged");

			if(NavigationController.GetType() == typeof(EnterPINNavigationController))
			{
				_pinControl.SetCommand ("DidFinish", Application.VMStore.ConfirmPINVM.DismissComparePINCommand);
			} else
			{
				_pinControl.SetCommand ("DidFinish", Application.VMStore.ConfirmPINVM.ComparePINCommand);
			}
				
			_startOverButton.SetCommand ("TouchUpInside", Application.VMStore.ConfirmPINVM.StartOverCommand);

			Application.VMStore.ConfirmPINVM.Incorrect += HandleIncorrect;
		}
		private void HandleIncorrect(object sender, EventArgs args)
		{
			_pinControl.Shake ();

		}
	}
}
