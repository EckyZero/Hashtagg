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
	[Register ("DependentListController")]
	partial class DependentListController
	{
		[Outlet]
		UIKit.UIView HeaderView { get; set; }

		[Outlet]
		iOS.PSProgressView ProgressView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}

			if (HeaderView != null) {
				HeaderView.Dispose ();
				HeaderView = null;
			}
		}
	}
}
