using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Microsoft.Practices.Unity;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Phone.Fragments
{
    public class BaseFragment : Fragment
    {
        private ExtendedNavigationService _navigationService;

        protected ExtendedNavigationService NavigationService
        {
            get
            {
                _navigationService = _navigationService ??
                                     IocContainer.GetContainer().Resolve<IExtendedNavigationService>() as ExtendedNavigationService;
                return _navigationService;
            }
        }
   }
}