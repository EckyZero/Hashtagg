using System;
using Shared.Common;
using System.Threading.Tasks;
using Xamarin.Auth;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Microsoft.Practices.Unity;

namespace Droid
{
	public class AndroidSocialService : ISocialService
	{
		#region Private Variables

		Xamarin.Social.Services.TwitterService _twitterService;
		ExtendedNavigationService _navigationService;

		#endregion

		#region Methods

		public AndroidSocialService ()
		{
			_navigationService = IocContainer.GetContainer().Resolve<IExtendedNavigationService>() as ExtendedNavigationService;
			_twitterService = new Xamarin.Social.Services.TwitterService() { 
				ConsumerKey = Config.TWITTER_CONSUMER_KEY, 
				ConsumerSecret = Config.TWITTER_CONSUMER_SECRET,
				CallbackUrl = new Uri(Config.TWITTER_CALLBACK_URL)
			};	
		}

		public async Task<string> TwitterRequestExecute (string method, Uri uri, System.Collections.Generic.IDictionary<string, string> parameters)
		{
			if(parameters == null)
			{
				parameters = new Dictionary<string,string> ();
			}

			var activity = _navigationService.Activity as Activity;
			var account = AccountStore.Create (activity).FindAccountsForService (Config.TWITTER_SERVICE_ID).FirstOrDefault();
			var request = _twitterService.CreateRequest (method, uri, parameters, account);
			var response = await request.GetResponseAsync();
			var result = response.GetResponseText();

			return result;
		}

		public void TwitterAuthenticationExecute (Action callback)
		{
			var activity = _navigationService.Activity as Activity;
			var intent = _twitterService.GetAuthenticateUI (activity, (account) => 
				{
					AccountStore.Create (activity).Save (account, Config.TWITTER_SERVICE_ID);
					_navigationService.GoBack();
				});
			activity.StartActivity (intent);
		}

		public async Task<bool> TwitterAccountExists ()
		{
			var activity = _navigationService.Activity as Activity;
			var accounts = await _twitterService.GetAccountsAsync (activity);
			var exists = (accounts != null) && accounts.Any();

			return exists;
		}
			
		#endregion
	}
}

