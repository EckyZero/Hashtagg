using System;
using System.Drawing;

using CoreFoundation;
using UIKit;
using Foundation;
using TelerikUI;

namespace iOS.Phone.Controllers.Generic
{
    [Register("GenericTelerikCalendar")]
    public class GenericTelerikCalendar : UIViewController
    {
        public GenericTelerikCalendar()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Creates a new calendar
            TKCalendar calendarView = new TKCalendar(this.View.Bounds);
			NSCalendar calendar = NSCalendar.CurrentCalendar;

            // resize to fit phone
            calendarView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;

            // Sets the minimum date the user can select
			calendarView.MinDate = TKCalendar.DateWithYear(2000, 1, 1, calendar);

            // Sets the maximum date the user can select
			calendarView.MaxDate = TKCalendar.DateWithYear(2020, 1, 1, calendar);

            // Sets the selection mode (Single, Multiple, Range)
            calendarView.SelectionMode = TKCalendarSelectionMode.Multiple;

            // Allows listening for events
            calendarView.Delegate = new CalendarDelegate();

            this.View.AddSubview(calendarView);
        }

        private class CalendarDelegate : TKCalendarDelegate
        {
            // Listens to date selected by user
            public override void DidSelectDate(TKCalendar calendar, NSDate date)
            {
            }
        }
    }
}