using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Common;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;

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
			var url = new Uri(String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_HOME_FEED));
			var response = await _socialService.TwitterRequestExecute (GET, url, null);
			var results = JsonConvert.DeserializeObject<IList<TwitterFeedItemDto>> (response);

			return results;
		}

		#endregion
	}
}

