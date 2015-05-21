using System;
using Android.Content;
using Android.App;

namespace Droid
{
	public static class DroidHelper
	{
		public static Action GetCallAction(string phoneNumber)
		{
			return new Action(delegate() 
			{
				var uri = Android.Net.Uri.Parse (string.Format("tel:{0}", phoneNumber));
				var intent = new Intent (Intent.ActionView, uri); 
				intent.SetFlags(ActivityFlags.NewTask);
				Application.Context.StartActivity(intent);
			});
		}

		public static Action GetTextAction(string phoneNumber)
		{
			return new Action(delegate() 
				{
					var uri = Android.Net.Uri.Parse (string.Format("sms:{0}", phoneNumber));
					var intent = new Intent (Intent.ActionView, uri); 
					intent.SetFlags(ActivityFlags.NewTask);
					Application.Context.StartActivity(intent);
				});
		}

		public static Action GetEmailAction(string email)
		{
			return new Action(delegate() 
				{
					//TODO: remove hardcoded subject
					var intent = new Intent (Intent.ActionSend); 
					intent.SetType("message/rfc822");
					intent.PutExtra(Intent.ExtraEmail, email);
					intent.PutExtra(Intent.ExtraSubject, "I need Compass' help!");
					intent.SetFlags(ActivityFlags.NewTask);
					Application.Context.StartActivity(intent);
				});
		}
	}
}

