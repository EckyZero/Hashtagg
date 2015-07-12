using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.VM;
using System.Threading.Tasks;

namespace Droid.Phone.Activities
{
   [Activity(Label = "HashTagg", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : BaseActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashLayout);
            await Task.Delay(2000);
            _navigationService.NavigateTo(ViewModelLocator.HOME_KEY);
        }
    }
}