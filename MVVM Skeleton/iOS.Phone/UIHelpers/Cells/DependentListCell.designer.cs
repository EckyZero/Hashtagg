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
	partial class DependentListCell
	{
		[Outlet]
		UIKit.UIButton ActionButton { get; set; }

		[Outlet]
		UIKit.UILabel Body { get; set; }

		[Outlet]
		UIKit.UILabel ContentFieldOne { get; set; }

		[Outlet]
		UIKit.UILabel ContentFieldOneLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentFieldThree { get; set; }

		[Outlet]
		UIKit.UILabel ContentFieldThreeLabel { get; set; }

		[Outlet]
		UIKit.UILabel ContentFieldTwo { get; set; }

		[Outlet]
		UIKit.UILabel ContentFieldTwoLabel { get; set; }

		[Outlet]
		UIKit.UILabel Header { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActionButton != null) {
				ActionButton.Dispose ();
				ActionButton = null;
			}

			if (Body != null) {
				Body.Dispose ();
				Body = null;
			}

			if (ContentFieldOne != null) {
				ContentFieldOne.Dispose ();
				ContentFieldOne = null;
			}

			if (ContentFieldOneLabel != null) {
				ContentFieldOneLabel.Dispose ();
				ContentFieldOneLabel = null;
			}

			if (ContentFieldThree != null) {
				ContentFieldThree.Dispose ();
				ContentFieldThree = null;
			}

			if (ContentFieldThreeLabel != null) {
				ContentFieldThreeLabel.Dispose ();
				ContentFieldThreeLabel = null;
			}

			if (ContentFieldTwo != null) {
				ContentFieldTwo.Dispose ();
				ContentFieldTwo = null;
			}

			if (ContentFieldTwoLabel != null) {
				ContentFieldTwoLabel.Dispose ();
				ContentFieldTwoLabel = null;
			}

			if (Header != null) {
				Header.Dispose ();
				Header = null;
			}
		}
	}
}
