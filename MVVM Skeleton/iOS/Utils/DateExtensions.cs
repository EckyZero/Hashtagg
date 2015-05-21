using System;
using Foundation;
using Shared.VM;
using TelerikUI;

namespace iOS
{
	public static class DateExtensions
	{
		public static NSDate ToNSDate(this DateTime dateTime)
		{
			return NSDate.FromTimeIntervalSinceReferenceDate((dateTime-(new DateTime(2001,1,1,0,0,0))).TotalSeconds);
		}

		public static DateTime ToDateTime(this NSDate nsDate)
		{
			return (new DateTime(2001,1,1,0,0,0)).AddSeconds(nsDate.SecondsSinceReferenceDate);
		}

		public static TKCalendarSelectionMode ToTKCalenderSelectionMode (this CalendarSelectionMode mode)
		{
			var tkMode = TKCalendarSelectionMode.Single;

			switch(mode)
			{
				case CalendarSelectionMode.None:
					tkMode = TKCalendarSelectionMode.None;
					break;
				case CalendarSelectionMode.Single:
					tkMode = TKCalendarSelectionMode.Single;
					break;
				case CalendarSelectionMode.Multiple:
					tkMode = TKCalendarSelectionMode.Multiple;
					break;
				case CalendarSelectionMode.Range:
					tkMode = TKCalendarSelectionMode.Range;
					break;
			}
			return tkMode;
		}
	}
}

