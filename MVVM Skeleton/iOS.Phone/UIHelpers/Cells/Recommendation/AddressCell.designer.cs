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
	[Register ("AddressCell")]
	partial class AddressCell
	{
		[Outlet]
		UIKit.UILabel AddressLabel { get; set; }

		[Outlet]
		UIKit.UIButton CallButton { get; set; }

		[Outlet]
		UIKit.UIButton DirectionsButton { get; set; }

		[Outlet]
		UIKit.UILabel DistanceLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (AddressLabel != null) {
				AddressLabel.Dispose ();
				AddressLabel = null;
			}

			if (DistanceLabel != null) {
				DistanceLabel.Dispose ();
				DistanceLabel = null;
			}

			if (DirectionsButton != null) {
				DirectionsButton.Dispose ();
				DirectionsButton = null;
			}
		}
	}
}
