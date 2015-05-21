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
	[Register ("CalendarController")]
	partial class CalendarController
	{
		[Outlet]
		UIKit.UIButton AddButton { get; set; }

		[Outlet]
		TelerikUI.TKCalendar CalendarView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (AddButton != null) {
				AddButton.Dispose ();
				AddButton = null;
			}

			if (CalendarView != null) {
				CalendarView.Dispose ();
				CalendarView = null;
			}
		}
	}
}
