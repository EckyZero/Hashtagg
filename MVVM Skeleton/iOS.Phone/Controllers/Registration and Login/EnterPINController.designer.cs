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
	[Register ("EnterPINController")]
	partial class EnterPINController
	{
		[Outlet]
		UIKit.UIView _containerView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint _containerViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIButton _forgotPinButton { get; set; }

		[Outlet]
		UIKit.UIView _inputAccessoryView { get; set; }

		[Outlet]
		iOS.PSPinControl _pinControl { get; set; }

		[Outlet]
		UIKit.UIButton _tourButton { get; set; }

		[Outlet]
		UIKit.UIButton _usePasswordButton { get; set; }

		[Outlet]
		UIKit.UILabel EmailButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_forgotPinButton != null) {
				_forgotPinButton.Dispose ();
				_forgotPinButton = null;
			}

			if (_inputAccessoryView != null) {
				_inputAccessoryView.Dispose ();
				_inputAccessoryView = null;
			}

			if (_pinControl != null) {
				_pinControl.Dispose ();
				_pinControl = null;
			}

			if (_tourButton != null) {
				_tourButton.Dispose ();
				_tourButton = null;
			}

			if (_usePasswordButton != null) {
				_usePasswordButton.Dispose ();
				_usePasswordButton = null;
			}

			if (EmailButton != null) {
				EmailButton.Dispose ();
				EmailButton = null;
			}

			if (_containerView != null) {
				_containerView.Dispose ();
				_containerView = null;
			}

			if (_containerViewHeightConstraint != null) {
				_containerViewHeightConstraint.Dispose ();
				_containerViewHeightConstraint = null;
			}
		}
	}
}
