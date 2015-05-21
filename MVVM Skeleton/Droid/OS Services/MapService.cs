using System;
using Shared.Common;
using Android.Content;

namespace Droid
{
	public class MapService : BaseService, IMapService
	{
		public bool GoToLocation(GeoLocation location)
		{
			var geoUri = Android.Net.Uri.Parse (String.Format ("geo:{0}", location));
			return GoToMap (geoUri);
		}

		public bool GoToAddress(string address, string city, string state, string country)
		{
			var geoUri = Android.Net.Uri.Parse (String.Format ("geo:0,0?q={0},{1},{2},{3}", address, city, state, country));
			return GoToMap (geoUri);
		}
			
		public bool DirectionsToAddress(string address, string city, string state, string country)
		{
			return GoToAddress (address, city, state, country);
		}

		private bool GoToMap(Android.Net.Uri uri)
		{
			try
			{
				var mapIntent = new Intent (Intent.ActionView, uri);
				_activity.StartActivity (mapIntent);
				return true;
			}
			catch (Exception e){
				_logger.Log (e);
				return false;
			}
		}
	}
}

