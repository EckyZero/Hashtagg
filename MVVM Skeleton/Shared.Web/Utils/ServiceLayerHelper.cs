using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Shared.Web
{
	public static class ServiceLayerHelper
	{
		public static string ToWebQueryString(this IDictionary<string, string> dict)
		{
			if (dict == null) return string.Empty;

			var buffer = new StringBuilder();

			int count = 0;
			bool end = false;

			foreach (string key in dict.Keys)
			{
				if (dict.Keys.Last<string>() == key)
					buffer.AppendFormat("{0}={1}", key, dict[key]);
				else
					buffer.AppendFormat("{0}={1}&", key, dict[key]);

				count++;
			}

			return buffer.ToString();
		}
	}
}

