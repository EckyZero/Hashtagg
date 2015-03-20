using System;

namespace Shared.Common
{
	public static class DateTimeExtensions
	{
		public static string ToCompassDate(this DateTime dateTime)
		{
			try{

				if(dateTime == DateTime.MinValue){
					return string.Empty;
				}

				var format = "ddd, MMM. d, yyyy";
				return dateTime.ToString (format);
			}
			catch {
				return dateTime.ToString ();
			}
		}

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
	}
}

