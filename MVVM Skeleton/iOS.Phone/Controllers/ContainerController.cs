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
		private IDisposable _stateObserver;

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
			PushesSidePanels = true;
			LeftPanel = menuController;
			CenterPanel = homeNavController;

			var image = UIImage.FromFile ("App-bg.png");
			var imageView = new UIImageView (image);

			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.Frame = View.Frame;
			imageView.TranslatesAutoresizingMaskIntoConstraints = true;

			_stateObserver = AddObserver ("state", NSKeyValueObservingOptions.New, ((NSObservedChange obj) => {

				// listen to state changes and forward lifecycle events
				if(State == JASidePanelState.LeftVisible)
				{
					homeController.ViewDidDisappear(true);
					menuController.ViewDidAppear(true);
				} 
				else if (State == JASidePanelState.CenterVisible)
				{
					menuController.ViewDidDisappear(true);
					homeController.ViewDidAppear(true);
				}
			}));

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

			return barButton;
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}

		protected override void Dispose (bool disposing)
		{
			if(disposing)
			{
				if(this != null)
				{
					_stateObserver.Dispose ();
					_stateObserver = null;
				}
			}
			base.Dispose (disposing);
		}

		#endregion
	}
}

