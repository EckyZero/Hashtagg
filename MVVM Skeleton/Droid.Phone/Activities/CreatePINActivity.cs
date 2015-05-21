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
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "CreatePINActivity", WindowSoftInputMode = (SoftInput.AdjustResize|SoftInput.StateAlwaysVisible))]
    public class CreatePINActivity : PINBaseActivity
    {
        private CreatePINViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = new CreatePINViewModel();
        }

        protected override PINStep PINType
        {
            get { return PINStep.CREATE; }
        }

        protected override void OnPINCompleted(string pin)
        {
            _viewModel.PIN = _pin.PIN;
            _viewModel.EnterPINCommand.Execute(null);
        }

        protected override void OnOptionTwoInvoked()
        {
            _viewModel.CancelCommand.Execute(null);
        }

    }
}