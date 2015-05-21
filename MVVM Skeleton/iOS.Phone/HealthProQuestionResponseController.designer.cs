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
	[Register ("HealthProQuestionResponseController")]
	partial class HealthProQuestionResponseController
	{
		[Outlet]
		UIKit.UIButton CallButton { get; set; }

		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		iOS.Phone.ExtendedTextView CommentsTextView { get; set; }

		[Outlet]
		iOS.PSFloatingTextControl ContactControl { get; set; }

		[Outlet]
		UIKit.UILabel ContentHeader { get; set; }

		[Outlet]
		UIKit.UILabel ContentSubHeader { get; set; }

		[Outlet]
		UIKit.UIImageView HealthProImage { get; set; }

		[Outlet]
		UIKit.UILabel HowToRespondLabel { get; set; }

		[Outlet]
		iOS.PSFloatingPickerControl QuestionControl { get; set; }

		[Outlet]
		iOS.PSSegmentedControl RequestedResponseControl { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ScrollViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.UIButton SendButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (ContactControl != null) {
				ContactControl.Dispose ();
				ContactControl = null;
			}

			if (ContentHeader != null) {
				ContentHeader.Dispose ();
				ContentHeader = null;
			}

			if (ContentSubHeader != null) {
				ContentSubHeader.Dispose ();
				ContentSubHeader = null;
			}

			if (HealthProImage != null) {
				HealthProImage.Dispose ();
				HealthProImage = null;
			}

			if (HowToRespondLabel != null) {
				HowToRespondLabel.Dispose ();
				HowToRespondLabel = null;
			}

			if (QuestionControl != null) {
				QuestionControl.Dispose ();
				QuestionControl = null;
			}

			if (RequestedResponseControl != null) {
				RequestedResponseControl.Dispose ();
				RequestedResponseControl = null;
			}

			if (CommentsTextView != null) {
				CommentsTextView.Dispose ();
				CommentsTextView = null;
			}

			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (ScrollViewBottomConstraint != null) {
				ScrollViewBottomConstraint.Dispose ();
				ScrollViewBottomConstraint = null;
			}

			if (SendButton != null) {
				SendButton.Dispose ();
				SendButton = null;
			}
		}
	}
}
