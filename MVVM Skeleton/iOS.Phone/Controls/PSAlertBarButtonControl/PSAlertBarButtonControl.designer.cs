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
	partial class PSAlertBarButtonControl
	{
		[Outlet]
		UIKit.UIView BadgeView { get; set; }

		[Outlet]
		UIKit.UILabel CountLabel { get; set; }

		[Outlet]
		UIKit.UIImageView MainImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint MainImageViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint MainImageViewTopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CountLabel != null) {
				CountLabel.Dispose ();
				CountLabel = null;
			}

			if (MainImageView != null) {
				MainImageView.Dispose ();
				MainImageView = null;
			}

			if (MainImageViewBottomConstraint != null) {
				MainImageViewBottomConstraint.Dispose ();
				MainImageViewBottomConstraint = null;
			}

			if (MainImageViewTopConstraint != null) {
				MainImageViewTopConstraint.Dispose ();
				MainImageViewTopConstraint = null;
			}

			if (BadgeView != null) {
				BadgeView.Dispose ();
				BadgeView = null;
			}
		}
	}
}
