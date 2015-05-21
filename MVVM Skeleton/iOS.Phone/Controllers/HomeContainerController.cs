using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using JASidePanels;
using CoreGraphics;
using Shared.Common;

namespace iOS.Phone
{
	public partial class HomeContainerController : JASidePanelController
	{
	    public int InitialContainerPage = -1; 

		public HomeContainerController (IntPtr handle) : base (handle) {}

		// by default applies a black shadow to the container. override in sublcass to change
		//- (void)styleContainer:(UIView *)container animate:(BOOL)animate duration:(NSTimeInterval)duration;
		public override void StyleContainer (UIView container, bool animate, double duration)
		{
			base.StyleContainer (container, animate, duration);
			container.Layer.ShadowOpacity = 0;

		}
		// by default applies rounded corners to the panel. override in sublcass to change
		//- (void)stylePanel:(UIView *)panel;
		public override void StylePanel (UIView panel){
			return;
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);

			//Instantiate HamburgerMenuController
			var leftController = storyboard.InstantiateViewController ("HamburgerMenuController") as HamburgerMenuController;
			//Instantiate HomeController (Center)
			UIViewController centerController;

			if (InitialContainerPage == (int)MenuActionType.Incentives) {
				centerController = UIStoryboard.FromName ("IncentiveStoryboard", null).InstantiateViewController ("IncentivesSummaryTableController") as IncentivesSummaryTableController;
			} 
			else
			{
				centerController = storyboard.InstantiateViewController ("HomeController") as HomeController;
			}

			var navigationController = new UINavigationController (centerController);

			navigationController.ConfigureToCompassDefaults (false);

			LeftPanel = leftController;
			CenterPanel = navigationController;
			ShouldDelegateAutorotateToVisiblePanel = false;

			if(Device.OS >= 7 && Device.OS < 8)
			{
				ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override UIBarButtonItem LeftButtonForCenterPanel ()
		{
			return new UIBarButtonItem (UIImage.FromBundle ("Hamburger"), UIBarButtonItemStyle.Plain, this, new ObjCRuntime.Selector("toggleLeftPanel:"));
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}

		public override void ToggleLeftPanel (NSObject sender)
		{
			base.ToggleLeftPanel (sender);

			var menuController = LeftPanel as HamburgerMenuController;
			var centerController = CenterPanel as UINavigationController;

			if(centerController.ChildViewControllers[0].GetType() == typeof(HomeController))
			{
				
				menuController.ViewWillAppear (true);	
			}		
		}
	}
}
