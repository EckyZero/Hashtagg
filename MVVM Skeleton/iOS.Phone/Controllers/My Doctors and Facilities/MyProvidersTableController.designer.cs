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
	[Register ("MyProvidersTableController")]
	partial class MyProvidersTableController
	{
		[Outlet]
		UIKit.UIButton AddAnotherDoctorButton { get; set; }

		[Outlet]
		UIKit.UIButton DocPrefButton { get; set; }

		[Outlet]
		UIKit.UIButton DocRecButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddAnotherDoctorButton != null) {
				AddAnotherDoctorButton.Dispose ();
				AddAnotherDoctorButton = null;
			}

			if (DocPrefButton != null) {
				DocPrefButton.Dispose ();
				DocPrefButton = null;
			}

			if (DocRecButton != null) {
				DocRecButton.Dispose ();
				DocRecButton = null;
			}
		}
	}
}
