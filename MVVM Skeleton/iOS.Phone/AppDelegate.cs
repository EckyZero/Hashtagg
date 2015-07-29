using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using Xamarin;
using CoreGraphics;
using JASidePanels;
using Shared.VM;
using Shared.Common;
using Shared.Service;

namespace iOS.Phone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		#region Variables

		ILifecycleService _lifecycleService = IocContainer.GetContainer ().Resolve<ILifecycleService> ();

		#endregion

		#region Properties

		public override UIWindow Window { get; set; }

		#endregion

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			var mainViewModel = new MainViewModel ();

			mainViewModel.RequestHomePage = OnRequestHomePage;
			mainViewModel.RequestOnboardingPage = OnRequestOnboardingPage;

			_lifecycleService.OnStart ();

			#if DEBUG
			Calabash.Start();
			#endif

			return true;
		}

		private void OnRequestHomePage (HomeViewModel viewModel)
		{
			var controller = new ContainerController (viewModel);

			SetRootViewController (controller);
		}

		private void OnRequestOnboardingPage (OnboardingViewModel viewModel)
		{
			var storyboard = UIStoryboard.FromName ("Onboarding", null);
			var controller = storyboard.InstantiateInitialViewController () as OnboardingController;

			controller.ViewModel = viewModel;

			SetRootViewController (controller);
		}

		private void SetRootViewController (UIViewController controller)
		{
			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = controller;
			Window.MakeKeyAndVisible ();	
		}

		public override void WillTerminate (UIApplication application)
		{
			_lifecycleService.OnTerminated ();
		}

		public override void DidEnterBackground (UIApplication application)
		{
			_lifecycleService.OnPause ();
		}

		public override void WillEnterForeground (UIApplication application)
		{
			_lifecycleService.OnResume ();
		}
	}
}
