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
	[Register ("BiometricResultDetailController")]
	partial class BiometricResultDetailController
	{
		[Outlet]
		UIKit.UIImageView DefinitionImageView { get; set; }

		[Outlet]
		UIKit.UILabel DefinitionText { get; set; }

		[Outlet]
		UIKit.UILabel ExplanationText { get; set; }

		[Outlet]
		UIKit.UILabel SummaryDate { get; set; }

		[Outlet]
		UIKit.UILabel SummaryMeasurementValue { get; set; }

		[Outlet]
		UIKit.UILabel SummaryStatus { get; set; }

		[Outlet]
		UIKit.UIView SummaryStatusView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DefinitionText != null) {
				DefinitionText.Dispose ();
				DefinitionText = null;
			}

			if (ExplanationText != null) {
				ExplanationText.Dispose ();
				ExplanationText = null;
			}

			if (SummaryDate != null) {
				SummaryDate.Dispose ();
				SummaryDate = null;
			}

			if (SummaryMeasurementValue != null) {
				SummaryMeasurementValue.Dispose ();
				SummaryMeasurementValue = null;
			}

			if (SummaryStatus != null) {
				SummaryStatus.Dispose ();
				SummaryStatus = null;
			}

			if (SummaryStatusView != null) {
				SummaryStatusView.Dispose ();
				SummaryStatusView = null;
			}

			if (DefinitionImageView != null) {
				DefinitionImageView.Dispose ();
				DefinitionImageView = null;
			}
		}
	}
}
