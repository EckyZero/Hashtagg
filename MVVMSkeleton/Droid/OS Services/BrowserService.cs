using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Shared.Common;

namespace Droid
{
    public class BrowserService : BaseService, IBrowserService
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
                    var uri = Android.Net.Uri.Parse(url);
                    var intent = new Intent(Intent.ActionView, uri);
                    _activity.StartActivity(intent);
                }
                else
                {
                    await dialogService.ShowMessage("Offline", "Currently Offline");
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