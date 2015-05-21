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
using Shared.Common;
using System.Threading.Tasks;
using Droid.Phone.Activities;
using Microsoft.Practices.Unity;

namespace Droid
{
    public class BrowserService : BaseService, IBrowserService
    {
        public async Task OpenUrlinApp(string url, string title)
        {
            bool success = true;

            var connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();

            var dialogService = IocContainer.GetContainer().Resolve<IExtendedDialogService>();

            try
            {

                if (connectivityService.IsConnected)
                {
                    var browserIntent = new Intent(_activity, typeof (BrowserActivity));
                    browserIntent.PutExtra("URL", url);
                    browserIntent.PutExtra("Title", title);
                    _activity.StartActivity(browserIntent);
                }
                else
                {
                    await dialogService.ShowMessage(ApplicationResources.GenericOffline, "Currently Offline");
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

        public async Task OpenUrlinOS(string url)
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
                    await dialogService.ShowMessage(ApplicationResources.GenericOffline, "Currently Offline");
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