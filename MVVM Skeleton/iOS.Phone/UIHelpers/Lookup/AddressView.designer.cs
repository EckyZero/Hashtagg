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
	partial class AddressView
	{
		[Outlet]
		UIKit.UIButton NextButton { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TitleLabelCenterVerticallyConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TitleLabelToTopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabelToTopConstraint != null) {
				TitleLabelToTopConstraint.Dispose ();
				TitleLabelToTopConstraint = null;
			}

			if (TitleLabelCenterVerticallyConstraint != null) {
				TitleLabelCenterVerticallyConstraint.Dispose ();
				TitleLabelCenterVerticallyConstraint = null;
			}

			if (NextButton != null) {
				NextButton.Dispose ();
				NextButton = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}
		}
	}
}
