using System;
using Foundation;

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
	}
}

