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
	[Register ("MyProvidersPromptDetailController")]
	partial class MyProvidersPromptDetailController
	{
		[Outlet]
		UIKit.UIButton AddDoctorButton { get; set; }

		[Outlet]
		UIKit.UIButton DocRecButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddDoctorButton != null) {
				AddDoctorButton.Dispose ();
				AddDoctorButton = null;
			}

			if (DocRecButton != null) {
				DocRecButton.Dispose ();
				DocRecButton = null;
			}
		}
	}
}
