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
	[Register ("DrActionCell")]
	partial class DrActionCell
	{
		[Outlet]
		UIKit.UIButton AddRemoveButton { get; set; }

		[Outlet]
		UIKit.UILabel AddRemoveLabel { get; set; }

		[Outlet]
		UIKit.UIButton CallButton { get; set; }

		[Outlet]
		UIKit.UILabel CallLabel { get; set; }

		[Outlet]
		UIKit.UIButton ScheduleButton { get; set; }

		[Outlet]
		UIKit.UILabel ScheduleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddRemoveButton != null) {
				AddRemoveButton.Dispose ();
				AddRemoveButton = null;
			}

			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (ScheduleButton != null) {
				ScheduleButton.Dispose ();
				ScheduleButton = null;
			}

			if (ScheduleLabel != null) {
				ScheduleLabel.Dispose ();
				ScheduleLabel = null;
			}

			if (AddRemoveLabel != null) {
				AddRemoveLabel.Dispose ();
				AddRemoveLabel = null;
			}

			if (CallLabel != null) {
				CallLabel.Dispose ();
				CallLabel = null;
			}
		}
	}
}
