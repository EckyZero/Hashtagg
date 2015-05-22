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
	}
}

