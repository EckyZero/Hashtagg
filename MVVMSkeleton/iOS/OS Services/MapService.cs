using System;
using Shared.Common;
using Foundation;
using UIKit;

namespace iOS
{
	public class MapService : BaseService, IMapService
	{
		private const string _baseUrl = "http://maps.apple.com/?";

		public bool GoToLocation(GeoLocation location)
		{
			try
			{
				var urlString = String.Format("{0}ll={1}",_baseUrl,location);
				var uri = new Uri(urlString);
				var url = new NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
				return GoToMap (url);
			}
			catch (Exception e)
			{
				_logger.Log (e);
				return false;
			}
		}

		public bool GoToAddress(string address, string city, string state, string country)
		{
			try
			{
				var urlString = string.Format ("{0}q={1},{2},{3},{4}", _baseUrl, address, city, state, country);
				var uri = new Uri(urlString);
				var url = new NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
				return GoToMap (url);	
			}
			catch (Exception e)
			{
				_logger.Log (e);
				return false;
			}
		}

		public bool DirectionsToAddress(string address, string city, string state, string country)
		{
			try
			{
				var urlString = string.Format ("{0}daddr={1},{2},{3},{4}", _baseUrl, address, city, state, country);
				var uri = new Uri(urlString);
				var url = new NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
				return GoToMap (url);	
			}
			catch (Exception e)
			{
				_logger.Log (e);
				return false;
			}
		}

		private bool GoToMap(NSUrl url)
		{
			try 
			{
				return UIApplication.SharedApplication.OpenUrl (url);
			}
			catch (Exception e)
			{
				_logger.Log (e);
				return false;
			}
		}
	}
}

