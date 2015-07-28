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
			#if DEBUG
			Calabash.Start();
			#endif

			_lifecycleService.RequestHomePage = OnRequestHomePage;
			_lifecycleService.RequestOnboardingPage = OnRequestOnboardingPage;

			_lifecycleService.OnStart ();

			return true;
		}

		private void OnRequestHomePage ()
		{
			var viewModel = new HomeViewModel ();
			var controller = new ContainerController (viewModel);

			SetRootViewController (controller);
		}

		private void OnRequestOnboardingPage ()
		{
			var storyboard = UIStoryboard.FromName ("Onboarding", null);
			var controller = storyboard.InstantiateInitialViewController ();

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
