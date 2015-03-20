using Foundation;
using Shared.Common;
using UIKit;

namespace iOS
{
	public class iOSBrowserService : IBrowserService
	{
		public void OpenUrl(string url)
		{
			var webURL = NSUrl.FromString (url);
			if (UIApplication.SharedApplication.CanOpenUrl(webURL)) {
				UIApplication.SharedApplication.OpenUrl(webURL);
			} else {
				throw new OSServiceException (string.Format("Could not open URL {0}", url));
			}
		}


	}
}

