using System;

namespace Shared.Common
{
	public static class DateTimeExtensions
	{
	    public static long ToEPOCH(this DateTime dateTime)
	    {
            TimeSpan t = dateTime - new DateTime(1970, 1, 1);
            long miliecondsSinceEpoch = (long)t.TotalMilliseconds;
	        return miliecondsSinceEpoch;
	    }

	    public static DateTime ToDateTime(this long miliseconds)
	    {
            DateTime start = new DateTime(1970, 1, 1);
            DateTime dateFromEpoch = start + TimeSpan.FromMilliseconds(miliseconds);
            return dateFromEpoch;
	    }

		public static string ToRelativeString (this DateTime dateTime)
		{
			const int SECOND = 1;
			const int MINUTE = 60 * SECOND;
			const int HOUR = 60 * MINUTE;
			const int DAY = 24 * HOUR;
			const int MONTH = 30 * DAY;

			var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
			double delta = Math.Abs(ts.TotalSeconds);
			var value = "";

			if (delta < 1 * MINUTE)
			{
				value = ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
			}
			else if (delta < 45 * MINUTE)
			{
				value = ts.Minutes + " min";
			}
			else if (delta < 120 * MINUTE)
			{
				value = "1 hr";
			}
			else if (delta < 24 * HOUR)
			{
				value = ts.Hours + " hrs";
			}
			else if (delta < 48 * HOUR)
			{
				value = "yesterday";
			}
			else if (delta < 30 * DAY)
			{
				value = dateTime.ToString("d");
			}
			return value;
		}
	}
}

