using System;
using Android.Content;
using Shared.Common;

namespace Droid
{
	public class PhoneService : BaseService, IPhoneService
	{
		public bool CallNumber(string phoneNumber)
		{
			try
			{
				var phoneUri = Android.Net.Uri.Parse (String.Format("tel:{0}",phoneNumber));
				var phoneIntent = new Intent (Intent.ActionView, phoneUri);
				_activity.StartActivity (phoneIntent);
				return true;
			}
			catch (Exception e){
				_logger.Log (e);
				return false;
			}
		}
	}
}

