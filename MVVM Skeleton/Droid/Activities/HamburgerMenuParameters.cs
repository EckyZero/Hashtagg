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

namespace Droid.Activities
{
    public class HamburgerMenuParameters
    {
        public int Tab { get; set; }

        public MenuActionType Action { get; set; }

        public HamburgerMenuParameters()
        {
        }

        public HamburgerMenuParameters(MenuActionType action, int tab = 0)
        {
            ;
            Tab = tab;
            Action = action;
        }
    }
}