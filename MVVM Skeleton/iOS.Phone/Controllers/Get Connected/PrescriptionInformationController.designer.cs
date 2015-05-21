// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOS.Phone
{
	[Register ("PrescriptionInformationController")]
	partial class PrescriptionInformationController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton AddItButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CancelButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ChangeButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ContainerView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		iOS.PSFloatingTextControl DosageTextControl { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		iOS.PSFloatingTextControl FrequencyTextControl { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel PrescriptionLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		iOS.PSProgressView ProgressView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AddItButton != null) {
				AddItButton.Dispose ();
				AddItButton = null;
			}
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
			if (ChangeButton != null) {
				ChangeButton.Dispose ();
				ChangeButton = null;
			}
			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}
			if (DosageTextControl != null) {
				DosageTextControl.Dispose ();
				DosageTextControl = null;
			}
			if (FrequencyTextControl != null) {
				FrequencyTextControl.Dispose ();
				FrequencyTextControl = null;
			}
			if (PrescriptionLabel != null) {
				PrescriptionLabel.Dispose ();
				PrescriptionLabel = null;
			}
			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}
		}
	}
}
