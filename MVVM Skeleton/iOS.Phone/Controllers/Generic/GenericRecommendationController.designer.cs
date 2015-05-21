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
	[Register ("GenericRecommendationController")]
	partial class GenericRecommendationController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIImageView ContextFooterImageView { get; set; }

		[Outlet]
		UIKit.UILabel ContextFooterLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ContextHeaderImageView { get; set; }

		[Outlet]
		UIKit.UILabel ContextHeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContextSubheaderLabel { get; set; }

		[Outlet]
		UIKit.UIButton MapLegendButton { get; set; }

		[Outlet]
		UIKit.UIButton MapLegendFullScreenButton { get; set; }

		[Outlet]
		UIKit.UIImageView MapLegendImageView { get; set; }

		[Outlet]
		UIKit.UIView MapLegendView { get; set; }

		[Outlet]
		MapKit.MKMapView MapView { get; set; }

		[Outlet]
		UIKit.UIImageView ReviewsByImageView { get; set; }

		[Outlet]
		UIKit.UILabel ReviewsByLabel { get; set; }

		[Outlet]
		UIKit.UIButton ShowMoreButton { get; set; }

		[Outlet]
		iOS.Phone.PSPullDownControl SlideView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint SlideViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		[Outlet]
		UIKit.UILabel YourDoctorLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}

			if (ContextFooterImageView != null) {
				ContextFooterImageView.Dispose ();
				ContextFooterImageView = null;
			}

			if (ContextFooterLabel != null) {
				ContextFooterLabel.Dispose ();
				ContextFooterLabel = null;
			}

			if (ContextHeaderImageView != null) {
				ContextHeaderImageView.Dispose ();
				ContextHeaderImageView = null;
			}

			if (ContextHeaderLabel != null) {
				ContextHeaderLabel.Dispose ();
				ContextHeaderLabel = null;
			}

			if (ContextSubheaderLabel != null) {
				ContextSubheaderLabel.Dispose ();
				ContextSubheaderLabel = null;
			}

			if (MapLegendButton != null) {
				MapLegendButton.Dispose ();
				MapLegendButton = null;
			}

			if (MapLegendImageView != null) {
				MapLegendImageView.Dispose ();
				MapLegendImageView = null;
			}

			if (MapLegendView != null) {
				MapLegendView.Dispose ();
				MapLegendView = null;
			}

			if (MapView != null) {
				MapView.Dispose ();
				MapView = null;
			}

			if (ReviewsByImageView != null) {
				ReviewsByImageView.Dispose ();
				ReviewsByImageView = null;
			}

			if (ReviewsByLabel != null) {
				ReviewsByLabel.Dispose ();
				ReviewsByLabel = null;
			}

			if (ShowMoreButton != null) {
				ShowMoreButton.Dispose ();
				ShowMoreButton = null;
			}

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

			if (YourDoctorLabel != null) {
				YourDoctorLabel.Dispose ();
				YourDoctorLabel = null;
			}

			if (MapLegendFullScreenButton != null) {
				MapLegendFullScreenButton.Dispose ();
				MapLegendFullScreenButton = null;
			}
		}
	}
}
