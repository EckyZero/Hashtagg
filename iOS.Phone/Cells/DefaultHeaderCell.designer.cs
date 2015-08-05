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
	[Register ("DefaultHeaderCell")]
	partial class DefaultHeaderCell
	{
		[Outlet]
		UIKit.UIView CircleView { get; set; }

		[Outlet]
		UIKit.UIView LineView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (CircleView != null) {
				CircleView.Dispose ();
				CircleView = null;
			}

			if (LineView != null) {
				LineView.Dispose ();
				LineView = null;
			}
		}
	}
}
