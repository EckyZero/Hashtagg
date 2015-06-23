using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Common;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using System.Globalization;

namespace Shared.Api
{
	public class TwitterApi : ApiClient, ITwitterApi
	{
		#region Private Variables

		ITwitterHelper _twitterHelper;

		#endregion

		#region Member Properties

		protected override string BASE_URL 
		{
			get { return Routes.TWITTER_BASE; }
		}

		#endregion

		#region Methods

		public TwitterApi ()
		{
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
		}

		public async Task<List<TwitterFeedItemDto>> GetHomeFeed()
		{
			var settings = new JsonSerializerSettings ();

			settings.DateFormatString = "ddd MMM dd HH:mm:ss zzzz yyyy";
			settings.Culture = CultureInfo.InvariantCulture;

			var url = new Uri(String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_HOME_FEED));
			var parameters = new Dictionary<string, string> () {
				{ "count", "10" }
			};

			try
			{
				var response = await _twitterHelper.ExecuteRequest (GET, url, parameters);
				var results = JsonConvert.DeserializeObject<List<TwitterFeedItemDto>> (response, settings);	

				return results;
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to get tweets", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		#endregion
	}
}

