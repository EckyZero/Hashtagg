using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace iOS.Phone
{
	partial class GetStartedPageController : UIPageViewController
	{
		private ITourViewModel _viewModel;

		public GetStartedPageController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer().Resolve<ITourViewModel> ();
			Application.VMStore.TourVM = _viewModel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// TODO: Add more GetStarted controllers here
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var firstController = storyboard.InstantiateViewController ("GetStartedController") as GetStartedController;
			var secondController = storyboard.InstantiateViewController ("GetStartedShowcase1Controller") as GetStartedShowcase1Controller;
			var pages = new BaseGetStartedController[]{firstController, secondController};

			Title = " ";
			DataSource = new GetConnectedPageDataSource (pages);

			SetViewControllers (new BaseGetStartedController[] {firstController}, UIPageViewControllerNavigationDirection.Forward, false, null);
		}
	}

	public class GetConnectedPageDataSource : UIPageViewControllerDataSource
	{
		private BaseGetStartedController[] _pages; 

		public GetConnectedPageDataSource(BaseGetStartedController[] pages)
		{
			_pages = pages;
		}

		override public UIViewController GetPreviousViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentController = referenceViewController as BaseGetStartedController;

			if(currentController.Index > 0)
			{
				return _pages[currentController.Index - 1];
			}
			return null;
		}

		override public UIViewController GetNextViewController(UIPageViewController pageViewController, UIViewController referenceViewController)
		{
			var currentController = referenceViewController as BaseGetStartedController;

			if(currentController.Index < _pages.Length - 1)
			{
				return _pages [currentController.Index + 1];
			}
			return null;
		}

		public override nint GetPresentationCount (UIPageViewController pageViewController)
		{
			return _pages.Length;
		}

		public override nint GetPresentationIndex (UIPageViewController pageViewController)
		{
			return 0;
		}
	}          

}
