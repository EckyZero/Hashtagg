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
	[Register ("GenericPromptDetailController")]
	partial class GenericPromptDetailController
	{
		[Outlet]
		UIKit.UILabel ContentBodyLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentFooterLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentHeaderLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		UIKit.UIButton PromptButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (ContentHeaderLabel != null) {
				ContentHeaderLabel.Dispose ();
				ContentHeaderLabel = null;
			}

			if (ContentBodyLabel != null) {
				ContentBodyLabel.Dispose ();
				ContentBodyLabel = null;
			}

			if (ContentFooterLabel != null) {
				ContentFooterLabel.Dispose ();
				ContentFooterLabel = null;
			}

			if (PromptButton != null) {
				PromptButton.Dispose ();
				PromptButton = null;
			}
		}
	}
}
