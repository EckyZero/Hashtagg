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
	[Register ("IncentivesActionDetail")]
	partial class IncentivesActionDetail
	{
		[Outlet]
		UIKit.UILabel BodyLabel { get; set; }

		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UILabel DateTextSubtitleLabel { get; set; }

		[Outlet]
		UIKit.UIView StatusBarView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (BodyLabel != null) {
				BodyLabel.Dispose ();
				BodyLabel = null;
			}

			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (StatusBarView != null) {
				StatusBarView.Dispose ();
				StatusBarView = null;
			}

			if (DateTextSubtitleLabel != null) {
				DateTextSubtitleLabel.Dispose ();
				DateTextSubtitleLabel = null;
			}
		}
	}
}
