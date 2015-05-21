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
	[Register ("FacilityPrivilegesCell")]
	partial class FacilityPrivilegesCell
	{
		[Outlet]
		UIKit.UILabel AddressLabel { get; set; }

		[Outlet]
		UIKit.UILabel CostTitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel CostValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel DistanceLabel { get; set; }

		[Outlet]
		UIKit.UIImageView InfoImageView { get; set; }

		[Outlet]
		UIKit.UILabel InfoLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint InfoLabelBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint InfoLabelTopConstraint { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel PhoneLabel { get; set; }

		[Outlet]
		UIKit.UIView SideView { get; set; }

		[Outlet]
		UIKit.UIImageView SuperlativeImageView { get; set; }

		[Outlet]
		UIKit.UILabel SuperlativeLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SuperlativeImageView != null) {
				SuperlativeImageView.Dispose ();
				SuperlativeImageView = null;
			}

			if (SuperlativeLabel != null) {
				SuperlativeLabel.Dispose ();
				SuperlativeLabel = null;
			}

			if (AddressLabel != null) {
				AddressLabel.Dispose ();
				AddressLabel = null;
			}

			if (CostTitleLabel != null) {
				CostTitleLabel.Dispose ();
				CostTitleLabel = null;
			}

			if (CostValueLabel != null) {
				CostValueLabel.Dispose ();
				CostValueLabel = null;
			}

			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
			}

			if (InfoImageView != null) {
				InfoImageView.Dispose ();
				InfoImageView = null;
			}

			if (InfoLabel != null) {
				InfoLabel.Dispose ();
				InfoLabel = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (PhoneLabel != null) {
				PhoneLabel.Dispose ();
				PhoneLabel = null;
			}

			if (InfoLabelBottomConstraint != null) {
				InfoLabelBottomConstraint.Dispose ();
				InfoLabelBottomConstraint = null;
			}

			if (InfoLabelTopConstraint != null) {
				InfoLabelTopConstraint.Dispose ();
				InfoLabelTopConstraint = null;
			}

			if (SideView != null) {
				SideView.Dispose ();
				SideView = null;
			}
		}
	}
}
