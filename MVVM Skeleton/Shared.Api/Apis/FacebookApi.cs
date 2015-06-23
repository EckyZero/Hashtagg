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
				{ "limit", "10" }
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

		#endregion
	}
}

