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
	[Register ("HomeController")]
	partial class HomeController
	{
		[Outlet]
		UIKit.UIImageView AccountImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint AccountImageViewTrailingConstraint { get; set; }

		[Outlet]
		UIKit.UIView AccountsView { get; set; }

		[Outlet]
		UIKit.UIView HeaderView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint HeaderViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem PostBarButton { get; set; }

		[Outlet]
		UIKit.UISegmentedControl SortOrderSegmentedControl { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TableViewTopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AccountsView != null) {
				AccountsView.Dispose ();
				AccountsView = null;
			}

			if (HeaderView != null) {
				HeaderView.Dispose ();
				HeaderView = null;
			}

			if (HeaderViewTopConstraint != null) {
				HeaderViewTopConstraint.Dispose ();
				HeaderViewTopConstraint = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (PostBarButton != null) {
				PostBarButton.Dispose ();
				PostBarButton = null;
			}

			if (SortOrderSegmentedControl != null) {
				SortOrderSegmentedControl.Dispose ();
				SortOrderSegmentedControl = null;
			}

			if (TableViewTopConstraint != null) {
				TableViewTopConstraint.Dispose ();
				TableViewTopConstraint = null;
			}

			if (AccountImageView != null) {
				AccountImageView.Dispose ();
				AccountImageView = null;
			}

			if (AccountImageViewTrailingConstraint != null) {
				AccountImageViewTrailingConstraint.Dispose ();
				AccountImageViewTrailingConstraint = null;
			}
		}
	}
}
