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
	[Register ("DefaultCell")]
	partial class DefaultCell
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UITextView BodyTextView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint BodyTextViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIButton CommentButton { get; set; }

		[Outlet]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.UIButton LikeButton { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		iOS.UIImageViewClickable PhotoImageView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint PhotoImageViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIButton ShareButton { get; set; }

		[Outlet]
		UIKit.UIImageView SocialTypeImageView { get; set; }

		[Outlet]
		UIKit.UILabel TimeLabel { get; set; }

		[Outlet]
		UIKit.UIImageView UserImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}

			if (BodyTextView != null) {
				BodyTextView.Dispose ();
				BodyTextView = null;
			}

			if (BodyTextViewHeightConstraint != null) {
				BodyTextViewHeightConstraint.Dispose ();
				BodyTextViewHeightConstraint = null;
			}

			if (CommentButton != null) {
				CommentButton.Dispose ();
				CommentButton = null;
			}

			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (LikeButton != null) {
				LikeButton.Dispose ();
				LikeButton = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (PhotoImageView != null) {
				PhotoImageView.Dispose ();
				PhotoImageView = null;
			}

			if (PhotoImageViewHeightConstraint != null) {
				PhotoImageViewHeightConstraint.Dispose ();
				PhotoImageViewHeightConstraint = null;
			}

			if (ShareButton != null) {
				ShareButton.Dispose ();
				ShareButton = null;
			}

			if (SocialTypeImageView != null) {
				SocialTypeImageView.Dispose ();
				SocialTypeImageView = null;
			}

			if (TimeLabel != null) {
				TimeLabel.Dispose ();
				TimeLabel = null;
			}

			if (UserImageView != null) {
				UserImageView.Dispose ();
				UserImageView = null;
			}
		}
	}
}
