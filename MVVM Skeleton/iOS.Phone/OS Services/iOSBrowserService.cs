using System;
using Shared.Common;
using Foundation;
using UIKit;
using iOS.Phone;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Linq;

namespace iOS
{
    public class iOSBrowserService : BaseService, IBrowserService
	{
		public async Task OpenUrlinApp(string url, string title)
		{
			bool success = true;

			var connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();

			var dialogService = IocContainer.GetContainer().Resolve<IExtendedDialogService>();

			try{

				if (connectivityService.IsConnected)
				{
					UIStoryboard storyboard = UIStoryboard.FromName ("SettingsStoryboard", null);
					var controller = storyboard.InstantiateViewController ("WebNavigationController") as WebNavigationController;
					controller.ConfigureToCompassDefaults();
					controller.InitWebsite = new NSUrl(url);
					controller.WebsiteTitle = title;

					var currentController = UIApplication.SharedApplication.Windows.First().RootViewController.ChildViewControllers.Last();

					await currentController.PresentViewControllerAsync(controller,true);
				}
				else
				{
					await dialogService.ShowMessage(ApplicationResources.GenericOffline, "Currently Offline");
				}
			}
			catch (Exception e){
				_logger.Log(e);
				success = false;
			}

			if (!success) {
				await dialogService.ShowMessage(ApplicationResources.GenericError, "Error Occured");
			}
		}


        public async Task OpenUrlinOS(string url)
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
                        await dialogService.ShowMessage(ApplicationResources.GenericOffline, "Currently Offline");
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
                await dialogService.ShowMessage(ApplicationResources.GenericError, "Error Occured");
            }
        }
	}
}

