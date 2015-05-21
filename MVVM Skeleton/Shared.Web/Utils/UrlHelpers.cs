using System;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace CompassMobile.Shared.SAL
{
	// From http://ole.michelsen.dk/blog/serialize-object-into-a-query-string-with-reflection.html
	// with some changes to support Xamarin
	public static class UrlHelpers
	{
		public static string GenerateQueryString(object request, string separator = ",")
		{
			if (request == null)
				throw new ArgumentNullException("request");

			// Get all properties on the object
			var properties = request.GetType().GetRuntimeProperties()
				.Where(x => x.CanRead)
				.Where(x => x.GetValue(request, null) != null)
				.ToDictionary(x => x.Name, x => x.GetValue(request, null));

			// Get names for all IEnumerable properties (excl. string)
			var propertyNames = properties
				.Where(x => !(x.Value is string) && x.Value is IEnumerable)
				.Select(x => x.Key)
				.ToList();

			// Concat all IEnumerable properties into a comma separated string
			foreach (var key in propertyNames)
			{
				var valueType = properties[key].GetType();
				var valueElemType = valueType.IsConstructedGenericType
					? valueType.GenericTypeArguments[0]
					: valueType.GetElementType();
				if (!(valueElemType.IsByRef) || valueElemType == typeof (string))
				{
					var enumerable = properties[key] as IEnumerable;
					properties[key] = string.Join(separator, enumerable.Cast<object>());
				}
			}

			// Concat all key/value pairs into a string separated by ampersand
			var query = string.Join("&", properties
				.Select(x => string.Concat(
					Uri.EscapeDataString(x.Key), "=",
					Uri.EscapeDataString(x.Value.ToString()))));
			return query;
		}
	}
}

