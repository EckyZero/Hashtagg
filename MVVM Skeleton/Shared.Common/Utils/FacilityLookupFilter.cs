using System;
using System.Collections.Generic;

namespace Shared.Common
{
	public class FacilityLookupFilter:ILookupFilter
	{
		public static Dictionary<FilterKey, string> QueryString = new Dictionary<FilterKey, string>
		{
			{FilterKey.FacilityType, "FacilityType" }
		};

		public FilterKey Key { get; set; }
		public string Value { get; set; }

		public FacilityLookupFilter (FilterKey key, string value)
		{
			Key = key;
			Value = value;
		}

		public string LookupFilterKey {
			get {
				return QueryString [Key];	
			}
		}

		public string LookupFilterValue {
			get {
				return Value;
			}
		}

		public override int GetHashCode()
		{
			return (int)Key;
		}

		public bool Equals (ILookupFilter other)
		{
			return other.Key == Key;
		}
	}
}

