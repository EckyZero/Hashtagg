using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Activities;
using Droid.Controls;
using Shared.Common;

namespace Droid.Phone.Activities
{
    [Activity(Label = "TooltipActivity")]
    public class TooltipActivity : ActionBarBaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {            
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Tooltip);

            string tipkey = _navigationService.GetAndRemoveParameter<string>(Intent);

            var toolbar = FindViewById<Toolbar>(Resource.Id.tooltipPage_toolbar);
            toolbar.Title = ToolTipHelper.GetToolTipPageTitleUsingKey(tipkey);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.Title = ToolTipHelper.GetToolTipPageTitleUsingKey(tipkey);

            var textview = FindViewById<PSTextView>(Resource.Id.tooltipPage_TooltipText);
            textview.Text = ToolTipHelper.GetMessageUsingKey(tipkey);

        }

        public override bool OnSupportNavigateUp()
        {
            GoBack();
            return true;
        }
    }
}