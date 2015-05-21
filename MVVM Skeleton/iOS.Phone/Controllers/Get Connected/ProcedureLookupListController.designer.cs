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
	[Register ("ProcedureLookupListController")]
	partial class ProcedureLookupListController
	{
		[Outlet]
		UIKit.UILabel ResultErrorDetailLabel { get; set; }

		[Outlet]
		UIKit.UILabel ResultErrorLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ResultErrorLabel != null) {
				ResultErrorLabel.Dispose ();
				ResultErrorLabel = null;
			}

			if (ResultErrorDetailLabel != null) {
				ResultErrorDetailLabel.Dispose ();
				ResultErrorDetailLabel = null;
			}
		}
	}
}
