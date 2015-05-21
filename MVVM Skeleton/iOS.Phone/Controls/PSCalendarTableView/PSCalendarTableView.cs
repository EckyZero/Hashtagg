
using System;
using System.Drawing;

using Foundation;
using UIKit;
using TimesSquare.iOS;

namespace iOS.Phone
{
	public partial class PSCalendarTableView : TSQCalendarView
	{

		public PSCalendarTableView(IntPtr handle) : base (handle) 
		{
			Initialize ();
		}
			
			
		private void Initialize()
		{
			Calendar = new NSCalendar (NSCalendarType.Gregorian);
			FirstDate = NSDate.Now;
			RowCellClass = new ObjCRuntime.Class ("CPHTSQCalendarRowCell");
			LastDate = NSDate.FromTimeIntervalSinceNow (60 * 60 * 24 * 365);
			BackgroundColor = UIColor.LightTextColor;
			PagingEnabled = false;
		}

		public void Listen(Action<DateTime> processDate)
		{
			DidSelectDate += (object sender, TSQCalendarViewDelegateAEventArgs e) => {
				processDate.Invoke((DateTime)e.Date);
			};
		}
	}
}

