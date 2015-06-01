
using System;

using Foundation;
using UIKit;

namespace CompassMobile.iOS.Phone
{
	public partial class HomeController : UIViewController
	{
		public HomeController () : base ("HomeController", null)
		{
		}

		public override void LoadView ()
		{
			base.LoadView ();

			var nibs = NSBundle.MainBundle.LoadNib ("HomeController", this, null);
			View = nibs.GetItem<UIView>(0);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

