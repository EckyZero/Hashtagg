// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOS
{
	[Register ("PSFloatingTextControl")]
	partial class PSFloatingTextControl
	{
		[Outlet]
		UIKit.UIButton _clearButton { get; set; }

		[Outlet]
		UIKit.UIButton _detailButton { get; set; }

		[Outlet]
		UIKit.UIButton _hideButton { get; set; }

		[Outlet]
		UIKit.UILabel _label { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint _labelCenterYConstraint { get; set; }

		[Outlet]
		UIKit.UIButton _showButton { get; set; }

		[Outlet]
		UIKit.UITextField _textField { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint _textFieldCenterYConstraint { get; set; }

		[Outlet]
		UIKit.UIView _view { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_clearButton != null) {
				_clearButton.Dispose ();
				_clearButton = null;
			}

			if (_detailButton != null) {
				_detailButton.Dispose ();
				_detailButton = null;
			}

			if (_hideButton != null) {
				_hideButton.Dispose ();
				_hideButton = null;
			}

			if (_label != null) {
				_label.Dispose ();
				_label = null;
			}

			if (_showButton != null) {
				_showButton.Dispose ();
				_showButton = null;
			}

			if (_textField != null) {
				_textField.Dispose ();
				_textField = null;
			}

			if (_view != null) {
				_view.Dispose ();
				_view = null;
			}

			if (_textFieldCenterYConstraint != null) {
				_textFieldCenterYConstraint.Dispose ();
				_textFieldCenterYConstraint = null;
			}

			if (_labelCenterYConstraint != null) {
				_labelCenterYConstraint.Dispose ();
				_labelCenterYConstraint = null;
			}
		}
	}
}
