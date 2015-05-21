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
	[Register ("GenericSurveyTableController")]
	partial class GenericSurveyTableController
	{
		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ImageViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ImageViewHeightConstraint { get; set; }

		[Outlet]
		iOS.PSProgressView ProgressView { get; set; }

		[Outlet]
		UIKit.UILabel SubHeaderLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}

			if (SubHeaderLabel != null) {
				SubHeaderLabel.Dispose ();
				SubHeaderLabel = null;
			}

			if (ImageViewHeightConstraint != null) {
				ImageViewHeightConstraint.Dispose ();
				ImageViewHeightConstraint = null;
			}

			if (ImageViewBottomConstraint != null) {
				ImageViewBottomConstraint.Dispose ();
				ImageViewBottomConstraint = null;
			}
		}
	}
}
