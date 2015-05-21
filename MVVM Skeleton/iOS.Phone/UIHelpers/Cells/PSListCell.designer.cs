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
	partial class PSListCell
	{
		[Outlet]
		UIKit.UIButton ActionButton { get; set; }

		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		UIKit.UILabel FooterLabel { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }

		[Outlet]
		UIKit.UILabel SecondFooterLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActionButton != null) {
				ActionButton.Dispose ();
				ActionButton = null;
			}

			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}

			if (FooterLabel != null) {
				FooterLabel.Dispose ();
				FooterLabel = null;
			}

			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}

			if (SecondFooterLabel != null) {
				SecondFooterLabel.Dispose ();
				SecondFooterLabel = null;
			}
		}
	}
}
