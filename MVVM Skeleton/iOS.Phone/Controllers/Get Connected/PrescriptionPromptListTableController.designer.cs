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
	[Register ("PrescriptionPromptListTableController")]
	partial class PrescriptionPromptListTableController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton AddButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton NextButton { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}
			if (NextButton != null) {
				NextButton.Dispose ();
				NextButton = null;
			}
		}
	}
}
