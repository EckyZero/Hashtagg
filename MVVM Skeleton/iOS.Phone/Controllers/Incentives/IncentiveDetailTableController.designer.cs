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
	[Register ("IncentiveDetailTableController")]
	partial class IncentiveDetailTableController
	{
		[Outlet]
		UIKit.UILabel AboutDetailLabel { get; set; }

		[Outlet]
		UIKit.UILabel AvailableAmountDetailLabel { get; set; }

		[Outlet]
		UIKit.UILabel AvailableAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel AvailableDollarSignLabel { get; set; }

		[Outlet]
		UIKit.UILabel EarnedAmountLabel { get; set; }

		[Outlet]
		UIKit.UILabel EarnedDollarSignLabel { get; set; }

		[Outlet]
		UIKit.UILabel RewardDetailLabel { get; set; }

		[Outlet]
		UIKit.UIView RewardView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint RewardViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint RewardViewTopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AboutDetailLabel != null) {
				AboutDetailLabel.Dispose ();
				AboutDetailLabel = null;
			}

			if (AvailableAmountDetailLabel != null) {
				AvailableAmountDetailLabel.Dispose ();
				AvailableAmountDetailLabel = null;
			}

			if (AvailableAmountLabel != null) {
				AvailableAmountLabel.Dispose ();
				AvailableAmountLabel = null;
			}

			if (AvailableDollarSignLabel != null) {
				AvailableDollarSignLabel.Dispose ();
				AvailableDollarSignLabel = null;
			}

			if (EarnedAmountLabel != null) {
				EarnedAmountLabel.Dispose ();
				EarnedAmountLabel = null;
			}

			if (EarnedDollarSignLabel != null) {
				EarnedDollarSignLabel.Dispose ();
				EarnedDollarSignLabel = null;
			}

			if (RewardDetailLabel != null) {
				RewardDetailLabel.Dispose ();
				RewardDetailLabel = null;
			}

			if (RewardView != null) {
				RewardView.Dispose ();
				RewardView = null;
			}

			if (RewardViewHeightConstraint != null) {
				RewardViewHeightConstraint.Dispose ();
				RewardViewHeightConstraint = null;
			}

			if (RewardViewTopConstraint != null) {
				RewardViewTopConstraint.Dispose ();
				RewardViewTopConstraint = null;
			}
		}
	}
}
