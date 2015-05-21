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
	[Register ("GenericTableController")]
	partial class GenericTableController
	{
		[Outlet]
		UIKit.UIButton FooterButton { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}

			if (FooterButton != null) {
				FooterButton.Dispose ();
				FooterButton = null;
			}
		}
	}
}
