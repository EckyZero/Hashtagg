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
	[Register ("DependentPromptController")]
	partial class DependentPromptController
	{
		[Outlet]
		UIKit.UIButton NoButton { get; set; }

		[Outlet]
		iOS.PSProgressView ProgressView { get; set; }

		[Outlet]
		UIKit.UIButton SkipButton { get; set; }

		[Outlet]
		UIKit.UIButton YesButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NoButton != null) {
				NoButton.Dispose ();
				NoButton = null;
			}

			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}

			if (YesButton != null) {
				YesButton.Dispose ();
				YesButton = null;
			}

			if (SkipButton != null) {
				SkipButton.Dispose ();
				SkipButton = null;
			}
		}
	}
}
