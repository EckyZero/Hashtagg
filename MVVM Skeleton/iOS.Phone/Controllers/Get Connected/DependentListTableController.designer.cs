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
	[Register ("DependentListTableController")]
	partial class DependentListTableController
	{
		[Outlet]
		UIKit.UIButton AddAnotherButton { get; set; }

		[Outlet]
		UIKit.UIButton NextButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddAnotherButton != null) {
				AddAnotherButton.Dispose ();
				AddAnotherButton = null;
			}

			if (NextButton != null) {
				NextButton.Dispose ();
				NextButton = null;
			}
		}
	}
}
