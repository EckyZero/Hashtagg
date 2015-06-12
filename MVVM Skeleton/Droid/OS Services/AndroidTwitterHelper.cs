﻿using System;
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
	public class AndroidTwitterHelper : ITwitterHelper
	{
		#region Private Variables

		Xamarin.Social.Services.TwitterService _twitterService;
		Activity _activity;

		#endregion

		#region Methods

		public AndroidTwitterHelper ()
		{
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
				
			var account = AccountStore.Create (_activity).FindAccountsForService (Config.TWITTER_SERVICE_ID).FirstOrDefault();
			var request = _twitterService.CreateRequest (method, uri, parameters, account);
			var response = await request.GetResponseAsync();
			var result = response.GetResponseText();

			return result;
		}

		public void TwitterAuthenticationExecute (Action callback)
		{
			var intent = _twitterService.GetAuthenticateUI (_activity, (account) => 
				{
					AccountStore.Create (_activity).Save (account, Config.TWITTER_SERVICE_ID);
					if(callback != null)
					{
						callback();	
					}
				});
			_activity.StartActivity (intent);
		}

		public async Task<bool> TwitterAccountExists ()
		{
			var accounts = await _twitterService.GetAccountsAsync (_activity);
			var exists = (accounts != null) && accounts.Any();

			return exists;
		}

		internal void RegisterActivity(Activity activity)
		{
			_activity = activity;
		}
			
		#endregion
	}
}

