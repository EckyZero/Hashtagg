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
	[Register ("BiometricResultsController")]
	partial class BiometricResultsController
	{
		[Outlet]
		UIKit.UILabel DescriptionLabel { get; set; }

		[Outlet]
		UIKit.UIView HeaderView { get; set; }

		[Outlet]
		UIKit.UILabel SummaryRiskFactorCountLabel { get; set; }

		[Outlet]
		UIKit.UIView SummaryStatusBarView { get; set; }

		[Outlet]
		UIKit.UILabel SummarySubtitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel SummaryTitleLabel { get; set; }

		[Outlet]
		UIKit.UIView SummaryView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DescriptionLabel != null) {
				DescriptionLabel.Dispose ();
				DescriptionLabel = null;
			}

			if (SummaryRiskFactorCountLabel != null) {
				SummaryRiskFactorCountLabel.Dispose ();
				SummaryRiskFactorCountLabel = null;
			}

			if (SummaryStatusBarView != null) {
				SummaryStatusBarView.Dispose ();
				SummaryStatusBarView = null;
			}

			if (SummarySubtitleLabel != null) {
				SummarySubtitleLabel.Dispose ();
				SummarySubtitleLabel = null;
			}

			if (SummaryTitleLabel != null) {
				SummaryTitleLabel.Dispose ();
				SummaryTitleLabel = null;
			}

			if (SummaryView != null) {
				SummaryView.Dispose ();
				SummaryView = null;
			}

			if (HeaderView != null) {
				HeaderView.Dispose ();
				HeaderView = null;
			}
		}
	}
}
