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
	[Register ("SuperlativeCell")]
	partial class SuperlativeCell
	{
		[Outlet]
		UIKit.NSLayoutConstraint AccredationBottomConstraint { get; set; }

		[Outlet]
		UIKit.UILabel AccredationLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint AccredationTopConstraint { get; set; }

		[Outlet]
		UIKit.UILabel NetworkLabel { get; set; }

		[Outlet]
		UIKit.UIImageView SuperlativeImageView { get; set; }

		[Outlet]
		UIKit.UILabel SuperlativeValueLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AccredationBottomConstraint != null) {
				AccredationBottomConstraint.Dispose ();
				AccredationBottomConstraint = null;
			}

			if (AccredationLabel != null) {
				AccredationLabel.Dispose ();
				AccredationLabel = null;
			}

			if (AccredationTopConstraint != null) {
				AccredationTopConstraint.Dispose ();
				AccredationTopConstraint = null;
			}

			if (NetworkLabel != null) {
				NetworkLabel.Dispose ();
				NetworkLabel = null;
			}

			if (SuperlativeImageView != null) {
				SuperlativeImageView.Dispose ();
				SuperlativeImageView = null;
			}

			if (SuperlativeValueLabel != null) {
				SuperlativeValueLabel.Dispose ();
				SuperlativeValueLabel = null;
			}
		}
	}
}
