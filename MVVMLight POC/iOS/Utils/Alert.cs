using Demo.Shared.Helpers;
using MonoTouch.UIKit;

namespace Demo.iOS.Utils
{
    public static class Alert
    {
        public static void Show(UIViewController viewController, string title, string message)
        {
            UIAlertAction action = UIAlertAction.Create(Strings.AppConstants.Ok, UIAlertActionStyle.Cancel, null);
            UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            alert.AddAction(action);
            viewController.PresentViewController(alert, true, null);
        }
    }
}