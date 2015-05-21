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
	[Register ("HealthDetailsController")]
	partial class HealthDetailsController
	{
		[Outlet]
		UIKit.UIView ConditionView { get; set; }

		[Outlet]
		UIKit.UIView ProcedureView { get; set; }

		[Outlet]
		UIKit.UIView ResultView { get; set; }

		[Outlet]
		SegmentedControl.SDSegmentedControl SegmentedControl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SegmentedControl != null) {
				SegmentedControl.Dispose ();
				SegmentedControl = null;
			}

			if (ProcedureView != null) {
				ProcedureView.Dispose ();
				ProcedureView = null;
			}

			if (ConditionView != null) {
				ConditionView.Dispose ();
				ConditionView = null;
			}

			if (ResultView != null) {
				ResultView.Dispose ();
				ResultView = null;
			}
		}
	}
}
