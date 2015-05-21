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
	[Register ("CostCell")]
	partial class CostCell
	{
		[Outlet]
		UIKit.UILabel AboutLabel { get; set; }

		[Outlet]
		UIKit.UILabel CostTitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel CostValueLabel { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }

		[Outlet]
		UIKit.UIButton TooltipButton { get; set; }

		[Outlet]
		UIKit.UILabel TotalLabel { get; set; }

		[Outlet]
		UIKit.UILabel TotalTitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AboutLabel != null) {
				AboutLabel.Dispose ();
				AboutLabel = null;
			}

			if (CostTitleLabel != null) {
				CostTitleLabel.Dispose ();
				CostTitleLabel = null;
			}

			if (CostValueLabel != null) {
				CostValueLabel.Dispose ();
				CostValueLabel = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (TotalLabel != null) {
				TotalLabel.Dispose ();
				TotalLabel = null;
			}

			if (TooltipButton != null) {
				TooltipButton.Dispose ();
				TooltipButton = null;
			}

			if (TotalTitleLabel != null) {
				TotalTitleLabel.Dispose ();
				TotalTitleLabel = null;
			}
		}
	}
}
