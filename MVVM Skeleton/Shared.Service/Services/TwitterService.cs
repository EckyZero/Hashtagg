﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Common;
using Shared.Api;
using Microsoft.Practices.Unity;
using AutoMapper;
using System.Collections.ObjectModel;

namespace Shared.Service
{
	public class TwitterService : BaseService, ITwitterService
	{
		#region Private Variables

		ITwitterApi _twitterApi;

		#endregion

		#region Methods

		public TwitterService ()
		{
			_twitterApi = IocContainer.GetContainer ().Resolve<ITwitterApi> ();
		}

		public async Task<ServiceResponse<ObservableCollection<Tweet>>> GetHomeFeed ()
		{
			var dtos = new List<TwitterFeedItemDto> ();
			var models = new ObservableCollection<Tweet> ();

			try
			{
				if(ConnectivityService.IsConnected)
				{
					dtos = await _twitterApi.GetHomeFeed () as List<TwitterFeedItemDto>;

					foreach(TwitterFeedItemDto dto in dtos)
					{
						var model = Mapper.Map<Tweet> (dto);
						models.Add (model);
					}

					return new ServiceResponse<ObservableCollection<Tweet>>(models,ServiceResponseType.SUCCESS);
				}
				else
				{
					return new ServiceResponse<ObservableCollection<Tweet>>(models,ServiceResponseType.NO_CONNECTION);
				}
			}
			catch (BaseException exception)
			{
				return new ServiceResponse<ObservableCollection<Tweet>> (models, ServiceResponseType.ERROR);
			}
			catch (Exception exception)
			{
				Logger.Log (new ServiceException ("Error getting tweets", exception), LogType.ERROR);
				return new ServiceResponse<ObservableCollection<Tweet>> (models, ServiceResponseType.ERROR);
			}
		}

		#endregion
	}
}
