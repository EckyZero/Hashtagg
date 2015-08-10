using System;
using System.Threading.Tasks;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;

namespace Shared.Api
{
	public class FacebookApi : ApiClient, IFacebookApi
	{
		#region Private Variables

		IFacebookHelper _facebookHelper;

		#endregion

		#region Member Properties

		protected override string BASE_URL 
		{
			get { return Routes.FACEBOOK_BASE; }
		}

		#endregion

		#region Methods

		public FacebookApi ()
		{
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		public async Task<FacebookResponseDto> GetHomeFeed ()
		{
			var url = new Uri (String.Format ("{0}{1}", BASE_URL, Routes.FACEBOOK_HOME_FEED));
			var parameters = new Dictionary<string, string> () {
				{ "limit", "100" },
				{ "fields", "full_picture,created_time,id,updated_time,message,link,shares,from,likes,comments,actions,story,name,description,type,source"}
			};

			try
			{
				var response = await _facebookHelper.ExecuteRequest(GET, url, parameters);
				var results = JsonConvert.DeserializeObject<FacebookResponseDto>(response);

				return results;
			}
			catch (Exception e)
			{
				var exception = new ApiException ("Failed to get facebook posts", e);
				_logger.Log (exception, LogType.ERROR);
				throw exception;
			}
		}

		public async Task<FacebookToFromDto> GetUser ()
		{
			var url = new Uri (String.Format ("{0}{1}", BASE_URL, Routes.FACEBOOK_USER));

			try
			{
				var response = await _facebookHelper.ExecuteRequest(GET, url);
				var result = JsonConvert.DeserializeObject<FacebookToFromDto>(response);

				return result;
			}
			catch (Exception e)
			{
				var exception = new ApiException ("Failed to get facebook user", e);
				_logger.Log (exception, LogType.ERROR);
				throw exception;
			}
		}

		public async Task Like(string postId)
		{
			var url = new Uri (String.Format ("{0}/{1}{2}", BASE_URL, postId, Routes.FACEBOOK_LIKE));

			try 
			{
				await _facebookHelper.ExecuteRequest(POST, url, null);
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to like facebook post", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		public async Task Unlike(string postId)
		{
			var url = new Uri (String.Format ("{0}/{1}{2}", BASE_URL, postId, Routes.FACEBOOK_LIKE));

			try 
			{
				await _facebookHelper.ExecuteRequest(DELETE, url, null);
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to unlike facebook post", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		public async Task Comment (string postId, string message)
		{
			var url = new Uri(String.Format("{0}/{1}{2}", BASE_URL, postId, Routes.FACEBOOK_COMMENT));
			var parameters = new Dictionary<string, string> () {
				{ "message", message }
			};

			try
			{
				// TODO: return id so we can track the post locally
				await _facebookHelper.ExecuteRequest(POST, url, parameters);	
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to comment on facebook post", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		public async Task DeleteComment (string commentId)
		{
			var url = new Uri(String.Format("{0}/{1}{2}", BASE_URL, Routes.FACEBOOK_DELETE_COMMENT, commentId));

			try
			{
				await _facebookHelper.ExecuteRequest(DELETE, url);	
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to delete a comment on facebook post", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		public async Task Post (string userId, string message)
		{
			// TODO: May need to change this endpoints if images or links need to be included
			var url = new Uri(String.Format("{0}/{1}{2}", BASE_URL, userId, Routes.FACEBOOK_POST));
			var parameters = new Dictionary<string, string> () {
//				{ "link", link },
//				{ "picture", picture },
				{ "message", message }
			};

			try
			{
				// TODO: Determine if we care about if this fails
				// Do we store locally and try to resync later?
				// Do we just alert the user that twitter failed?
				await _facebookHelper.ExecuteRequest(POST, url, parameters);	
			}
			catch (Exception e)
			{
				var exception = new ApiException("Failed to post a tweet", e);
				_logger.Log(exception, LogType.ERROR);
				throw exception;
			}
		}

		#endregion
	}
}

