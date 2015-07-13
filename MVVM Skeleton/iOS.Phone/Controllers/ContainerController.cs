using System;
using JASidePanels;
using Foundation;
using UIKit;
using Shared.VM;

namespace iOS.Phone
{
	public class ContainerController : JASidePanelController
	{
		#region Variables

		private HomeViewModel _homeViewModel;

		#endregion

		#region Properties

//		public event EventHandler WillShowLeftPanel;
//		public event EventHandler WillShowCenterPanel;

		#endregion

		#region Methods

		public ContainerController (HomeViewModel homeViewModel) 
		{ 
			_homeViewModel = homeViewModel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var homeBoard = UIStoryboard.FromName ("Home", null);
			var menuBoard = UIStoryboard.FromName ("Menu", null);
			var menuController = menuBoard.InstantiateInitialViewController () as MenuController;
			var homeNavController = homeBoard.InstantiateInitialViewController() as UINavigationController;
			var homeController = homeNavController.TopViewController as HomeController;

			homeController.ViewModel = _homeViewModel;
			menuController.ViewModel = new MenuViewModel (_homeViewModel.Title);

			DefinesPresentationContext = true;
			ProvidesPresentationContextTransitionStyle = true;
			ShouldDelegateAutorotateToVisiblePanel = false;
			LeftPanel = menuController;
			CenterPanel = homeNavController;
			AllowLeftSwipe = false;
			AllowRightSwipe = false;

			var image = UIImage.FromFile ("App-bg.png");
			var imageView = new UIImageView (image);

			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.Frame = View.Frame;
			imageView.TranslatesAutoresizingMaskIntoConstraints = true;

			View.AddSubview (imageView);
			View.SendSubviewToBack (imageView);
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
			var barButton = new UIBarButtonItem (UIImage.FromBundle ("Menu Button"), UIBarButtonItemStyle.Plain, this, new ObjCRuntime.Selector("toggleLeftPanel:"));

			barButton.Clicked += (sender, e) => {
				ToggleLeftPanel(barButton);
			};
			return barButton;
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
			var homeController = centerController.TopViewController as HomeController;

			if(homeController != null)
			{
				homeController.ViewWillDisappear (true);
				menuController.ViewWillAppear (true);	
			}		
		}

		#endregion
	}
}

