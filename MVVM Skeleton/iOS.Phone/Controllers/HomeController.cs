
using System;

using Foundation;
using UIKit;
using Xamarin.Social.Services;
using Xamarin.Auth;
using Social;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace CompassMobile.iOS.Phone
{
	public partial class HomeController : UIViewController
	{
		HomeViewModel _viewModel = new HomeViewModel();

		public HomeController () : base ("HomeController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			
			// 1. Create the service
//			var twitter = new TwitterService() { 
//				ConsumerKey = "2RHZEbx22DrA1WXdOjs73bY1f", 
//				ConsumerSecret = "O6L3O8Hvu6EplPptb3yMGFSKylyxlKaLaFiELTpKgIitGsTN9P",
//				CallbackUrl = new Uri("https://components.xamarin.com/view/xamarin.auth")
//			};

//			_twitterService = new TwitterService() { 
//				ConsumerKey = "2RHZEbx22DrA1WXdOjs73bY1f", 
//				ConsumerSecret = "O6L3O8Hvu6EplPptb3yMGFSKylyxlKaLaFiELTpKgIitGsTN9P",
//				CallbackUrl = new Uri("http://localhost.com")
//			};


//			twitter.AuthorizeUrl = new Uri ("https://api.twitter.com/oauth/authorize");
//			twitter.AccessTokenUrl = new Uri ("https://api.twitter.com/oauth/access_token");
//			twitter.RequestTokenUrl = new Uri ("https://api.twitter.com/oauth/request_token");


			TwitterButton.SetCommand ("TouchUpInside", _viewModel.RefreshCommand);
//			TwitterButton.TouchUpInside += async (sender, e) => {
//				await _viewModel
//			};
		}

	

//		private async void GetTwitter(Account account)
//		{
//			AccountStore.Create ().Save (account, "Twitter");
//			var accounts = await _twitterService.GetAccountsAsync ();
//
//			var parameters = new NSDictionary ();
//			var url = new NSUrl ("https://api.twitter.com/1.1/statuses/home_timeline.json");
//			var request = _twitterService.CreateRequest ("GET", url, account);
////			_twitterService.Create
//
//			var response = await request.GetResponseAsync ();
//			var result = response.GetResponseText ();
//		}

		private void Share(Xamarin.Social.ShareResult result)
		{
			System.Diagnostics.Debug.WriteLine (result);
		}
	}
}

