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
	[Register ("SurveyCellThreeButton")]
	partial class SurveyCellThreeButton
	{
		[Outlet]
		UIKit.UIButton CenterButton { get; set; }

		[Outlet]
		UIKit.UIButton LeftButton { get; set; }

		[Outlet]
		UIKit.UIButton RightButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LeftButton != null) {
				LeftButton.Dispose ();
				LeftButton = null;
			}

			if (CenterButton != null) {
				CenterButton.Dispose ();
				CenterButton = null;
			}

			if (RightButton != null) {
				RightButton.Dispose ();
				RightButton = null;
			}
		}
	}
}
