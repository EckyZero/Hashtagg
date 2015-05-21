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
	[Register ("PrescriptionLookupListController")]
	partial class PrescriptionLookupListController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel ResultErrorDetailLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel ResultErrorLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ResultErrorDetailLabel != null) {
				ResultErrorDetailLabel.Dispose ();
				ResultErrorDetailLabel = null;
			}
			if (ResultErrorLabel != null) {
				ResultErrorLabel.Dispose ();
				ResultErrorLabel = null;
			}
		}
	}
}
