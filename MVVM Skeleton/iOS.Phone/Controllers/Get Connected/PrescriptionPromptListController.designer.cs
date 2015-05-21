// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOS.Phone
{
	[Register ("PrescriptionPromptListController")]
	partial class PrescriptionPromptListController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView HeaderView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		iOS.PSProgressView ProgressView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (HeaderView != null) {
				HeaderView.Dispose ();
				HeaderView = null;
			}
			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}
		}
	}
}
