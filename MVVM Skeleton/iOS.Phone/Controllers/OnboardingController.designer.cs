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
	[Register ("OnboardingController")]
	partial class OnboardingController
	{
		[Outlet]
		UIKit.UIButton FacebookButton { get; set; }

		[Outlet]
		UIKit.UIButton GoButton { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint GoButtonBottomConstraint { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.UIImageView TitleImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TitleImageViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIButton TwitterButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FacebookButton != null) {
				FacebookButton.Dispose ();
				FacebookButton = null;
			}

			if (GoButton != null) {
				GoButton.Dispose ();
				GoButton = null;
			}

			if (GoButtonBottomConstraint != null) {
				GoButtonBottomConstraint.Dispose ();
				GoButtonBottomConstraint = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (TwitterButton != null) {
				TwitterButton.Dispose ();
				TwitterButton = null;
			}

			if (TitleImageView != null) {
				TitleImageView.Dispose ();
				TitleImageView = null;
			}

			if (TitleImageViewTopConstraint != null) {
				TitleImageViewTopConstraint.Dispose ();
				TitleImageViewTopConstraint = null;
			}
		}
	}
}
