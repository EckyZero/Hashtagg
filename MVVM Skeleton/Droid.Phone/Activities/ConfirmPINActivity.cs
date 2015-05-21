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
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "ConfirmPINActivity", WindowSoftInputMode = (SoftInput.AdjustResize | SoftInput.StateAlwaysVisible))]
    public class ConfirmPINActivity : PINBaseActivity
    {
        private IConfirmPINViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = IocContainer.GetContainer().Resolve<IConfirmPINViewModel>();
            _viewModel.Incorrect += HandleIncorrect;
            _viewModel.PIN = _navigationService.GetAndRemoveParameter<string>(Intent);
        }
 
        private void HandleIncorrect(object sender, EventArgs args)
        {
            IncorrectPIN();
        }

        protected override PINStep PINType
        {
            get { return PINStep.CONFIRM; }
        }

        protected override void OnPINCompleted(string pin)
        {
            _viewModel.ConfirmPIN = _pin.PIN;
            try
            {
                _viewModel.ComparePINCommand.Execute(null);
            }
            catch
            {
                // retry
                _viewModel.ComparePINCommand.Execute(null);
            }
        }

        protected override void OnOptionTwoInvoked()
        {
            _viewModel.StartOverCommand.Execute(null);
        }

    }
}