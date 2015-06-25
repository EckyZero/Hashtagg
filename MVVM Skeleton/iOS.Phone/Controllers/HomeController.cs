
using System;

using Foundation;
using UIKit;
using Xamarin.Social.Services;
using Xamarin.Auth;
using Social;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using System.Linq;
using Shared.Common;

namespace iOS.Phone
{
	public partial class HomeController : UIViewController
	{
		#region Member Properties

		public HomeViewModel ViewModel { get; set; }

		#endregion

		public HomeController (IntPtr handle) : base (handle)
		{
			if(ViewModel == null) {
				ViewModel = new HomeViewModel ();
			}
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

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(HomeTableController)) {

				var controller = segue.DestinationViewController as HomeTableController;
				controller.Collection = ViewModel.CardViewModels;
			}
		}
	}
}

