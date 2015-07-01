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
	[Register ("HomeController")]
	partial class HomeController
	{
		[Outlet]
		UIKit.UIView AccountsView { get; set; }

		[Outlet]
		UIKit.UIView HeaderView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint HeaderViewTopConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TableViewTopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeaderView != null) {
				HeaderView.Dispose ();
				HeaderView = null;
			}

			if (HeaderViewTopConstraint != null) {
				HeaderViewTopConstraint.Dispose ();
				HeaderViewTopConstraint = null;
			}

			if (TableViewTopConstraint != null) {
				TableViewTopConstraint.Dispose ();
				TableViewTopConstraint = null;
			}

			if (AccountsView != null) {
				AccountsView.Dispose ();
				AccountsView = null;
			}
		}
	}
}
