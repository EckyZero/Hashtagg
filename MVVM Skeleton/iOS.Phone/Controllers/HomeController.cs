
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
		#region Private Variables

		private UIRefreshControl _refreshControl;

		#endregion

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
			ViewModel.RequestCompleted = OnRequestCompleted;

			FacebookButton.SetCommand ("TouchUpInside", ViewModel.FacebookCommand);
			TwitterButton.SetCommand ("TouchUpInside", ViewModel.TwitterCommand);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(HomeTableController)) {

				var controller = segue.DestinationViewController as HomeTableController;

				controller.Collection = ViewModel.CardViewModels;
				controller.OnPullToRefresh = OnPullToRefresh;
			}
		}

		private void OnPullToRefresh(UIRefreshControl refreshControl)
		{
			_refreshControl = refreshControl;
			ViewModel.RefreshCommand.Execute (null);
		}

		private void OnRequestCompleted ()
		{
			if(_refreshControl != null) {
				_refreshControl.EndRefreshing ();
			}
		}
	}
}

