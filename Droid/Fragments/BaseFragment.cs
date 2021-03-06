﻿
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
using GalaSoft.MvvmLight.Views;

namespace Droid
{
    public class BaseFragment : Android.Support.V4.App.Fragment
    {
        ExtendedNavigationService _navigationService;

        public string Title { get; set; }
        public Action LeftActionBarButton { get; set; }
        public Action RightActionBarButton { get; set; }

        protected ExtendedNavigationService NavigationService
        {
            get
            { 
                _navigationService = _navigationService ?? IocContainer.GetContainer().Resolve<IExtendedNavigationService>() as ExtendedNavigationService;
                return _navigationService;
            }
        }
    }
}

