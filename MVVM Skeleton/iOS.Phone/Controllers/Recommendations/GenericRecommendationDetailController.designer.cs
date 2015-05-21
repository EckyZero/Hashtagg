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
	[Register ("GenericRecommendationDetailController")]
	partial class GenericRecommendationDetailController
	{
		[Outlet]
		UIKit.UIView FooterView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint FooterViewBottomConstraint { get; set; }

		[Outlet]
		MapKit.MKMapView MapView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint MapViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.UITableView TableView { get; set; }

		[Outlet]
		UIKit.UILabel YourDoctorLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MapView != null) {
				MapView.Dispose ();
				MapView = null;
			}

			if (MapViewBottomConstraint != null) {
				MapViewBottomConstraint.Dispose ();
				MapViewBottomConstraint = null;
			}

			if (TableView != null) {
				TableView.Dispose ();
				TableView = null;
			}

			if (YourDoctorLabel != null) {
				YourDoctorLabel.Dispose ();
				YourDoctorLabel = null;
			}

			if (FooterView != null) {
				FooterView.Dispose ();
				FooterView = null;
			}

			if (FooterViewBottomConstraint != null) {
				FooterViewBottomConstraint.Dispose ();
				FooterViewBottomConstraint = null;
			}
		}
	}
}
