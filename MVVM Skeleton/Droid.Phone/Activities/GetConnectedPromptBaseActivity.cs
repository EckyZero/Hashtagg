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
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    //T = Provider
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GetConnectedPromptBaseActivity<T> : BaseActivity
    {
        protected abstract int MainImageId { get; }

        protected abstract string TitleText { get; }

        protected abstract string DescriptionText { get; }

        protected abstract string SearchHint { get; }

        protected abstract string IHaveNoneButtonText { get; }

        protected abstract bool ShowDoLater { get; }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.GetConnectedPrompt);

            InitBindings();

            SetupScreen();
        }

        private void SetupScreen()
        {
            FindViewById<TextView>(Resource.Id.GetConnectedPromptTitle).Text = TitleText;
            FindViewById<TextView>(Resource.Id.GetConnectedPromptDescription).Text = DescriptionText;
            FindViewById<ImageView>(Resource.Id.GetConnectedPromptImage).SetImageResource(MainImageId);
            FindViewById<TextView>(Resource.Id.GetConnectedPromptSearch).Text = SearchHint;
            FindViewById<Button>(Resource.Id.GetConnectedPromptIHaveNone).Text = IHaveNoneButtonText;
            if (ShowDoLater)
            {
                FindViewById<Button>(Resource.Id.GetConnectedPromptSkipButton).Visibility = ViewStates.Visible;
            }
        }

        protected abstract void Resume();

        protected override async void OnResume()
        {
            base.OnResume();
            Resume();
        }

        protected abstract void InitBindings();

        //Dissable Hardware Back
        public override void OnBackPressed() { }
    }
}