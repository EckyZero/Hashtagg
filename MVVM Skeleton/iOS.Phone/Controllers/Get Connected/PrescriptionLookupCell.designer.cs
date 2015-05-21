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
	[Register ("PrescriptionLookupCell")]
	partial class PrescriptionLookupCell
	{
		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}
		}
	}
}
