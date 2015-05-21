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
	[Register ("BiometricResultCell")]
	partial class BiometricResultCell
	{
		[Outlet]
		UIKit.UIImageView DrillDownButton { get; set; }

		[Outlet]
		UIKit.UILabel MeasurementDateLabel { get; set; }

		[Outlet]
		UIKit.UILabel MeasurementValueLabel { get; set; }

		[Outlet]
		UIKit.UIImageView StatusImageView { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MeasurementValueLabel != null) {
				MeasurementValueLabel.Dispose ();
				MeasurementValueLabel = null;
			}

			if (MeasurementDateLabel != null) {
				MeasurementDateLabel.Dispose ();
				MeasurementDateLabel = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (StatusImageView != null) {
				StatusImageView.Dispose ();
				StatusImageView = null;
			}

			if (DrillDownButton != null) {
				DrillDownButton.Dispose ();
				DrillDownButton = null;
			}
		}
	}
}
