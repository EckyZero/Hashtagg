using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Common;
using Shared.Api;
using Microsoft.Practices.Unity;
using AutoMapper;

namespace Shared.Service
{
	public class TwitterService : ITwitterService
	{
		#region Private Variables

		ITwitterApi _twitterApi;

		#endregion

		#region Methods

		public TwitterService ()
		{
			_twitterApi = IocContainer.GetContainer ().Resolve<ITwitterApi> ();
		}

		public async Task<IList<TwitterFeedItem>> GetHomeFeed ()
		{
			var dtos = await _twitterApi.GetHomeFeed ();
			var models = new List<TwitterFeedItem> ();

			foreach(TwitterFeedItemDto dto in dtos)
			{
				var model = Mapper.Map<TwitterFeedItem> (dto);
				models.Add (model);
			}

			return models;
		}

		#endregion
	}
}

