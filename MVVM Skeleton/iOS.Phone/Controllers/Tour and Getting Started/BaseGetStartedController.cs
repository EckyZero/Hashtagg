
using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace iOS.Phone
{
	public abstract class BaseGetStartedController : UIViewController
	{
		public int Index { get; protected set;}

		protected BaseGetStartedController (IntPtr handle) : base (handle) {}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if(NavigationController != null)
			{
				NavigationController.SetNavigationBarHidden (true, true);
			}
			UIKit.UIPageControl.UIPageControlAppearance pageControl = UIPageControl.Appearance;
			pageControl.PageIndicatorTintColor = UIColor.DarkGray;
			pageControl.CurrentPageIndicatorTintColor = UIColor.White;
			pageControl.BackgroundColor = View.BackgroundColor.ColorWithAlpha (1.0f);
		}
	}
}

