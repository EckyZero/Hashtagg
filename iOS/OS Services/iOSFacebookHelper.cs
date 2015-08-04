using System;
using Shared.Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Auth;
using System.Linq;
using UIKit;

namespace iOS
{
	public class iOSFacebookHelper : IFacebookHelper
	{
		#region Private Variables

		Xamarin.Social.Services.FacebookService _facebookService;

		#endregion

		#region Methods

		public iOSFacebookHelper ()
		{
			_facebookService = new Xamarin.Social.Services.FacebookService () {
				ClientId = Config.FACEBOOK_CLIENT_ID,
				ClientSecret = Config.FACEBOOK_SECRET,
				RedirectUrl = new Uri(Config.FACEBOOK_REDIRECT_URL),
				Scope = Config.FACEBOOK_SCOPE
			};
		}

		public async Task<string> ExecuteRequest (string method, Uri uri, IDictionary<string,string> parameters = null)
		{
			if(parameters == null)
			{
				parameters = new Dictionary<string,string> ();
			}
			var account = AccountStore.Create ().FindAccountsForService (Config.FACEBOOK_SERVICE_ID).FirstOrDefault();
			var request = _facebookService.CreateRequest (method, uri, parameters, account);
			var response = await request.GetResponseAsync ();
			var result = response.GetResponseText ();

			return result;
		}

		public void Authenticate (Action callback)
		{
			var presentingController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			var controller = _facebookService.GetAuthenticateUI ( (account) => 
				{
					if(account != null)
					{
						AccountStore.Create ().Save (account, Config.FACEBOOK_SERVICE_ID);	
						if(callback != null) 
						{
							callback();
						}	
					}
					presentingController.DismissViewController(true, null);
				});
			
			presentingController.PresentViewController (controller, true, null);
		}

		public async Task<bool> AccountExists ()
		{
			var accounts = await _facebookService.GetAccountsAsync ();
			var exists = (accounts != null) && accounts.Any();

			return exists;
		}

		public void DeleteAccount ()
		{
			var store = AccountStore.Create ();
			var account = store.FindAccountsForService (Config.FACEBOOK_SERVICE_ID).FirstOrDefault();

			store.Delete (account, Config.FACEBOOK_SERVICE_ID);
		}

		public SocialAccount GetAccount()
		{
			var store = AccountStore.Create ();
			var account = store.FindAccountsForService (Config.FACEBOOK_SERVICE_ID).FirstOrDefault();
			SocialAccount socialAccount = null;

			if (account != null) {
				socialAccount = new SocialAccount (account.Username, account.Properties, account.Cookies);	
			}
			return socialAccount;
		}

		public void Synchronize (SocialAccount socialAccount)
		{
			var store = AccountStore.Create ();
			var account = store.FindAccountsForService (Config.FACEBOOK_SERVICE_ID).FirstOrDefault();

			// merge on unique keys
			if(account != null && socialAccount != null) {
				foreach (string key in socialAccount.Properties.Keys) {
					if(!account.Properties.ContainsKey(key)) {
						account.Properties.Add (key, socialAccount.Properties [key]);
					}
				}
			}
			store.Save (account, Config.FACEBOOK_SERVICE_ID);
		}

		#endregion
	}
}

