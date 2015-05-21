using System;
using MessageUI;
using Foundation;
using UIKit;
using Shared.Common;

namespace iOS
{
	public static class Device
	{
		public static nfloat KeyboardHeight = 216; // Default value. Should be override if we need something more accurate

        public static MFMailComposeViewController Email (string[] recipients, string subject)
        {
			var mailController = new MFMailComposeViewController ();

			mailController.SetToRecipients (recipients);
			mailController.SetSubject (subject);

            mailController.Finished += MailControllerFinished;

            return mailController;
        }
 
        private static void MailControllerFinished(object s, MFComposeResultEventArgs e)
        {
            e.Controller.DismissViewController(true, null);
        }

		public static void Call (string number)
		{
			var url = new NSUrl ("tel:" + number);

			// TODO: Throw exception if it cannot
			if(UIApplication.SharedApplication.CanOpenUrl(url))
			{
				UIApplication.SharedApplication.OpenUrl(url);
			}
		}

		public static void Text (string number)
		{
			var url = new NSUrl ("sms:" + number);

			// // TODO: Throw exception if it cannot
			if(UIApplication.SharedApplication.CanOpenUrl(url))
			{
				UIApplication.SharedApplication.OpenUrl(url);
			}
		}

		public static void Alert (UIViewController controller, string title, string message)
		{
			UIAlertAction action = UIAlertAction.Create(ApplicationResources.Ok, UIAlertActionStyle.Cancel, null);
			UIAlertController alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

//			alert.TExtC

//			alert.View.TintColor = UIColor.Red;
			alert.Title = title;
			alert.Message = message;

			alert.AddAction(action);
			controller.PresentViewController(alert, true, null);
		}

		public static nint OS
		{
			get
			{
				var device = UIDevice.CurrentDevice;
				return nint.Parse(device.SystemVersion.Substring(0,1)); 
			}
		}
	}
}

