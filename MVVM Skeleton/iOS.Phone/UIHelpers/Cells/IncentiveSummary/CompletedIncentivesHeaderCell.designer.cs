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
	[Register ("CompletedIncentivesHeaderCell")]
	partial class CompletedIncentivesHeaderCell
	{
		[Outlet]
		UIKit.UILabel PointsEarnedLabel { get; set; }

		[Outlet]
		UIKit.UILabel PointsEarnedTextLabel { get; set; }

		[Outlet]
		UIKit.UILabel PremiumEarnedLabel { get; set; }

		[Outlet]
		UIKit.UILabel PremiumEarnedTextLabel { get; set; }

		[Outlet]
		UIKit.UILabel TodoCountLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PointsEarnedLabel != null) {
				PointsEarnedLabel.Dispose ();
				PointsEarnedLabel = null;
			}

			if (PointsEarnedTextLabel != null) {
				PointsEarnedTextLabel.Dispose ();
				PointsEarnedTextLabel = null;
			}

			if (PremiumEarnedLabel != null) {
				PremiumEarnedLabel.Dispose ();
				PremiumEarnedLabel = null;
			}

			if (PremiumEarnedTextLabel != null) {
				PremiumEarnedTextLabel.Dispose ();
				PremiumEarnedTextLabel = null;
			}

			if (TodoCountLabel != null) {
				TodoCountLabel.Dispose ();
				TodoCountLabel = null;
			}
		}
	}
}
