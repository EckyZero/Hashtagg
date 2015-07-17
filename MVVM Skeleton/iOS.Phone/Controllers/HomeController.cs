﻿
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
using CoreAnimation;
using MediaPlayer;

namespace iOS.Phone
{
	public partial class HomeController : UIViewController
	{
		#region Private Variables

		private UIRefreshControl _refreshControl;
		private nfloat _lastY;
		private UIViewFullscreen _fullScreenView;
		private MPMoviePlayerViewController _movieController;
		private PSObservableTableController _tableController;

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
			ViewModel.RequestPhotoViewer = OnRequestPhotoViewer;
			ViewModel.RequestMovieViewer = OnRequestMovieViewer;

			if(!ViewModel.IsLoaded) {
				await ViewModel.DidLoad();
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(PSObservableTableController)) {

				_tableController = segue.DestinationViewController as PSObservableTableController;

				_tableController.Collection = ViewModel.CardViewModels;
				_tableController.OnPullToRefresh = OnPullToRefresh;
				_tableController.HandleScrolled = OnScrolled;
				_tableController.HandleDraggingStarted = OnDraggingStarted;

				_tableController.SetEstimatedHeight (175);
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
				var rect = CGRect.FromLTRB (prevImageView.Frame.X + 2, prevImageView.Frame.Y + 2, prevImageView.Frame.Width - 4, prevImageView.Frame.Height - 4);
				var path = UIBezierPath.FromRoundedRect (rect, prevImageView.Frame.Height / 2);
				var prevMaskLayer = new CAShapeLayer ();

				prevMaskLayer.Path = path.CGPath;

//				maskLayer.ShadowPath = path.CGPath;
				prevMaskLayer.ShadowColor = UIColor.Black.CGColor;
				prevMaskLayer.ShadowOpacity = 0.35f;
				prevMaskLayer.ShadowOffset = new CGSize (0, 1);
//				prevImageView.Layer.CornerRadius = prevImageView.Frame.Height/2;
				prevMaskLayer.ShadowRadius = 2;
				prevMaskLayer.ShadowPath = path.CGPath;
//				prevImageView.Layer.MasksToBounds = true;
				prevImageView.ClipsToBounds = false;
				prevImageView.Layer.MasksToBounds = false;
				prevImageView.Layer.Mask = prevMaskLayer;
//				prevImageView.Layer.ShadowPath = path.CGPath;

				prevImageView.SetImage (new NSUrl(images [0]), defaultImage);	

				images.RemoveAt (0);
				AccountsView.Layer.MasksToBounds = false;
				AccountsView.ClipsToBounds = false;
//				AccountsView.Layer.Bounds

				foreach (string imageUrl in images)
				{
					var imageView = new UIImageView ();
					var maskLayer = new CAShapeLayer ();

					maskLayer.Path = path.CGPath;


//					imageView.Layer.ShadowPath = path.CGPath;
//					imageView.Path = path.CGPath;

//					maskLayer.Path = path;
//					imageView.Layer.Mask = maskLayer;

//					imageView.Layer.CornerRadius = prevImageView.Layer.CornerRadius;

					maskLayer.ShadowColor = prevMaskLayer.ShadowColor;
					maskLayer.ShadowOpacity = prevMaskLayer.ShadowOpacity;
					maskLayer.ShadowOffset = prevMaskLayer.ShadowOffset;
					maskLayer.ShadowRadius = prevMaskLayer.ShadowRadius;
					maskLayer.ShadowPath = prevMaskLayer.ShadowPath;

//					imageView.Layer.ShadowPath = path.CGPath;
					imageView.ClipsToBounds = prevImageView.ClipsToBounds;
					imageView.Layer.MasksToBounds = prevImageView.Layer.MasksToBounds;
					imageView.Layer.Mask = maskLayer;
//					imageView.Layer.ShadowRadius = 2;
					imageView.TranslatesAutoresizingMaskIntoConstraints = false;

					imageView.SetImage (new NSUrl (imageUrl), defaultImage);

					var heightConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, prevImageView.Frame.Height);
					var widthConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, prevImageView.Frame.Width);
					var leadingConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Leading, NSLayoutRelation.Equal, prevImageView, NSLayoutAttribute.Trailing, 1, -prevImageView.Frame.Width/2);
					var centerYConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, AccountsView, NSLayoutAttribute.CenterY, 1, 0);
					var trailingConstraint = NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Trailing, NSLayoutRelation.Equal, AccountsView, NSLayoutAttribute.Trailing, 1, 0);	

					AccountsView.AddSubview (imageView);
					AccountsView.SendSubviewToBack (imageView);

					AccountsView.RemoveConstraint (prevTrailingConstraint);
					AccountsView.AddConstraint (heightConstraint);
					AccountsView.AddConstraint (widthConstraint);
					AccountsView.AddConstraint (leadingConstraint);
					AccountsView.AddConstraint (centerYConstraint);
					AccountsView.AddConstraint (trailingConstraint);

					prevMaskLayer = maskLayer;
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

		private void OnRequestPhotoViewer (BaseContentCardViewModel viewModel) 
		{
			var index = ViewModel.CardViewModels.IndexOf (viewModel);
			var cell = _tableController.TableView.CellAt (NSIndexPath.FromRowSection (index, 0)) as DefaultCell;

			if(_fullScreenView == null) {
				_fullScreenView = new UIViewFullscreen();
			}
			_fullScreenView.SetImage(cell.Image);
			_fullScreenView.Show();
		}

		private void OnRequestMovieViewer (BaseContentCardViewModel viewModel)
		{
			var url = NSUrl.FromString(viewModel.MovieUrl);
			_movieController = new MPMoviePlayerViewController(url);

			PresentMoviePlayerViewController (_movieController);
		}
	}
}

