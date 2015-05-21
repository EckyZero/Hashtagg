using System;
using UIKit;
using Shared.Common;
using CoreGraphics;
using System.Linq;

namespace iOS
{
	public static class NavigationControllerExtensions
	{
		public static void ConfigureToCompassDefaults(this UINavigationController navigationController, bool removeShadow = false)
		{
			navigationController.NavigationBar.BarTintColor = SharedColors.CompassBlue.ToUIColor ();
			navigationController.NavigationBar.TintColor = SharedColors.White.ToUIColor ();
			navigationController.NavigationBar.Translucent = false;
			navigationController.NavigationBar.Opaque = true;
			navigationController.NavigationBar.SetBackgroundImage(SharedColors.CompassBlue.ToUIColor().ToImage(new CGRect(0,0,navigationController.View.Frame.Width, navigationController.NavigationBar.Frame.Height + 20)), UIBarPosition.Any, UIBarMetrics.Default);
			navigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.White,
				Font = UIFont.FromName("FuturaStd-Bold", 16f),
			};

			if(removeShadow)
			{
				navigationController.NavigationBar.ShadowImage = SharedColors.CompassBlue.ToUIColor().ToImage(new CGRect(0,0,navigationController.View.Frame.Width, 1));
			}
				
			navigationController.NavigationBar.BackIndicatorImage = UIImage.FromFile ("BackButton");
			navigationController.NavigationBar.BackIndicatorTransitionMaskImage = UIImage.FromFile ("BackButton");

			navigationController.Delegate = new NavigationControllerDelegate ();
		}

		public static void HideBarsWhileScrolling(this UINavigationController navController, UIScrollView scrollView)
		{
			if(scrollView.ContentSize.Height > scrollView.Frame.Height)
			{
				if(scrollView.ContentOffset.Y > 0)
				{
					if(navController.NavigationBarHidden == false)
					{
						navController.SetNavigationBarHidden (true, true);
					}
				}
				else
				{
					if(navController.NavigationBarHidden == true)
					{
						navController.SetNavigationBarHidden (false, (Device.OS >= 8));
					}
				}
			}
		}

		private class NavigationControllerDelegate : UINavigationControllerDelegate
		{
			public override void WillShowViewController (UINavigationController navigationController, UIViewController viewController, bool animated)
			{
				// Set universal styling of the back button

				// TODO: REPLACE ME WITH THE CODE BELOW ONCE THE UI TEST ISSUES IS FIXED
				viewController.NavigationItem.BackBarButtonItem = new UIBarButtonItem (" ", UIBarButtonItemStyle.Done, null, null);

//				var backBarButton = new UIBarButtonItem (UIImage.FromFile ("BackButton.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
//					navigationController.PopViewController(true);	
//				});
					
//				if(navigationController.ChildViewControllers.Length > 1)
//				{
//					viewController.NavigationItem.SetLeftBarButtonItem (backBarButton, true);	
//				}

				// Make sure all textfields are cleared before moving on
				// This is only an issue when the textfield (i.e. searchbar) is embedded in the navigationController's navigationbar
				navigationController.View.EndEditing (true);

				// Toggle the status bar color depending on whether the navigation bar is hidden
				if(navigationController.NavigationBarHidden)
				{ // White
					viewController.NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;
				}
				else
				{ // Black
					viewController.NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
				}
			}
		}
	}
}

