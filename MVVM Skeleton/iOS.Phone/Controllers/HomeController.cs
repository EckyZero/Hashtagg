
using System;

using Foundation;
using UIKit;
using Xamarin.Social.Services;
using Xamarin.Auth;
using Social;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
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
	
			InitUI ();
			InitBindings ();
		}

		private void InitUI ()
		{
			
		}

		private void InitBindings ()
		{
//			RefreshButton.SetCommand ("TouchUpInside", _viewModel.RefreshCommand);
//			TwitterButton.SetCommand ("TouchUpInside", _viewModel.TwitterCommand);
//			FacebookButton.SetCommand ("TouchUpInside", _viewModel.FacebookCommand);
		}
	}
}

