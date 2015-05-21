using System;
using TimesSquare.iOS;
using Foundation;
using UIKit;
using CoreGraphics;


namespace TimesSquareiOSSample
{
	[Register ("CPHTSQCalendarRowCell")]
	public class CPHTSQCalendarRowCell : TSQCalendarRowCell
	{

		public CPHTSQCalendarRowCell () : base ()
		{			
		}

		public CPHTSQCalendarRowCell (IntPtr handler) : base (handler)
		{
		}

		public override void LayoutViews (nuint index, CGRect rect)
		{
			rect.Y += ColumnSpacing;
			rect.Height -= BottomRow ? 2.0f : 1.0f * ColumnSpacing;
			base.LayoutViews (index, rect);
		}

		public override UIImage TodayBackgroundImage {
			get {
				return UIImage.FromBundle ("calendar_images/CalendarTodaysDate.png").StretchableImage (4, 4);
			}
		}

		public override UIImage SelectedBackgroundImage {
			get {
				return UIImage.FromBundle ("calendar_images/CalendarSelectedDate.png").StretchableImage (4, 4);
			}
		}

		public override UIImage NotThisMonthBackgroundImage {
			get {
				return UIImage.FromBundle ("calendar_images/CalendarPreviousMonth.png").StretchableImage (0, 0);
			}
		}

		public override UIImage BackgroundImage {
			get {
				return UIImage.FromBundle ( BottomRow ? "calendar_images/CalendarRowBottom.png" : "calendar_images/CalendarRow.png");
			}
		}
	}
}

