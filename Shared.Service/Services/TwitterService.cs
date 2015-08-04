using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared.Common;
using Shared.Api;
using Microsoft.Practices.Unity;
using AutoMapper;
using System.Collections.ObjectModel;
using System.Linq;

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
			var models = new ObservableCollection<Tweet> ();

			try
			{
				if(_connectivityService.IsConnected)
				{
					var dtos = await _twitterApi.GetHomeFeed ();

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
			catch (BaseException e)
			{
				return new ServiceResponse<ObservableCollection<Tweet>> (models, ServiceResponseType.ERROR);
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error getting tweets", exception), LogType.ERROR);
				return new ServiceResponse<ObservableCollection<Tweet>> (models, ServiceResponseType.ERROR);
			}
		}

		public async Task<ServiceResponse<TwitterUser>> GetUser (string screenName)
		{
			var user = new TwitterUser ();

			try
			{
				if(_connectivityService.IsConnected)
				{
					var dto = await _twitterApi.GetUser(screenName);
					user = Mapper.Map<TwitterUser> (dto);

					return new ServiceResponse<TwitterUser>(user, ServiceResponseType.SUCCESS);
				}
				else
				{
					return new ServiceResponse<TwitterUser>(user,ServiceResponseType.NO_CONNECTION);
				}
			}
			catch (BaseException e)
			{
				return new ServiceResponse<TwitterUser>(user,ServiceResponseType.ERROR);
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error getting Twitter user", exception), LogType.ERROR);
				return new ServiceResponse<TwitterUser>(user,ServiceResponseType.ERROR);
			}
		}

		public async Task<ServiceResponseType> Like (string tweetId)
		{
			try
			{
				if(_connectivityService.IsConnected)
				{
					await _twitterApi.Like(tweetId);
					return ServiceResponseType.SUCCESS;
				}
				else
				{
					return ServiceResponseType.NO_CONNECTION;
				}
			}
			catch (BaseException e)
			{
				return ServiceResponseType.ERROR;
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error getting tweets", exception), LogType.ERROR);
				return ServiceResponseType.ERROR;
			}
		}

		public async Task<ServiceResponseType> Unlike (string tweetId)
		{
			try
			{
				if(_connectivityService.IsConnected)
				{
					await _twitterApi.Unlike(tweetId);
					return ServiceResponseType.SUCCESS;
				}
				else
				{
					return ServiceResponseType.NO_CONNECTION;
				}
			}
			catch (BaseException e)
			{
				return ServiceResponseType.ERROR;
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error unliking facebook post", exception), LogType.ERROR);
				return ServiceResponseType.ERROR;
			}
		}

		public async Task<ServiceResponseType> Comment (string tweetId, string message)
		{
			try
			{
				if(_connectivityService.IsConnected)
				{
					await _twitterApi.Comment(tweetId, message);
					return ServiceResponseType.SUCCESS;
				}
				else
				{
					return ServiceResponseType.NO_CONNECTION;
				}
			}
			catch (BaseException e)
			{
				return ServiceResponseType.ERROR;
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error commenting on tweet", exception), LogType.ERROR);
				return ServiceResponseType.ERROR;
			}
		}

		public async Task<ServiceResponseType> DeleteTweet (string tweetId)
		{
			try
			{
				if(_connectivityService.IsConnected)
				{
					await _twitterApi.DeleteTweet(tweetId);
					return ServiceResponseType.SUCCESS;
				}
				else
				{
					return ServiceResponseType.NO_CONNECTION;
				}
			}
			catch (BaseException e)
			{
				return ServiceResponseType.ERROR;
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error deleting a  tweet", exception), LogType.ERROR);
				return ServiceResponseType.ERROR;
			}
		}

		public async Task<ServiceResponseType> Post (string message)
		{
			try
			{
				if(_connectivityService.IsConnected)
				{
					await _twitterApi.Post(message);
					return ServiceResponseType.SUCCESS;
				}
				else
				{
					return ServiceResponseType.NO_CONNECTION;
				}
			}
			catch (BaseException e)
			{
				return ServiceResponseType.ERROR;
			}
			catch (Exception exception)
			{
				_logger.Log (new ServiceException ("Error posting tweet", exception), LogType.ERROR);
				return ServiceResponseType.ERROR;
			}
		}

		#endregion
	}
}

