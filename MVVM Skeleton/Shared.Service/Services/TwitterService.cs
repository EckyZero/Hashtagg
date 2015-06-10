using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Common;
using Shared.Api;
using Microsoft.Practices.Unity;
using AutoMapper;
using System.Collections.ObjectModel;

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

		public async Task<ObservableCollection<Tweet>> GetHomeFeed ()
		{
			var dtos = await _twitterApi.GetHomeFeed ();
			var models = new ObservableCollection<Tweet> ();

			foreach(TwitterFeedItemDto dto in dtos)
			{
				var model = Mapper.Map<Tweet> (dto);
				models.Add (model);
			}

			return models;
		}

		#endregion
	}
}

