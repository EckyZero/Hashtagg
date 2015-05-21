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
	[Register ("FacilityBioSummaryCell")]
	partial class FacilityBioSummaryCell
	{
		[Outlet]
		UIKit.NSLayoutConstraint AddressBottomConstraint { get; set; }

		[Outlet]
		UIKit.UILabel AddressLabel { get; set; }

		[Outlet]
		UIKit.UILabel CostTitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel CostValueLabel { get; set; }

		[Outlet]
		UIKit.UIButton DirectionButton { get; set; }

		[Outlet]
		UIKit.UILabel DistanceLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ImageViewHieghtConstraint { get; set; }

		[Outlet]
		UIKit.UIImageView MainImageView { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UIButton PhoneButton { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.UIButton WebsiteButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageViewHieghtConstraint != null) {
				ImageViewHieghtConstraint.Dispose ();
				ImageViewHieghtConstraint = null;
			}

			if (AddressBottomConstraint != null) {
				AddressBottomConstraint.Dispose ();
				AddressBottomConstraint = null;
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

			if (DirectionButton != null) {
				DirectionButton.Dispose ();
				DirectionButton = null;
			}

			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
			}

			if (MainImageView != null) {
				MainImageView.Dispose ();
				MainImageView = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (PhoneButton != null) {
				PhoneButton.Dispose ();
				PhoneButton = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (WebsiteButton != null) {
				WebsiteButton.Dispose ();
				WebsiteButton = null;
			}
		}
	}
}
