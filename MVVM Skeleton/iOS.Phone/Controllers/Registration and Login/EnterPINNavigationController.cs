
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.Common;

namespace iOS.Phone
{
	public partial class EnterPINNavigationController : UINavigationController
	{
		public EnterPINNavigationController (UINavigationController referenceNavController, UIViewController rootViewController) : base(rootViewController)
		{
			// Copy values over
			this.SetNavigationBarHidden (true, false);
			this.NavigationBar.BackIndicatorTransitionMaskImage = referenceNavController.NavigationBar.BackIndicatorTransitionMaskImage;
			this.NavigationBar.BackIndicatorImage = referenceNavController.NavigationBar.BackIndicatorImage;
			this.NavigationBar.BarTintColor = referenceNavController.NavigationBar.BarTintColor;
			this.NavigationBar.TintColor = referenceNavController.NavigationBar.TintColor;
			this.NavigationBar.Opaque = true;

			UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				Font = UIFont.FromName ("FuturaStd-Bold", 16),
				TextColor = SharedColors.White.ToUIColor()
			});
		}
	}
}

