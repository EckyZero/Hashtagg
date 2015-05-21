using System;
using System.Collections.Generic;

namespace Shared.Common
{
	public class ProviderLookupFilter:ILookupFilter
	{
		public static Dictionary<FilterKey, string> QueryString = new Dictionary<FilterKey, string>
		{
			{FilterKey.Procedure, "ProcedureId" },
			{FilterKey.Specialty, "SpecialtyId" }
		};
		
		public FilterKey Key { get; set; }
		public string Value { get; set; }

		public ProviderLookupFilter (FilterKey key, string value)
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

