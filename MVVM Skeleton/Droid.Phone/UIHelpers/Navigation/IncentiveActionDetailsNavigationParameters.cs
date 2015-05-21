using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.VM;

namespace Droid.Phone.UIHelpers.Navigation
{
    public class IncentiveActionDetailsNavigationParameters
    {
        public IncentiveActionDetailViewModel ViewModel { get; private set; }

        public IncentiveActionDetailsNavigationParameters(IncentiveActionDetailViewModel vm)
        {
            ViewModel = vm;
        }
    }
}