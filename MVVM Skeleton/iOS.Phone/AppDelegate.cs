using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using Xamarin;
using CoreGraphics;
using JASidePanels;
using Shared.VM;

namespace iOS.Phone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			#if DEBUG
			Calabash.Start();
			#endif

			// TODO: Toggle between onboarding/home depending on if they've signed in already
			var storyboard = UIStoryboard.FromName ("Onboarding", null);
			var controller = storyboard.InstantiateInitialViewController ();

			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = controller;
			Window.MakeKeyAndVisible ();

			return true;
		}
	}
}
