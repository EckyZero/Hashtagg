using System;
using Shared.Common;
using Foundation;
using UIKit;

namespace iOS
{
	public class EmailService: BaseService, IEmailService
	{
		public bool Email(string emailTo = null)
		{
			try 
			{
				var url = new NSUrl (string.Format ("mailto:{0}", emailTo));
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

