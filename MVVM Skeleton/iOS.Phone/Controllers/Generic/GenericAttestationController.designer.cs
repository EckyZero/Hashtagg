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
	[Register ("GenericAttestationController")]
	partial class GenericAttestationController
	{
		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UILabel ContentBodyLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentFooterLabel { get; set; }

		[Outlet]
		iOS.PSFloatingTextControl NameControl { get; set; }

		[Outlet]
		iOS.PSFloatingTextControl PhoneControl { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint PhoneControlHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.UIButton SubmitButton { get; set; }

		[Outlet]
		UIKit.UILabel ToolTipLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ToolTipLabelTopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (ContentBodyLabel != null) {
				ContentBodyLabel.Dispose ();
				ContentBodyLabel = null;
			}

			if (ContentFooterLabel != null) {
				ContentFooterLabel.Dispose ();
				ContentFooterLabel = null;
			}

			if (NameControl != null) {
				NameControl.Dispose ();
				NameControl = null;
			}

			if (PhoneControl != null) {
				PhoneControl.Dispose ();
				PhoneControl = null;
			}

			if (PhoneControlHeightConstraint != null) {
				PhoneControlHeightConstraint.Dispose ();
				PhoneControlHeightConstraint = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (SubmitButton != null) {
				SubmitButton.Dispose ();
				SubmitButton = null;
			}

			if (ToolTipLabel != null) {
				ToolTipLabel.Dispose ();
				ToolTipLabel = null;
			}

			if (ToolTipLabelTopConstraint != null) {
				ToolTipLabelTopConstraint.Dispose ();
				ToolTipLabelTopConstraint = null;
			}
		}
	}
}
