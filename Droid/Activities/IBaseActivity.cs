using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Droid
{
    public interface IBaseActivity : ILocationListener
    {
        string ActivityKey { get; }

        string NextPageKey { get; set; }

        void GoBack();
    }
}