using System;
using Shared.Common;
using Android.Content;

namespace Droid
{
	public class EmailService : BaseService, IEmailService
	{
		public bool Email(string emailTo = null)
		{
			try
			{
				var emailIntent = new Intent (Android.Content.Intent.ActionSend);

				if(!string.IsNullOrWhiteSpace(emailTo)){
					emailIntent.PutExtra (Android.Content.Intent.ExtraEmail, 
					new string[]{emailTo} );
				}

				_activity.StartActivity (emailIntent);
				return true;
			}
			catch (Exception e){
				_logger.Log (e);
				return false;
			}
		}
	}
}

