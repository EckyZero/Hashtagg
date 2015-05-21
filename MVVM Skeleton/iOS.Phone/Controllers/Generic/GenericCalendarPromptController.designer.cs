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
	[Register ("GenericCalendarPromptController")]
	partial class GenericCalendarPromptController
	{
		[Outlet]
		UIKit.UIView CalendarView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint CalendarViewHeightConstraint { get; set; }

		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UIButton ChangeButton { get; set; }

		[Outlet]
		UIKit.UILabel ContentBodyLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentFooterLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentHeaderLabel { get; set; }

		[Outlet]
		UIKit.UIButton DateButton { get; set; }

		[Outlet]
		UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		UIKit.UIButton SubmitButton { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint SubtitleLabelTopConstraint { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (ChangeButton != null) {
				ChangeButton.Dispose ();
				ChangeButton = null;
			}

			if (ContentBodyLabel != null) {
				ContentBodyLabel.Dispose ();
				ContentBodyLabel = null;
			}

			if (ContentFooterLabel != null) {
				ContentFooterLabel.Dispose ();
				ContentFooterLabel = null;
			}

			if (ContentHeaderLabel != null) {
				ContentHeaderLabel.Dispose ();
				ContentHeaderLabel = null;
			}

			if (DateButton != null) {
				DateButton.Dispose ();
				DateButton = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (SubmitButton != null) {
				SubmitButton.Dispose ();
				SubmitButton = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (SubtitleLabelTopConstraint != null) {
				SubtitleLabelTopConstraint.Dispose ();
				SubtitleLabelTopConstraint = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (CalendarView != null) {
				CalendarView.Dispose ();
				CalendarView = null;
			}

			if (CalendarViewHeightConstraint != null) {
				CalendarViewHeightConstraint.Dispose ();
				CalendarViewHeightConstraint = null;
			}
		}
	}
}
