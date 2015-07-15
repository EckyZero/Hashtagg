
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
using System.Threading.Tasks;
using System.Collections.Generic;
using SDWebImage;

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

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();
	
			InitUI ();
			await InitBindings ();

			ViewModel.HeaderImagesCommand.Execute (null);
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

			NameLabel.Text = ViewModel.Title;
		}

		private async Task InitBindings ()
		{
			ViewModel.RequestCompleted = OnRequestCompleted;
			ViewModel.RequestHeaderImages = OnRequestHeaderImages;

			if(!ViewModel.IsLoaded) {
				await ViewModel.DidLoad();
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(PSObservableTableController)) {

				var controller = segue.DestinationViewController as PSObservableTableController;

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

		private void OnRequestHeaderImages (List<string> images)
		{
			var defaultImage = UIImage.FromFile (ViewModel.DefaultAccountImageName);

			if(images.Count > 0) 
			{
				var prevImageView = AccountImageView;
				var prevTrailingConstraint = AccountImageViewTrailingConstraint;

				prevImageView.Layer.ShadowColor = UIColor.Black.CGColor;
				prevImageView.Layer.ShadowOffset = new CGSize (0, 0);
				prevImageView.Layer.ShadowRadius = 10;
				prevImageView.Layer.CornerRadius = prevImageView.Frame.Height/2;
				prevImageView.SetImage (new NSUrl(images [0]), defaultImage);	

				images.RemoveAt (0);

				foreach (string imageUrl in images)
				{
					var imageView = new UIImageView ();

					imageView.SetImage (new NSUrl (imageUrl), defaultImage);
					imageView.Layer.CornerRadius = prevImageView.Layer.CornerRadius;
					imageView.ClipsToBounds = prevImageView.ClipsToBounds;
					imageView.Layer.ShadowColor = prevImageView.Layer.ShadowColor;
					imageView.Layer.ShadowOffset = prevImageView.Layer.ShadowOffset;
					imageView.Layer.ShadowRadius = prevImageView.Layer.ShadowRadius;
					imageView.TranslatesAutoresizingMaskIntoConstraints = false;

					var heightConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, prevImageView.Frame.Height);
					var widthConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, prevImageView.Frame.Width);
					var leadingConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, prevImageView, NSLayoutAttribute.Trailing, 1, -prevImageView.Frame.Width/2);
					var centerYConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, AccountsView, NSLayoutAttribute.CenterY, 1, 0);
					var trailingConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, AccountsView, NSLayoutAttribute.Trailing, 1, 0);	

					AccountsView.AddSubview (imageView);
					AccountsView.AddConstraint (heightConstraint);
					AccountsView.AddConstraint (widthConstraint);
					AccountsView.AddConstraint (leadingConstraint);
					AccountsView.AddConstraint (centerYConstraint);
					AccountsView.AddConstraint (trailingConstraint);

					AccountsView.RemoveConstraint (prevTrailingConstraint);

					prevImageView = imageView;
					prevTrailingConstraint = trailingConstraint;
				}
				AccountsView.LayoutIfNeeded ();
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

