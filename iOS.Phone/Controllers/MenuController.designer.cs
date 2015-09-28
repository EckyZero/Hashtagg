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
	[Register ("MenuController")]
	partial class MenuController
	{
		[Outlet]
		UIKit.UIButton PrimaryButton { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView TitleImageView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PrimaryButton != null) {
				PrimaryButton.Dispose ();
				PrimaryButton = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (TitleImageView != null) {
				TitleImageView.Dispose ();
				TitleImageView = null;
			}
		}
	}
}
