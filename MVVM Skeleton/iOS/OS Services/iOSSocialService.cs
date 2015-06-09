using System;
using Shared.Api;
using Xamarin.Social.Services;
using Shared.Common;
using Xamarin.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using System.Linq;

namespace iOS
{
	public class iOSSocialService : ISocialService
	{
		#region Private Variables

		TwitterService _twitterService;

		#endregion

		#region Methods

		public iOSSocialService ()
		{
			_twitterService = new TwitterService() { 
				ConsumerKey = Config.TWITTER_CONSUMER_KEY, 
				ConsumerSecret = Config.TWITTER_CONSUMER_SECRET,
				CallbackUrl = new Uri(Config.TWITTER_CALLBACK_URL)
			};	
		}

		public async Task<string> TwitterRequestExecute (string method, Uri uri, IDictionary<string, string> parameters)
		{
			if(parameters == null)
			{
				parameters = new Dictionary<string,string> ();
			}

			var account = AccountStore.Create ().FindAccountsForService (Config.TWITTER_SERVICE_ID).FirstOrDefault();
			var request = _twitterService.CreateRequest (method, uri, parameters, account);

			var response = await request.GetResponseAsync();
			var result = response.GetResponseText();

			return result;
		}

		public void TwitterAuthenticationUI (UIViewController presentingController, Action callback)
		{
			var controller = _twitterService.GetAuthenticateUI ( (account) => {
				AccountStore.Create ().Save (account, Config.TWITTER_SERVICE_ID);
				if(callback != null) {
					callback();
				}
			});
			presentingController.PresentViewController (controller, true, null);
		}

		#endregion
	}
}

