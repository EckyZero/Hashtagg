using System;
using UIKit;

namespace iOS.Phone
{
	public abstract class BaseNavigationController : UINavigationController
	{
		protected BaseNavigationController () {}
		protected BaseNavigationController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.ConfigureToCompassDefaults (false);
		}
	}
}

