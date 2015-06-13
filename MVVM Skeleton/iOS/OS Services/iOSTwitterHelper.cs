using System;
using Shared.Api;
using Xamarin.Social.Services;
using Shared.Common;
using Xamarin.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;
using System.Linq;
using Shared.Service;

namespace iOS
{
	public class iOSTwitterHelper : ITwitterHelper
	{
		#region Private Variables

		Xamarin.Social.Services.TwitterService _twitterService;

		#endregion

		#region Methods

		public iOSTwitterHelper ()
		{
			_twitterService = new Xamarin.Social.Services.TwitterService() { 
				ConsumerKey = Config.TWITTER_CONSUMER_KEY, 
				ConsumerSecret = Config.TWITTER_CONSUMER_SECRET,
				CallbackUrl = new Uri(Config.TWITTER_CALLBACK_URL)
			};	
		}

		public async Task<string> ExecuteRequest (string method, Uri uri, IDictionary<string, string> parameters = null)
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

		public void Authenticate (Action callback)
		{
			var presentingController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			var controller = _twitterService.GetAuthenticateUI ( (account) => 
				{
					if(account != null)
					{
						AccountStore.Create ().Save (account, Config.TWITTER_SERVICE_ID);	
					}
					presentingController.DismissViewController(true, () => 
						{
							if(callback != null) 
							{
								callback();
							}	
						});
				});
			presentingController.PresentViewController (controller, true, null);
		}

		public async Task<bool> AccountExists ()
		{
			var accounts = await _twitterService.GetAccountsAsync ();
			var exists = (accounts != null) && accounts.Any();

			return exists;
		}

		#endregion
	}
}

