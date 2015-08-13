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
	[Register ("PostController")]
	partial class PostController
	{
		[Outlet]
		UIKit.UIImageView BackgroundImageView { get; set; }

		[Outlet]
		UIKit.UILabel CountLabel { get; set; }

		[Outlet]
		UIKit.UIButton FacebookButton { get; set; }

		[Outlet]
		UIKit.UIView FooterView { get; set; }

		[Outlet]
		UIKit.UIButton PostButton { get; set; }

		[Outlet]
		UIKit.UITextView TextView { get; set; }

		[Outlet]
		UIKit.UIButton TwitterButton { get; set; }

		[Action ("OnCancelButtonTapped:")]
		partial void OnCancelButtonTapped (UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BackgroundImageView != null) {
				BackgroundImageView.Dispose ();
				BackgroundImageView = null;
			}

			if (CountLabel != null) {
				CountLabel.Dispose ();
				CountLabel = null;
			}

			if (FacebookButton != null) {
				FacebookButton.Dispose ();
				FacebookButton = null;
			}

			if (FooterView != null) {
				FooterView.Dispose ();
				FooterView = null;
			}

			if (PostButton != null) {
				PostButton.Dispose ();
				PostButton = null;
			}

			if (TextView != null) {
				TextView.Dispose ();
				TextView = null;
			}

			if (TwitterButton != null) {
				TwitterButton.Dispose ();
				TwitterButton = null;
			}
		}
	}
}
