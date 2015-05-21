using System;
using Accounts;
using Foundation;
using UIKit;

using Shared.Common;
using Microsoft.Practices.Unity;


namespace iOS
{
    public class NotificationsService : BaseService, INotificationsService
	{
        public void RegisterForNotifications()
        {
			//Do not ask for this in Debug (Specifically off for UI Tests/Simulators)
			#if DEBUG
			return;
			#endif

			if (UIDevice.CurrentDevice.CheckSystemVersion (8,0)){
				UIUserNotificationType notificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge;
				var settings = UIUserNotificationSettings.GetSettingsForTypes(notificationTypes, new NSSet (new string[] {}));
				UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);
				UIApplication.SharedApplication.RegisterForRemoteNotifications ();
			} else {
				UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge;
				UIApplication.SharedApplication.RegisterForRemoteNotificationTypes (notificationTypes);
			}
		}


		public int ProcessNotification(NSDictionary options, UIApplication application, bool fromFinishedLaunching, int currentPersonKey)
		{
			//Go ahead and set the badge to 0 if the app is launched
			if(fromFinishedLaunching || application.ApplicationState != UIApplicationState.Active)
				UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
			
			if (null != options && options.ContainsKey (new NSString (CloudNotificationConstants.APS))) {

				if (!fromFinishedLaunching && application.ApplicationState == UIApplicationState.Active) {
					return -1;
				}


				NSDictionary apsMessageDictionary = options.ObjectForKey (new NSString (CloudNotificationConstants.APS)) as NSDictionary;

				//string alert = string.Empty;
			    //string recordId = string.Empty;
				string actionKey = string.Empty;

				bool parseSucess = false;

				int badge = -1;
				int personKey = -1;

				if (apsMessageDictionary == null)
					return -1;

				var nsPersonKey = new NSString (CloudNotificationConstants.PERSON_KEY);

				if (apsMessageDictionary.ContainsKey (nsPersonKey)) {
					parseSucess = int.TryParse (apsMessageDictionary [nsPersonKey].ToString (), out personKey);
					personKey = parseSucess ? personKey : -1;
					if (personKey != currentPersonKey)
						return -1;
				}


				var nsActionKey = new NSString (CloudNotificationConstants.ACTION_KEY);

				if (apsMessageDictionary.ContainsKey (nsActionKey))
					actionKey = (apsMessageDictionary [nsActionKey] as NSString).ToString ();
				
				if (actionKey == CloudNotificationConstants.INCENTIVES)
					return (int)StartupPage.IncentiveSummary;
				if (actionKey == CloudNotificationConstants.HOME)
					return (int)StartupPage.Home;



				//This Information remains here if needed in future state



				//var nsRecordIdKey = new NSString(CloudNotificationConstants.RECORD_ID_KEY);

				//if (apsMessageDictionary.ContainsKey(nsRecordIdKey))
				//	recordId = (apsMessageDictionary[nsRecordIdKey] as NSString).ToString();


				//var nsAlertKey = new NSString(CloudNotificationConstants.ALERT);

				//if (apsMessageDictionary.ContainsKey(nsAlertKey))
				//    alert = (apsMessageDictionary[nsAlertKey] as NSString).ToString();

				//var nsBadgeKey = new NSString(CloudNotificationConstants.BADGE);

				//if (apsMessageDictionary.ContainsKey(nsBadgeKey))
				//{
				//    string badgeStr = (apsMessageDictionary[nsBadgeKey] as NSObject).ToString();
				//    parseSucess  = int.TryParse(badgeStr, out badge);
				//    badge = parseSucess ? badge : -1;
				//}


				//If this came from the ReceivedRemoteNotification while the app was running, in the foreground
				//if (!fromFinishedLaunching && application.ApplicationState == UIApplicationState.Active) {
				//Set Badge to 0 once app is started form notification
				//UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;

				//We Do nothing with alert message when app is running!
				//if (!string.IsNullOrEmpty(alert))
				//{
				//    UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
				//    avAlert.Show();
				//}
				//}
			}
			return -1;
		}
	}
}

