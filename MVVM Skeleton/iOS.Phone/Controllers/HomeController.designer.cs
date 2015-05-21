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
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIView HelpView { get; set; }

		[Outlet]
		UIKit.UIImageView HelpViewAccessoryImageView { get; set; }

		[Outlet]
		UIKit.UIImageView HelpViewImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint HelpViewImageViewWidthConstraint { get; set; }

		[Outlet]
		UIKit.UILabel HelpViewLabel { get; set; }

		[Outlet]
		UIKit.UIImageView SlideImageView { get; set; }

		[Outlet]
		UIKit.UILabel SlideLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint SlideLabelBottomConstraint { get; set; }

		[Outlet]
		iOS.Phone.PSPullDownControl SlideView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint SlideViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TableViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIView TitleView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SlideView != null) {
				SlideView.Dispose ();
				SlideView = null;
			}

			if (SlideViewHeightConstraint != null) {
				SlideViewHeightConstraint.Dispose ();
				SlideViewHeightConstraint = null;
			}

			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (TitleView != null) {
				TitleView.Dispose ();
				TitleView = null;
			}

			if (HelpView != null) {
				HelpView.Dispose ();
				HelpView = null;
			}

			if (HelpViewImageView != null) {
				HelpViewImageView.Dispose ();
				HelpViewImageView = null;
			}

			if (HelpViewAccessoryImageView != null) {
				HelpViewAccessoryImageView.Dispose ();
				HelpViewAccessoryImageView = null;
			}

			if (HelpViewLabel != null) {
				HelpViewLabel.Dispose ();
				HelpViewLabel = null;
			}

			if (HelpViewImageViewWidthConstraint != null) {
				HelpViewImageViewWidthConstraint.Dispose ();
				HelpViewImageViewWidthConstraint = null;
			}

			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}

			if (SlideLabelBottomConstraint != null) {
				SlideLabelBottomConstraint.Dispose ();
				SlideLabelBottomConstraint = null;
			}

			if (TableViewTopConstraint != null) {
				TableViewTopConstraint.Dispose ();
				TableViewTopConstraint = null;
			}

			if (SlideLabel != null) {
				SlideLabel.Dispose ();
				SlideLabel = null;
			}

			if (SlideImageView != null) {
				SlideImageView.Dispose ();
				SlideImageView = null;
			}
		}
	}
}
