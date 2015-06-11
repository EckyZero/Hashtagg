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
		#region Private Properties

		ISocialService _socialService;

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
			_socialService = IocContainer.GetContainer ().Resolve<ISocialService> ();
		}

		public async Task<IList<TwitterFeedItemDto>> GetHomeFeed()
		{
			var settings = new JsonSerializerSettings ();

			settings.DateFormatString = "ddd MMM dd HH:mm:ss zzzz yyyy";
			settings.Culture = CultureInfo.InvariantCulture;

			var url = new Uri(String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_HOME_FEED));
			var parameters = new Dictionary<string, string> () {
				{ "count", "200" }
			};

			try
			{
				var response = await _socialService.TwitterRequestExecute (GET, url, parameters);
				var results = JsonConvert.DeserializeObject<IList<TwitterFeedItemDto>> (response, settings);	

				return results;
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to get tweets", e);
				Logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		#endregion
	}
}

