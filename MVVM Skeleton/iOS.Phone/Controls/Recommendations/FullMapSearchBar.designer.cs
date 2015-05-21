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
	partial class FullMapSearchBar
	{
		[Outlet]
		UIKit.UIButton PromptButton { get; set; }

		[Outlet]
		UIKit.UILabel SearchTextLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SearchTextLabel != null) {
				SearchTextLabel.Dispose ();
				SearchTextLabel = null;
			}

			if (PromptButton != null) {
				PromptButton.Dispose ();
				PromptButton = null;
			}
		}
	}
}
