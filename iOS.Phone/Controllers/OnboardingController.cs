// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using System.ComponentModel;
using CoreGraphics;
using CoreAnimation;
using System.Threading.Tasks;
using Shared.Common;

namespace iOS.Phone
{
	public partial class OnboardingController : UIViewController
	{
		#region Variables

		private bool _isFirstLoad = true;

		#endregion

		#region Properties

		public OnboardingViewModel ViewModel { get; set; }

		public CGRect NavigationRect { 
			get { return ActivityIndicator.Frame; }
		}

		#endregion

		#region Methods

		public OnboardingController (IntPtr handle) : base (handle)
		{
			if(ViewModel == null) {
				ViewModel = new OnboardingViewModel ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			foreach(UIView view in View.Subviews) {
				view.Alpha = 0;
			}
			TitleImageView.Alpha = 1;
			BackgroundImageView.Alpha = 1;
			ActivityIndicator.StopAnimating ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if(NavigationController != null) {
				NavigationController.SetNavigationBarHidden (true, true);
				NavigationController.Delegate = new OnboardingNavigationControllerDelegate ();
			}
		}

		public override async void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if(_isFirstLoad == true)
			{
				_isFirstLoad = false;

				await Task.Delay(1000);

				InitUI ();
				InitBindings ();	

				await ViewModel.DidLoad ();
			}
			GoButton.TouchUpInside += OnGoButtonTapped;
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			GoButton.TouchUpInside -= OnGoButtonTapped;
		}

		private void InitUI ()
		{
			// Setup UI elements
			InitSocialButton(FacebookButton);
			InitSocialButton (TwitterButton);

			var bubbleCenterYConstraint = 0;

			GoButton.Layer.CornerRadius = 6;
			GoButton.Layer.BorderColor = ThemeManager.Instance.CurrentTheme.Disabled.ToUIColor().CGColor;
			GoButton.Layer.BorderWidth = 1;

			// Animations
			View.SetNeedsLayout ();

			TitleImageViewTopConstraint.Constant = 46;

			FacebookButton.Alpha = 0;
			TwitterButton.Alpha = 0;
			GoButton.Alpha = 0;
			SubtitleLabel.Alpha = 0;

			UIView.AnimateNotify (0.75, () => {
				View.LayoutIfNeeded();
				SubtitleLabel.Alpha = 1;
			}, (ic) => {
				FacebookButton.SetNeedsLayout();
				FacebookButtonCenterYConstraint.Constant = bubbleCenterYConstraint;

				UIView.AnimateNotify (1, 0, 0.4f, 1, 0, () => {
					FacebookButton.LayoutIfNeeded();
					FacebookButton.Alpha = 1;			
				}, (ic1) => {
					TwitterButton.SetNeedsLayout();
					TwitterButtonCenterYConstraint.Constant = bubbleCenterYConstraint;
					UIView.AnimateNotify (1, 0, 0.4f, 1, 0, () => {
						TwitterButton.LayoutIfNeeded();
						TwitterButton.Alpha = 1;	
					}, (ic2) => {
						UIView.AnimateNotify (0.5, () => {
							GoButtonBottomConstraint.Constant = 26;
							GoButton.Alpha = 1;	
						}, null);
					});
				});
			});
		}

		private void InitBindings ()
		{
			ViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				if(e.PropertyName.Equals("IsFacebookSelected")) {
					OnSocialButtonSelected(FacebookButton, ViewModel.IsFacebookSelected);
				}
				else if(e.PropertyName.Equals("IsTwitterSelected")) {
					OnSocialButtonSelected(TwitterButton, ViewModel.IsTwitterSelected);
				}
			};

			FacebookButton.SetCommand ("TouchUpInside", ViewModel.FacebookCommand);
			TwitterButton.SetCommand ("TouchUpInside", ViewModel.TwitterCommand);

			ViewModel.RequestHomePage = OnRequestHomePage;
			ViewModel.CanExecute = OnCanExecute;
		}

		private void InitSocialButton (UIButton button) 
		{
			button.Layer.CornerRadius = button.Frame.Height / 2;
			button.Layer.BorderWidth = 1;
			button.Layer.BorderColor = button.CurrentTitleColor.CGColor;
			button.TitleLabel.BackgroundColor = UIColor.Clear;
		}

		private void OnSocialButtonSelected (UIButton button, bool selected)
		{
			button.Selected = !button.Selected;

			if(button.Selected) {
				button.BackgroundColor = UIColor.FromCGColor (button.Layer.BorderColor);
				button.TitleLabel.TextColor = UIColor.White;	
			} else {
				button.BackgroundColor = UIColor.Clear;
				button.TitleLabel.TextColor = UIColor.FromCGColor (button.Layer.BorderColor);	
			}
			button.TitleLabel.BackgroundColor = UIColor.Clear;
		}

		private void OnRequestHomePage(HomeViewModel viewModel)
		{
			var controller = new ContainerController (viewModel);

			NavigationController.PushViewController (controller, true);
		}

		private void OnCanExecute (bool canExecute)
		{
			GoButton.Enabled = canExecute;
			GoButton.Layer.BorderColor = canExecute ? GoButton.TitleLabel.TextColor.CGColor : ThemeManager.Instance.CurrentTheme.Disabled.ToUIColor().CGColor;
		}

		private void OnGoButtonTapped (object sender, EventArgs args)
		{
			GoButton.Hidden = true;
			FacebookButton.UserInteractionEnabled = false;
			TwitterButton.UserInteractionEnabled = false;
			ActivityIndicator.Alpha = 1;
			ActivityIndicator.StartAnimating ();

			ViewModel.GoCommand.Execute (null);
		}

		#endregion
	}

	public class OnboardingNavigationControllerDelegate : UINavigationControllerDelegate
	{
		public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForOperation (UINavigationController navigationController, UINavigationControllerOperation operation, UIViewController fromViewController, UIViewController toViewController)
		{
			return new OnboardingTransitionAnimator ();
		}
	}

	public class OnboardingTransitionAnimator : UIViewControllerAnimatedTransitioning
	{
		IUIViewControllerContextTransitioning _transitionContext;

		public override double TransitionDuration (IUIViewControllerContextTransitioning transitionContext)
		{
			return 0.25;
		}

		public override void AnimateTransition (IUIViewControllerContextTransitioning transitionContext)
		{
			//1
			_transitionContext = transitionContext;

			//2
			var containerView = _transitionContext.ContainerView;
			var fromViewController = _transitionContext.GetViewControllerForKey (UITransitionContext.FromViewControllerKey) as OnboardingController;
			var toViewController = _transitionContext.GetViewControllerForKey (UITransitionContext.ToViewControllerKey) as ContainerController;
			var fromRect = fromViewController.NavigationRect;
			var toRect = new CGRect (-toViewController.View.Bounds.Width / 2, -toViewController.View.Bounds.Height / 2, toViewController.View.Bounds.Width * 2, toViewController.View.Bounds.Height * 2);

			//3
			containerView.AddSubview(toViewController.View);

			//4
			var circleMaskPathInitial = UIBezierPath.FromRoundedRect(fromRect, fromRect.Height/2);

			var circleMaskPathFinal = UIBezierPath.FromRoundedRect (toRect, toRect.Height/2);

			//5
			var maskLayer = new CAShapeLayer();
			maskLayer.Path = circleMaskPathFinal.CGPath;
			toViewController.View.Layer.Mask = maskLayer;

			//6
			var maskLayerAnimation = CABasicAnimation.FromKeyPath("path");
			maskLayerAnimation.SetFrom(circleMaskPathInitial.CGPath);
			maskLayerAnimation.SetTo(circleMaskPathFinal.CGPath);
			maskLayerAnimation.Duration =  TransitionDuration(_transitionContext);
			maskLayerAnimation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseIn);

			maskLayerAnimation.AnimationStopped += (object sender, CAAnimationStateEventArgs e) => {
				if(_transitionContext != null) {
					_transitionContext.CompleteTransition (!_transitionContext.TransitionWasCancelled);
					var controller = _transitionContext.GetViewControllerForKey (UITransitionContext.FromViewControllerKey);
					if(controller != null) {
						controller.View.Layer.Mask = null;	
					}
				}
			};

			maskLayer.AddAnimation (maskLayerAnimation, "path");
		}
	}
}
