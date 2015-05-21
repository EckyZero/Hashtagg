using System;
using UIKit;
using Foundation;
using System.Diagnostics;
using Shared.VM;
using Shared.Common;
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight.Helpers;
using Shared.BL;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using System.Collections.Generic;

namespace iOS.Phone
{
	partial class HomeController : UIViewController
	{
		private HomeViewModel _viewModel;
		private PSAlertBarButtonControl _alertControl;
		private UIView _backgroundCard1;
		private UIView _backgroundCard2;
		private UIView _touchOverlayView;
		private UIPanGestureRecognizer _touchOverlayGesture;
		private bool _forceClose = false;

		public HomeController (IntPtr handle) : base (handle) 
		{
			_viewModel = new HomeViewModel ();
			Application.VMStore.HomeVM = _viewModel;
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_forceClose = _viewModel.ShowFullList;

			InitUI ();
			InitBindings ();

			await _viewModel.DidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			// Since the HelpButton is added to the navigationController's view ...
			// ... We need to manually manage it's visibility
			UIView.Animate (SlideView.AnimationDuration, () => {
				HelpView.Alpha = 1;
			});

			NavigationItem.TitleView.Alpha = SlideView.IsClosed ? 0 : 1;
//			NavigationItem.TitleView.Alpha = (_viewModel.IsFirstLoad && !_forceClose) ? 1 : 0;
//			await _viewModel.DidLoad ();
//			if(_viewModel.ForceClose)
//			{}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			// Since the HelpButton is added to the navigationController's view ...
			// ... We need to manually manage it's visibility
			UIView.Animate (SlideView.AnimationDuration, () => {
				HelpView.Alpha = 0;
			});
		}

		private void InitUI ()
		{
			// Set the titleView's image to be Compass' logo
			var imageView = new UIImageView (new CGRect (0, 0, NavigationController.NavigationBar.Frame.Width - 32, NavigationController.NavigationBar.Frame.Height - 15));
			imageView.Image = UIImage.FromFile ("CompassLogo.png");
			imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			NavigationItem.TitleView = imageView;

			// Set starting constraints depending on our screen size
			var slideViewHeightConstraint = SlideView.Constraints.FirstOrDefault (c => c.FirstItem.Equals (SlideView) && c.FirstAttribute == NSLayoutAttribute.Height);
			var tableViewHeightConstraint = TableView.Constraints.FirstOrDefault (c => c.FirstItem.Equals (TableView) && c.FirstAttribute == NSLayoutAttribute.Height);

			tableViewHeightConstraint.Constant = 0;
			slideViewHeightConstraint.Constant = View.Frame.Height - 150;

			// Add the incentive alerticon/button to the navigation bar
			_alertControl = new PSAlertBarButtonControl (_viewModel.BadgeCount);

			NavigationController.ConfigureToCompassDefaults (true);
			NavigationItem.SetRightBarButtonItem (_alertControl.ToUIBarButtonItem(), true);

			// Format and add EventHandlers to the SlideView's state
			SlideView.UserInteractionEnabled = false;
			SlideView.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile("BGIcons.png"));
			SlideView.ColorForExpandableSection = View.BackgroundColor;
			SlideView.StretchEnabled = false;

			SlideView.DidChange += OnSlideViewDidChange;
			SlideView.WillOpen += OnSlideViewWillOpen;
			SlideView.WillClose += OnSlideViewWillClose;

			// Format and add actions to the HelpView
			var tap = new UITapGestureRecognizer (() => {
				_viewModel.HelpMenuCommand.Execute(null);
			});
			tap.NumberOfTapsRequired = 1;

			HelpView.AddGestureRecognizer (tap);
			HelpView.Layer.CornerRadius = 5;
			HelpView.Layer.BorderColor = SharedColors.Tan2.ToUIColor ().CGColor;
			HelpView.Layer.BorderWidth = 2;

			// IMPORTANT NOTE: The SlideView no longer exists on the HomeController's view
			// This means that any constraints set in the storyboard no longer exist
			// We do this (i.e. add the HelpView to the NavigationController's view) because we need it to move on top of the navigation bar
			// See ViewWillAppear: and ViewWillDisappear: for how we manage it's alpha during controller transitions
			NavigationController.View.AddSubviews (HelpView);

			var centerYConstraint = NSLayoutConstraint.Create (HelpView, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, NavigationController.NavigationBar, NSLayoutAttribute.CenterY, 1, slideViewHeightConstraint.Constant/2 + UIApplication.SharedApplication.StatusBarFrame.Height);
			var centerXConstraint = NSLayoutConstraint.Create (HelpView, NSLayoutAttribute.CenterX, NSLayoutRelation.Equal, NavigationController.NavigationBar, NSLayoutAttribute.CenterX, 1, 0);
			var widthConstraint = NSLayoutConstraint.Create (HelpView, NSLayoutAttribute.Width, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, NavigationController.View.Frame.Width - 32);
			var heightConstraint = NSLayoutConstraint.Create (HelpView, NSLayoutAttribute.Height, NSLayoutRelation.Equal, null, NSLayoutAttribute.NoAttribute, 1, 45);

			NavigationController.View.AddConstraint (centerYConstraint);
			NavigationController.View.AddConstraint (centerXConstraint);
			NavigationController.View.AddConstraint (widthConstraint);
			NavigationController.View.AddConstraint (heightConstraint);

			NavigationController.View.SetNeedsLayout ();
			NavigationController.View.LayoutIfNeeded ();

			// Misc. setup of the TableView in preparation for display
			TableView.EstimatedRowHeight = 180;
			ActivityIndicator.StartAnimating ();

			// Add a transparent view to exist on top of the tableView
			// This will be removed once the SlideView is closed
			// We do this as to not interfere with the TableView's already existing PanGestureRecognizer
			_touchOverlayView = new UIView (new CGRect(0, SlideView.Frame.Height, SlideView.Frame.Width, View.Frame.Height - SlideView.Frame.Height));
			_touchOverlayView.BackgroundColor = UIColor.Clear;

			View.AddSubview (_touchOverlayView);
			_touchOverlayGesture = SlideView.AddGestureToView (_touchOverlayView);
		}

		private void InitBindings ()
		{
			_alertControl.SetCommand("Clicked", _viewModel.BadgeCommand);

			_viewModel.RequestIncentivePage = OnRequestIncentivePage;
			_viewModel.RequestReloadList = OnRequestReloadList;
			_viewModel.RequestHelpMenuPage = OnRequestHelpMenuPage;

			_viewModel.PropertyChanged += (sender, e) => {
				if(e.PropertyName.Equals("BadgeCount")) {
					_alertControl.SetCount(_viewModel.BadgeCount);
				}
			};
		}

//		private async void OnSlideViewDidClose (object sender, CGPoint point)
//		{
//			await Task.Delay(1);
//
//			OnSlideViewWillClose (sender, point);
//		}

		private void OnSlideViewWillClose (object sender, CGPoint point)
		{
			// Animate elements to match the slideView's closed state
			var width = NavigationItem.TitleView.Frame.Width - 25.0f;
			var y = NavigationItem.TitleView.Frame.Y - 8;
			var height = NavigationItem.TitleView.Frame.Height - 4;
			var duration = _forceClose ? 0 : SlideView.AnimationDuration;

			AnimateSlideView (width, y, height, 0, 10, 0, 5, true);

			// Make the tableView the primary views
			TableView.SetNeedsLayout ();
			TableViewTopConstraint.Constant = BaseCardCell.DefaultMargin / 2;

			UIView.Animate (duration, () => {
				TableView.LayoutIfNeeded();	
			});

			// Ensure no other misc. elements are blocking tableView touches
			if(_touchOverlayView != null)
			{
				_touchOverlayView.RemoveGestureRecognizer(_touchOverlayGesture);
				_touchOverlayView.RemoveFromSuperview();
				_touchOverlayGesture = null;
				_touchOverlayView = null;	
			}

			_viewModel.CardsLoaded = true;
		}

		private void OnSlideViewWillOpen (object sender, CGPoint point)
		{
			// Animate elements to match the slideView's opened state
			var width = NavigationController.View.Frame.Width - 32.0f;
			var y = SlideView.Frame.Height/2 + UIApplication.SharedApplication.StatusBarFrame.Height;

			AnimateSlideView (width, y, 45, 1, 14, 2, 5, true);
		}

		private void OnSlideViewDidChange (object sender, CGPoint point)
		{
			var translatedY = point.Y + UIApplication.SharedApplication.StatusBarFrame.Height;

			// Set original values of UI elements
			// We have to hard code these instead of referencing the UI element directly because these will be changing over time
			var originalY = SlideView.Frame.Height/2 + UIApplication.SharedApplication.StatusBarFrame.Height;
			var originalX = 16.0f;
			var originalWidth = NavigationController.View.Frame.Width - 32.0f;
			var originalHeight = 45;
			var originalBorderWidth = 2.0f;
			var originalCornerRadius = 5.0f;
			var originalFontSize = 14.0f;

			// Set the final destination values for the animation
			var finalY = NavigationItem.TitleView.Frame.Y - 8;
			var finalX = NavigationItem.TitleView.Frame.X;
			var finalWidth = NavigationItem.TitleView.Frame.Width - 25.0f;
			var finalHeight = NavigationItem.TitleView.Frame.Height - 4;
			var finalBorderWidth = 0.0f;
			var finalCornerRadius = 5.0f;
			var finalFontSize = 10.0f;

			// Calculate the total change that each UI element has to travel
			var totalYToTravel = originalY - finalY;
			var totalXToTravel = finalX - originalX;
			var totalWidthToTravel = originalWidth - finalWidth;
			var totalHeightToTravel = originalHeight - finalHeight;
			var totalBorderToTravel = originalBorderWidth - finalBorderWidth;
			var totalCornerRadiusToTravel = originalCornerRadius - finalCornerRadius;
			var totalFontToTravel = originalFontSize - finalFontSize;
			 
			var percentComplete = 1 - (translatedY - finalY) / totalYToTravel;

			// Based of the percent complete, calculate what the new values must be for each element
			var remainingYToTravel = originalY - percentComplete * totalYToTravel;
			var remainingWidthToTravel = originalWidth - percentComplete * totalWidthToTravel;
			var remainingHeightToTravel = originalHeight - percentComplete * totalHeightToTravel;
			var remainingBorderToTravel = originalBorderWidth - percentComplete * totalBorderToTravel;
			var remainingCornerToTravel = originalCornerRadius - percentComplete * totalCornerRadiusToTravel;
			var remainingFontToTravel = originalFontSize - percentComplete * totalFontToTravel;
			var remainingXToTravel = originalX + percentComplete * totalXToTravel;

			var alpha = 1 - (percentComplete * 2.25f);

			if(percentComplete <= 1)
			{
				AnimateSlideView (remainingWidthToTravel, remainingYToTravel, remainingHeightToTravel, alpha, remainingFontToTravel, remainingBorderToTravel, remainingCornerToTravel, true);
			}	
		}

		private void AnimateSlideView(nfloat helpButtonWidth, nfloat helpButtonY, nfloat helpButtonHeight, nfloat alpha, nfloat fontSize, nfloat borderWidth, nfloat cornerRadius, bool animated = false)
		{
			if (animated)
				HelpView.SetNeedsLayout ();

			// Set new constraint values
			var duration = _forceClose ? 0 : SlideView.AnimationDuration;
			var centerYConstraint = NavigationController.View.Constraints.FirstOrDefault ( c => c.FirstItem.Equals(HelpView) && c.FirstAttribute == NSLayoutAttribute.CenterY);
			var widthConstraint = NavigationController.View.Constraints.FirstOrDefault (c => c.FirstItem.Equals (HelpView) && c.FirstAttribute == NSLayoutAttribute.Width);
			var heightConstraint = NavigationController.View.Constraints.FirstOrDefault (c => c.FirstItem.Equals (HelpView) && c.FirstAttribute == NSLayoutAttribute.Height);

			widthConstraint.Constant = helpButtonWidth;
			centerYConstraint.Constant = helpButtonY;
			heightConstraint.Constant = helpButtonHeight;


			// Animate/non-animate changes
			if (animated)
			{
				UIView.Animate (duration, () => { 
					HelpView.LayoutIfNeeded (); 
					TitleView.Alpha = alpha;
					NavigationItem.TitleView.Alpha = alpha;
					HelpViewImageView.Alpha = (1 - alpha);
					HelpViewLabel.Font = UIFont.FromName (HelpViewLabel.Font.FamilyName, fontSize);
					HelpView.Layer.BorderWidth = borderWidth;
					HelpView.Layer.CornerRadius = cornerRadius;

					if(_backgroundCard1 != null)
						_backgroundCard1.Alpha = alpha;
					if(_backgroundCard2 != null)
						_backgroundCard2.Alpha = alpha;
				});	
			}
			else
			{
				TitleView.Alpha = alpha;
				NavigationItem.TitleView.Alpha = alpha;
				HelpViewImageView.Alpha = (1 - alpha);
				HelpViewLabel.Font = UIFont.FromName (HelpViewLabel.Font.FamilyName, fontSize);
				HelpView.Layer.BorderWidth = borderWidth;
				HelpView.Layer.CornerRadius = cornerRadius;

				if(_backgroundCard1 != null)
					_backgroundCard1.Alpha = alpha;
				if(_backgroundCard2 != null)
					_backgroundCard2.Alpha = alpha;
			}
		}

		private async void OnRequestReloadList (List<BaseCardViewModel> viewModels)
		{
			// Reload the TableSource
			var animationDuration = 0.75f;
			TableView.Source = new HomeTableSource (viewModels);
			ActivityIndicator.StopAnimating ();

			if (_forceClose)
			{
				// Force the TableView to be the primary view
				// We do this twice because of a UIViewAnimation bug when performing animations quickly
				SlideView.Close (false);
				SlideView.Close (false);

				return;
			}
			// Slight delay to make sure the view has had time to layout its subviews
			await Task.Delay(1);

			// Clear programmatically added views just in case they already exist
			if (_backgroundCard1 != null) {
				_backgroundCard1.RemoveFromSuperview ();
				_backgroundCard1 = null;
			}				
			if (_backgroundCard2 != null) {
				_backgroundCard2.RemoveFromSuperview ();
				_backgroundCard2 = null;
			}

			// Set new constraints on the slideView's elements
			SlideLabel.SetNeedsLayout ();
			SlideImageView.SetNeedsLayout ();
			SlideLabelBottomConstraint.Constant = 25;

			// Animate the first background view in
			var card1 = viewModels[2];
			_backgroundCard1 = CreateBackgroundCardView(card1, 24);

			UIView.AnimateNotify (animationDuration, 0, 0.4f, 1, 0, () => {
				
				SlideLabel.LayoutIfNeeded();
				SlideImageView.LayoutIfNeeded();
				SlideLabel.Alpha = 1;
				SlideImageView.Alpha = 1;
				_backgroundCard1.Frame = new CGRect(_backgroundCard1.Frame.X, SlideView.Frame.Height + 15, _backgroundCard1.Frame.Width, _backgroundCard1.Frame.Height);

			}, (ic1) => {
				// Animate the second background view in
				var card2 = viewModels[1];
				_backgroundCard2 = CreateBackgroundCardView(card2, 16);

				UIView.AnimateNotify (animationDuration, 0, 0.4f, 1, 0, () => {
					_backgroundCard2.Frame = new CGRect(_backgroundCard2.Frame.X, SlideView.Frame.Height + 30, _backgroundCard2.Frame.Width, _backgroundCard2.Frame.Height);
				}, (ic2) => {
					// Animate the TableView in
					TableView.SetNeedsLayout ();
					TableViewTopConstraint.Constant = 45 - BaseCardCell.DefaultMargin/2;
					UIView.AnimateNotify (animationDuration, 0, 0.4f, 1, 0, () => {
						View.BringSubviewToFront(TableView);
						TableView.LayoutIfNeeded();					
					}, (ic3) => {
						// Ensure pannable views are on the top and accessible
						SlideView.UserInteractionEnabled = true;
						if(_touchOverlayView != null)
						{
							View.BringSubviewToFront(_touchOverlayView);	
						}
					});					
				});
			});
		}

		private UIView CreateBackgroundCardView (BaseCardViewModel viewModel, nfloat margin)
		{
			var identifier = BaseCardCell.GetIdentifier (viewModel);
			var cell = TableView.DequeueReusableCell (identifier) as BaseCardCell;

			// Create the backing cell
			cell.Configure (viewModel, true);
			cell.Frame = new CGRect (0, 0, View.Frame.Width - margin*2, cell.CalculateHeight (viewModel, true));

			// Format the view that will contain the cell
			var view = new UIView();
			view.Frame = new CGRect (margin, View.Frame.Height, View.Frame.Width - margin*2, cell.CalculateHeight (viewModel, true));
			view.BackgroundColor = UIColor.White;
			view.Layer.BorderColor = SharedColors.Tan2.ToUIColor ().CGColor;
			view.Layer.BorderWidth = 1;
			view.ClipsToBounds = true;
			view.TranslatesAutoresizingMaskIntoConstraints = (Device.OS < 8);

			// Add the new background card view to the view hierarchy
			view.AddSubview (cell);
			View.AddSubview (view);

			return view;
		}

		private void OnRequestIncentivePage (IHomeViewModel viewModel)
		{
			// Initialize and push the incentive page
			var storyboard = UIStoryboard.FromName ("IncentiveStoryboard", null);
			var controller = storyboard.InstantiateViewController ("IncentivesSummaryTableController");

			NavigationController.PushViewController (controller, true);
		}
			
		private void OnRequestHelpMenuPage (IHomeViewModel viewModel)
		{	
			// Initialize and present the helpmenu page
			var storyboard = UIStoryboard.FromName("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController("HelpMenuController");

			ParentViewController.PresentViewController(controller, (Device.OS >= 8), null);
		}

		public class HomeTableSource : UITableViewSource
		{
			List<BaseCardViewModel> _viewModels;
			bool _scrolledToTop = false;

			public HomeTableSource (List<BaseCardViewModel> viewModels)
			{
				_viewModels = viewModels;
			}

			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return _viewModels.Count;
			}

			public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
			{
				var viewModel = _viewModels [indexPath.Row];
				var identifier = BaseCardCell.GetIdentifier (viewModel);
				var cell = tableView.DequeueReusableCell (identifier) as BaseCardCell;
				var height = cell.CalculateHeight (viewModel);

				return height;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var viewModel = _viewModels [indexPath.Row];
				var identifier = BaseCardCell.GetIdentifier (viewModel);
				var cell = tableView.DequeueReusableCell (identifier) as BaseCardCell;

				cell.Configure (viewModel);

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				// TODO: Implement when we have our full list of cards
			}
		}
	}
}
