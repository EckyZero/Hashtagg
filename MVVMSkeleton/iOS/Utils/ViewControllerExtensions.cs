using System;
using UIKit;

namespace iOS
{
	public static class ViewControllerExtensions
	{
		public static UIViewController FindViewControllerClass(this UIViewController controller, Type type )
		{
			var rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			var presentedController = rootViewController.PresentedViewController;
			UIViewController desiredController = null;

			if(presentedController != null)
			{
				if(IsKindOfClass(presentedController, type))
				{
					desiredController = presentedController;
				}
				else if(IsKindOfClass(presentedController, typeof(UINavigationController)))
				{
					foreach (UIViewController viewController in presentedController.ChildViewControllers)
					{
						if(IsKindOfClass(viewController, type))
						{
							desiredController = viewController;
							break;
						}
					}
				}
			}
			else 
			{
				foreach (UIViewController viewController in rootViewController.ChildViewControllers)
				{
					if(IsKindOfClass(viewController, type))
					{
						desiredController = viewController;
						break;
					}
					foreach (UIViewController childController in viewController.ChildViewControllers)
					{
						if(IsKindOfClass(childController, type))
						{
							desiredController = childController;
							break;
						}
					}
				}
				if(desiredController == null)
				{
					if(IsKindOfClass(rootViewController, type))
					{
						desiredController = rootViewController;
					}
				}
			}

			return desiredController;
		}

		private static bool IsKindOfClass (UIViewController controller, Type type)
		{
			bool isController = controller.IsKindOfClass (new ObjCRuntime.Class (type));

			return isController;
		}
	}
}

