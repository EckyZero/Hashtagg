using System;
using Shared.Common;
using Foundation;
using UIKit;

namespace iOS
{
	public class PhoneService : BaseService, IPhoneService
	{
		public bool CallNumber (string phoneNumber)
		{
			try 
			{
				var url = new NSUrl (string.Format ("tel:{0}", phoneNumber));
				return UIApplication.SharedApplication.OpenUrl (url);
			}
			catch (Exception e)
			{
				_logger.Log (e);
				return false;
			}
		}
	}
}

