using System;
using Shared.Common;
using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Linq;

namespace iOS
{
    public class iOSBrowserService : BaseService, IBrowserService
	{
        public async Task OpenUrl(string url)
        {
            bool success = true;

            var connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();

            var dialogService = IocContainer.GetContainer().Resolve<IExtendedDialogService>();

            try
            {

                if (connectivityService.IsConnected)
                {
                    var webURL = NSUrl.FromString(url);
                    if (UIApplication.SharedApplication.CanOpenUrl(webURL))
                    {
                        UIApplication.SharedApplication.OpenUrl(webURL);
                    }
                    else
                    {
                        await dialogService.ShowMessage("Offline", "Currently Offline");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(e);
                success = false;
            }

            if (!success)
            {
                await dialogService.ShowMessage("Error", "Error Occured");
            }
        }
	}
}

