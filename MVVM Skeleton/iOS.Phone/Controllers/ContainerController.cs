using System;
using JASidePanels;
using Foundation;
using UIKit;
using Shared.VM;

namespace iOS.Phone
{
	public class ContainerController : JASidePanelController
	{
		#region Methods

		public ContainerController () { }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var homeBoard = UIStoryboard.FromName ("Home", null);
			var menuBoard = UIStoryboard.FromName ("Menu", null);
			var homeController = homeBoard.InstantiateInitialViewController ();
			var menuController = menuBoard.InstantiateInitialViewController () as MenuController;

			menuController.ViewModel = new MenuViewModel ();

			ShouldDelegateAutorotateToVisiblePanel = false;
			LeftPanel = menuController;
			CenterPanel = homeController;
		}

		public override void StyleContainer (UIView container, bool animate, double duration)
		{
			base.StyleContainer (container, animate, duration);
			container.Layer.ShadowOpacity = 0;
		}

		public override void StylePanel (UIView panel)
		{
			return;
		}

		public override UIBarButtonItem LeftButtonForCenterPanel ()
		{
			return new UIBarButtonItem (UIImage.FromBundle ("Menu Button"), UIBarButtonItemStyle.Plain, this, new ObjCRuntime.Selector("toggleLeftPanel:"));
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}

		public override void ToggleLeftPanel (NSObject sender)
		{
			base.ToggleLeftPanel (sender);

			var menuController = LeftPanel as MenuController;
			var centerController = CenterPanel as UINavigationController;

			if(centerController.ChildViewControllers[0].GetType() == typeof(HomeController))
			{
				menuController.ViewWillAppear (true);	
			}		
		}

		#endregion
	}
}

