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
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "GetStartedActivity")]
    public class GetStartedActivity : BaseActivity
    {
        private ITourViewModel _viewModel;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = IocContainer.GetContainer().Resolve<ITourViewModel>();
            MainApplication.VMStore.TourVM = _viewModel;

            SetContentView(Resource.Layout.GetStarted);

            InitBindings();
        }

        public void InitBindings()
        {
            Button getStartedButton = FindViewById<Button>(Resource.Id.GetStartedButton);
            getStartedButton.SetCommand("Click", _viewModel.GetStartedCommand);

            Button loginButton = FindViewById<Button>(Resource.Id.GetStartedLoginButton);
            loginButton.SetCommand("Click", _viewModel.LoginCommand);
        }

        //Dissable Hardware Back
        public override void OnBackPressed() { }

    }
}