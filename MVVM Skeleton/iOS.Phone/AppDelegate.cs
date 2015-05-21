using Foundation;
using UIKit;

using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.BL;
using System;
using Shared.DAL;
using Shared.BL;
using Shared.Common.Utils;
using Xamarin;
using System.Collections.Generic;

namespace iOS.Phone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private ILifecycleService _lifeCycleService;

		private SplashController _splash;

		NotificationsService _notificationsService;

        private Shared.BL.IMemberBL _memberBl;

		public override UIWindow Window {
			get;
			set;
		}

		#region Push Notifications

		public override void RegisteredForRemoteNotifications (UIApplication application, NSData deviceToken)
		{
			IocContainer.GetContainer ().Resolve<INotificationsBL> ().SaveTokenLocally (deviceToken.ToString ().Trim('<','>'), "ios");
		}

		public override void FailedToRegisterForRemoteNotifications (UIApplication application , NSError error)
		{
			#if DEBUG
			//if(ObjCRuntime.Runtime.Arch == ObjCRuntime.Arch.DEVICE)
			//	new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
			#endif
		}

		//Called if app is alive and in forground, or is resumed from background
		public override void ReceivedRemoteNotification (UIApplication application, NSDictionary userInfo)
		{
			_lifeCycleService.OnResume ();

		    Member currentMember = _memberBl.GetCurrentMember();

			var startupPage = _notificationsService.ProcessNotification(userInfo, application, false, currentMember != null && currentMember.PersonKey != null ? (int)currentMember.PersonKey : -1);

			if (startupPage != -1) {
				
				UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);

				var controller = storyboard.InstantiateViewController ("MainNavigationController") as UINavigationController;
				var splashController = storyboard.InstantiateViewController ("SplashController") as SplashController;
				splashController.IsFirstLoad = true;
				splashController.SkipAnimation = true;
				splashController.NotificationStartupPage = startupPage;

				controller.ConfigureToCompassDefaults ();

				var root = Window.RootViewController as UINavigationController;
				root.DismissViewController (false, null);
				root.SetViewControllers (new UIViewController[] { splashController }, false);
			}
		}

		#endregion
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			var startupPage = -1;



			_notificationsService  = IocContainer.GetContainer ().Resolve<INotificationsService> () as NotificationsService;

            _memberBl = IocContainer.GetContainer().Resolve<IMemberBL>();

			_notificationsService.RegisterForNotifications ();

			if (launchOptions != null) {
				Member currentMember = _memberBl.GetCurrentMember ();
				var notificationOptionsKey = new NSString ("UIApplicationLaunchOptionsRemoteNotificationKey");
				var notificationOptions = launchOptions.ContainsKey (notificationOptionsKey) ? launchOptions.ObjectForKey (notificationOptionsKey) as NSDictionary : null;
				startupPage = _notificationsService.ProcessNotification (notificationOptions, application, true, currentMember != null && currentMember.PersonKey != null ? (int)currentMember.PersonKey : -1);
			}

			IAppInfoDAL appInfoDAL = IocContainer.GetContainer().Resolve<IAppInfoDAL> ();
			IGetConnectedManager getConnectedManager = IocContainer.GetContainer().Resolve<IGetConnectedManager> ();
		    IAppUpgradeService appUpgradeService = IocContainer.GetContainer().Resolve<IAppUpgradeService>();
			// Init instance of lifecycle
            _lifeCycleService = new LifecycleService(_memberBl, appInfoDAL, getConnectedManager, appUpgradeService);

            _lifeCycleService.OnApplicationStart += appUpgradeService.BackgroundUpdateAppUpgradeState;
            _lifeCycleService.OnApplicationResume += appUpgradeService.BackgroundUpdateAppUpgradeState;

			IocContainer.GetContainer ().RegisterInstance<ILifecycleService> (_lifeCycleService);

			_lifeCycleService.OnStart ();

			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("MainNavigationController") as UINavigationController;
//			var child = storyboard.InstantiateViewController("HomeContainerController");
//			var controller = new UINavigationController(child);

			controller.ConfigureToCompassDefaults ();

			SplashController splashController = (controller.ChildViewControllers [0] as SplashController);
            splashController.IsFirstLoad = true;
		    splashController.NotificationStartupPage = startupPage;

			var nav = (ExtendedNavigationService)IocContainer.GetContainer().Resolve<IExtendedNavigationService>();
			nav.Initialize (controller);

//			controller.NavigationBar.BackgroundColor = SharedColors.CompassBlue.ToUIColor ();
//			controller.NavigationBar.Opaque = true;
//			controller.NavigationBar.Translucent = false;
//			controller.NavigationBar.BarTintColor = SharedColors.CompassBlue.ToUIColor ();

			Window = new UIWindow (UIScreen.MainScreen.Bounds);
			Window.RootViewController = controller;
			Window.MakeKeyAndVisible ();

//			child.NavigationController.SetNavigationBarHidden (true, true);
//			controller.PushViewController (storyboard.InstantiateViewController ("HomeContainerController"), true);

			#if DEBUG || TESTCLOUD
			Calabash.Start();
			#endif

			return true;
		}

		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
			_lifeCycleService.OnSleep ();

			UIViewController rootController = UIApplication.SharedApplication.Windows [0].RootViewController;

			EnterPINController enterController = rootController.FindViewControllerClass (typeof(EnterPINController)) as EnterPINController;
			var currentNav = UIApplication.SharedApplication.Windows [0].RootViewController as UINavigationController;

			if (enterController == null && (currentNav.VisibleViewController.GetType() != typeof(SplashController))) {

				var root = UIApplication.SharedApplication.Windows [0].RootViewController.FindViewControllerClass(typeof(UIViewController));
				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("MainNavigationController") as UINavigationController;
				_splash = storyboard.InstantiateViewController ("SplashController") as SplashController;
				_splash.SetBackgroundState (true);
				var splashNavController = new EnterPINNavigationController (controller, _splash);

				root.PresentViewController (splashNavController, true, null);

//				foreach(UIWindow window in UIApplication.SharedApplication.Windows)
//				{
//					CheckSubviews (window.Subviews);	
//				}
			}
		}

		private void CheckSubviews(UIView[] views)
		{
			foreach(UIView view in views)
			{
				if(view.IsKindOfClass(new ObjCRuntime.Class(typeof(UIAlertView))))
				{
					((UIAlertView)view).DismissWithClickedButtonIndex (((UIAlertView)view).CancelButtonIndex, false);
				}	
				else if (view.IsKindOfClass(new ObjCRuntime.Class(typeof(UIActionSheet))))
				{
					((UIActionSheet)view).DismissWithClickedButtonIndex (((UIActionSheet)view).CancelButtonIndex, false);
				}
				else
				{
					CheckSubviews (view.Subviews);
				}
			}
		}

		//public override void WillEnterForeground (UIApplication application)
		public override void OnActivated (UIApplication application)
		{
			_lifeCycleService.OnResume ();

			if(_splash != null)
			{
				_splash.SetBackgroundState (false);
			}
		}

//		This method should be used to release shared resources and it should store the application state.
//		If your application supports background exection this method is called instead of WillTerminate
//		when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
			//OnResignActivation
		}

//		This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
			//OnActivated
		}

//		This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
			//OnTerminated
		}
	}
}
