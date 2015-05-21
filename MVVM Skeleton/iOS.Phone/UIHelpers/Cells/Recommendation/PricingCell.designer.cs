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
	[Register ("PricingCell")]
	partial class PricingCell
	{
		[Outlet]
		UIKit.UILabel FirstProcedureTitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel FirstProcedureValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel PricingDetailLabel { get; set; }

		[Outlet]
		UIKit.UILabel PricingTitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel ProcedureTitleLabel { get; set; }

		[Outlet]
		UIKit.UIButton TooltipButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FirstProcedureTitleLabel != null) {
				FirstProcedureTitleLabel.Dispose ();
				FirstProcedureTitleLabel = null;
			}

			if (FirstProcedureValueLabel != null) {
				FirstProcedureValueLabel.Dispose ();
				FirstProcedureValueLabel = null;
			}

			if (PricingDetailLabel != null) {
				PricingDetailLabel.Dispose ();
				PricingDetailLabel = null;
			}

			if (PricingTitleLabel != null) {
				PricingTitleLabel.Dispose ();
				PricingTitleLabel = null;
			}

			if (ProcedureTitleLabel != null) {
				ProcedureTitleLabel.Dispose ();
				ProcedureTitleLabel = null;
			}

			if (TooltipButton != null) {
				TooltipButton.Dispose ();
				TooltipButton = null;
			}
		}
	}
}
