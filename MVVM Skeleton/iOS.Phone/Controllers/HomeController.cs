
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
using CoreGraphics;

namespace iOS.Phone
{
	public partial class HomeController : UIViewController
	{
		#region Private Variables

		private UIRefreshControl _refreshControl;
		private nfloat _lastY;

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
			NavigationController.SetNavigationBarHidden (false, true);

			NavigationController.NavigationBar.SetBackgroundImage(NavigationController.NavigationBar.BarTintColor.ToImage(new CGRect(0,0,NavigationController.View.Frame.Width, NavigationController.NavigationBar.Frame.Height + 20)), UIBarPosition.Any, UIBarMetrics.Default);
			NavigationController.NavigationBar.ShadowImage = NavigationController.NavigationBar.BarTintColor.ToImage(new CGRect(0,0, NavigationController.View.Frame.Width, 1));

			var attributes = new UITextAttributes () {
				Font = UIFont.FromName ("SanFranciscoText-Light", 13)
			};
			SortOrderSegmentedControl.SetTitleTextAttributes(attributes, UIControlState.Normal);
		}

		private void InitBindings ()
		{
			ViewModel.RequestCompleted = OnRequestCompleted;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(HomeTableController)) {

				var controller = segue.DestinationViewController as HomeTableController;

				controller.Collection = ViewModel.CardViewModels;
				controller.OnPullToRefresh = OnPullToRefresh;
				controller.HandleScrolled = OnScrolled;
				controller.HandleDraggingStarted = OnDraggingStarted;

				controller.SetEstimatedHeight (175);
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

		private void OnScrolled (UITableView tableView)
		{
			var point = tableView.PanGestureRecognizer.TranslationInView (tableView);
			var difference = _lastY - point.Y;
			var percentComplete = HeaderViewTopConstraint.Constant / -HeaderView.Frame.Height;
			nfloat? newConstant = null;

			if(HeaderViewTopConstraint.Constant - difference <= -HeaderView.Frame.Height) 
			{
				// Stop the adjustment once the header view is off the screen
				newConstant = -HeaderView.Frame.Height;
			}
			else if (HeaderViewTopConstraint.Constant - difference >= 0 && tableView.ContentOffset.Y <= 0) 
			{
				// Stop the adjustment once the header view is fully in view
				newConstant = 0;
			} 
			else
			{
				if(difference <= 0 && tableView.ContentOffset.Y <= 0)
				{
					// Only move the header view back out if we're at the top	
					newConstant = HeaderViewTopConstraint.Constant -= difference;
				} 
				else if (difference >= 0 && tableView.ContentOffset.Y >= 0)
				{
					// Move the header view out whenever we're scrolling down
					newConstant = HeaderViewTopConstraint.Constant - difference;
				}
			}

			// Adjust the constraint per the user's scroll
			if(newConstant.HasValue) {
				HeaderViewTopConstraint.Constant = newConstant.Value;	
			}

			// Track the current position
			_lastY = point.Y;
		}

		private void OnDraggingStarted (UITableView tableView)
		{
			// Set the initial reference point for scrolling animations
			var point = tableView.PanGestureRecognizer.TranslationInView (tableView);

			_lastY = point.Y;
		}
	}
}

