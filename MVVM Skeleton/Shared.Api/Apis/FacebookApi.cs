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
				{ "limit", "10" },
				{ "fields", "full_picture,created_time,id,updated_time,message,link,shares,from,likes,comments,actions,story,name,description"}
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

		#endregion
	}
}

