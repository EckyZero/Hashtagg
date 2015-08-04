using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Common;

namespace Droid
{
    public class ConnectivityService : BaseService, IConnectivityService
    {
        private ConnectivityManager Manager
        {
            get
            {
                return _activity.GetSystemService(Context.ConnectivityService) as ConnectivityManager;
            }
        }

        public bool IsConnected
        {
            get { return Manager.ActiveNetworkInfo != null && Manager.ActiveNetworkInfo.IsConnected; }
        }
    }
}