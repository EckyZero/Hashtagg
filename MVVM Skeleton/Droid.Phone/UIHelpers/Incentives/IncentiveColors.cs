using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Droid.UIHelpers;
using Shared.Common;

namespace Droid.Phone.UIHelpers.Incentives
{
    public static class IncentiveColors
    {
        public static Color Active
        {
            get { return SharedColors.IncentiveActive.ToDroidColor(); }
        }

        public static Color Complete
        {
            get { return SharedColors.IncentiveComplete.ToDroidColor(); }
        }

        public static Color Expired
        {
            get { return SharedColors.IncentiveExpired.ToDroidColor(); }
        }

        public static Color Urgent
        {
            get { return SharedColors.IncentiveUrgent.ToDroidColor(); }
        }
    }
}