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
	partial class PSProgressView
	{
		[Outlet]
		UIKit.UILabel Label { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint LabelLeadingConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint LabelWidthConstraint { get; set; }

		[Outlet]
		UIKit.UIProgressView ProgressView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Label != null) {
				Label.Dispose ();
				Label = null;
			}

			if (LabelLeadingConstraint != null) {
				LabelLeadingConstraint.Dispose ();
				LabelLeadingConstraint = null;
			}

			if (LabelWidthConstraint != null) {
				LabelWidthConstraint.Dispose ();
				LabelWidthConstraint = null;
			}

			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}
		}
	}
}
