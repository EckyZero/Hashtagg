// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOS.Phone
{
	[Register ("EnterPINResetController")]
	partial class EnterPINResetController
	{
		[Outlet]
		UIKit.UIButton _cancelButton { get; set; }

		[Outlet]
		UIKit.UIView _containerView { get; set; }

		[Outlet]
		UIKit.UIView _inputAccessoryView { get; set; }

		[Outlet]
		iOS.PSPinControl _pinControl { get; set; }

		[Outlet]
		UIKit.UILabel EmailButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_cancelButton != null) {
				_cancelButton.Dispose ();
				_cancelButton = null;
			}

			if (_inputAccessoryView != null) {
				_inputAccessoryView.Dispose ();
				_inputAccessoryView = null;
			}

			if (_pinControl != null) {
				_pinControl.Dispose ();
				_pinControl = null;
			}

			if (EmailButton != null) {
				EmailButton.Dispose ();
				EmailButton = null;
			}

			if (_containerView != null) {
				_containerView.Dispose ();
				_containerView = null;
			}
		}
	}
}
