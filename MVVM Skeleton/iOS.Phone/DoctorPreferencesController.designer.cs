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
	[Register ("DoctorPreferencesController")]
	partial class DoctorPreferencesController
	{
		[Outlet]
		iOS.PSSegmentedControl AgeControl { get; set; }

		[Outlet]
		UIKit.UILabel AgeHeaderLabel { get; set; }

		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		iOS.PSSegmentedControl GenderControl { get; set; }

		[Outlet]
		UIKit.UILabel GenderHeaderLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint GenderHeaderTopConstraint { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel LookupHeaderLabel { get; set; }

		[Outlet]
		iOS.PSProgressView ProgressView { get; set; }

		[Outlet]
		iOS.PSPromptButton PromptButton { get; set; }

		[Outlet]
		UIKit.UIScrollView ScrollView { get; set; }

		[Outlet]
		UIKit.UIButton SearchButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}

			if (AgeControl != null) {
				AgeControl.Dispose ();
				AgeControl = null;
			}

			if (AgeHeaderLabel != null) {
				AgeHeaderLabel.Dispose ();
				AgeHeaderLabel = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (GenderControl != null) {
				GenderControl.Dispose ();
				GenderControl = null;
			}

			if (GenderHeaderLabel != null) {
				GenderHeaderLabel.Dispose ();
				GenderHeaderLabel = null;
			}

			if (GenderHeaderTopConstraint != null) {
				GenderHeaderTopConstraint.Dispose ();
				GenderHeaderTopConstraint = null;
			}

			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}

			if (LookupHeaderLabel != null) {
				LookupHeaderLabel.Dispose ();
				LookupHeaderLabel = null;
			}

			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}

			if (PromptButton != null) {
				PromptButton.Dispose ();
				PromptButton = null;
			}

			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}
		}
	}
}
