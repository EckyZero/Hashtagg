using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using Xamarin;

namespace iOS.Phone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			#if DEBUG
			Calabash.Start();
			#endif

			return base.FinishedLaunching(application,launchOptions);
		}
	}
}
