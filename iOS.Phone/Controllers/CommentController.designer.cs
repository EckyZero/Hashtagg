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
	[Register ("CommentController")]
	partial class CommentController
	{
		[Outlet]
		UIKit.UIView CommentContainerView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint CommentContainerViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.UITextView CommentTextView { get; set; }

		[Outlet]
		UIKit.UIButton ReplyButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ReplyButton != null) {
				ReplyButton.Dispose ();
				ReplyButton = null;
			}

			if (CommentTextView != null) {
				CommentTextView.Dispose ();
				CommentTextView = null;
			}

			if (CommentContainerView != null) {
				CommentContainerView.Dispose ();
				CommentContainerView = null;
			}

			if (CommentContainerViewBottomConstraint != null) {
				CommentContainerViewBottomConstraint.Dispose ();
				CommentContainerViewBottomConstraint = null;
			}
		}
	}
}
