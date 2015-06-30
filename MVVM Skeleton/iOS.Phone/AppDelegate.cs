﻿using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using Xamarin;
using CoreGraphics;
using JASidePanels;

namespace iOS.Phone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }
		public JASidePanelController ContainerController { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			#if DEBUG
			Calabash.Start();
			#endif

			var storyboard = UIStoryboard.FromName ("Home", null);
			var controller = storyboard.InstantiateInitialViewController ();

			ContainerController = new JASidePanelController();
			ContainerController.ShouldDelegateAutorotateToVisiblePanel = false;
			ContainerController.LeftPanel = new UIViewController ();
			ContainerController.CenterPanel = controller;
			ContainerController.RightPanel = new UIViewController ();

			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = ContainerController;
			Window.MakeKeyAndVisible ();

			return true;
		}
	}
}
