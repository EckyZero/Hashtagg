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
		JsonSerializerSettings _settings;

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
			_settings = new JsonSerializerSettings ();

			_settings.DateFormatString = "ddd MMM dd HH:mm:ss zzzz yyyy";
			_settings.Culture = CultureInfo.InvariantCulture;
		}

		public async Task<List<TwitterFeedItemDto>> GetHomeFeed()
		{
			var url = new Uri(String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_HOME_FEED));
			var parameters = new Dictionary<string, string> () {
				{ "count", "3" },
				{ "include_rts", "1" },
				{ "exclude_replies", "0" },
				{ "include_entities", "1" },
				{ "trim_user", "0" },
				{ "contributor_details", "1" }
			};

			try
			{
                _logger.StartTrackTime(TimedEvent.GET_TWITTER_FEED.ToString());
				var response = await _twitterHelper.ExecuteRequest (GET, url, parameters);
				var results = JsonConvert.DeserializeObject<List<TwitterFeedItemDto>> (response, _settings);	

				return results;
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to get tweets", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.GET_TWITTER_FEED.ToString());
            }
		}

		public async Task<TwitterUserDto> GetUser (string screenName)
		{
			var url = new Uri (String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_USER));
			var parameters = new Dictionary<string, string> () {
				{ "screen_name", screenName }
			};

			try
			{
                _logger.StartTrackTime(TimedEvent.GET_TWITTER_USER.ToString());
				var response = await _twitterHelper.ExecuteRequest(GET, url, parameters);
				var results = JsonConvert.DeserializeObject<TwitterUserDto>(response, _settings);

				return results;
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to user", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.GET_TWITTER_USER.ToString());
            }
		}

		public async Task Like(string tweetId)
		{
			var url = new Uri (String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_LIKE));
			var parameters = new Dictionary<string, string> () {
				{ "id", tweetId }
			};

			try 
			{
                _logger.StartTrackTime(TimedEvent.TWITTER_LIKE.ToString());
				await _twitterHelper.ExecuteRequest(POST, url, parameters);
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to like tweet", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.TWITTER_LIKE.ToString());
            }
		}

		public async Task Unlike(string tweetId)
		{
			var url = new Uri (String.Format ("{0}{1}", BASE_URL, Routes.TWITTER_UNLIKE));
			var parameters = new Dictionary<string, string> () {
				{ "id", tweetId }
			};

			try 
			{
                _logger.StartTrackTime(TimedEvent.TWITTER_UNLIKE.ToString());
				await _twitterHelper.ExecuteRequest(POST, url, parameters);
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to unlike tweet", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.TWITTER_UNLIKE.ToString());
            }
		}

		public async Task Comment (string tweetId, string message)
		{
			var url = new Uri(String.Format("{0}{1}", BASE_URL, Routes.TWITTER_POST));
			var parameters = new Dictionary<string, string> () {
				{ "in_reply_to_status_id", tweetId },
				{ "status", message }
			};

			try
			{
				// TODO: return the id so we can track this
                _logger.StartTrackTime(TimedEvent.TWITTER_COMMENT.ToString());
				await _twitterHelper.ExecuteRequest(POST, url, parameters);	
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to comment on twitter post", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.TWITTER_COMMENT.ToString());
            }
		}

		public async Task DeleteTweet (string tweetId)
		{
			var url = new Uri(String.Format("{0}/{1}/{2}.json", BASE_URL, Routes.TWITTER_DELETE_POST, tweetId));

			try
			{
                _logger.StartTrackTime(TimedEvent.DELETE_TWITTER_POST.ToString());
				await _twitterHelper.ExecuteRequest(POST, url);	
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to delete a comment on twitter post", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.DELETE_TWITTER_POST.ToString());
            }
		}

		public async Task Post (string message)
		{
			// TODO: May need to change this endpoints if images or links need to be included
			var url = new Uri(String.Format("{0}{1}", BASE_URL, Routes.TWITTER_POST));
			var parameters = new Dictionary<string, string> () {
				{ "status", message }
			};

			try
			{
				// TODO: Determine if we care about if this fails
				// Do we store locally and try to resync later?
				// Do we just alert the user that twitter failed?
                _logger.StartTrackTime(TimedEvent.TWITTER_POST.ToString());
				await _twitterHelper.ExecuteRequest(POST, url, parameters);	
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to post a tweet", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
            finally
            {
                _logger.StopTrackTime(TimedEvent.TWITTER_POST.ToString());
            }
		}


		#endregion
	}
}

