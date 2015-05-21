using System;
using UIKit;
using CoreGraphics;

namespace iOS.Phone
{
	partial class MainNavigationController : UINavigationController
	{
		public MainNavigationController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// TODO: Pull all the fonts, sizes, and colors into a unified class
			NavigationBar.TitleTextAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.White,
				Font = UIFont.FromName("FuturaStd-Bold", 14f)
			};

			this.ConfigureToCompassDefaults ();

//			NavigationItem.BackBarButtonItem.SetImage
//			[backButton setImageInsets:UIEdgeInsetsMake(20, 0, -20, 0)];
					
//			NavigationBar.TintColor = UIColor.White;
//			UIImage image = UIImage.FromBundle ("BackButton");
//			NavigationItem.BackBarButtonItem = new UIBarButtonItem (UIImage.FromBundle ("BackButton"), UIBarButtonItemStyle.Done, null, null);
//			NavigationBar.BackIndicatorImage = image;
//			NavigationBar.BackIndicatorTransitionMaskImage = image;
//			BackIn
//			NavigationBar.SetBackgroundImage (ImageFromColor (NavigationBar.BarTintColor), UIBarPosition.Any, UIBarMetrics.Default);
//			NavigationBar.ShadowImage = ImageFromColor(NavigationBar.BarTintColor);
//			NavigationBar.ShadowImage = ImageFromColor(UIColor.FromRGB(220.0f/255.0f,219.0f/255.0f,185.0f/225.0f));
//			NavigationBar.
				
		}



	}
}
