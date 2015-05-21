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
using Shared.BL;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "EnterPINResetActivity", WindowSoftInputMode = (SoftInput.AdjustResize|SoftInput.StateAlwaysVisible))]
    public class EnterPINResetActivity : PINBaseActivity
    {
        private EnterPINResetViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = new EnterPINResetViewModel();
            _viewModel.Incorrect += HandleIncorrect;

            _viewModel.RequestCreatePINReset += _viewModel_RequestCreatePINReset;

            _viewModel.RequestSettingsPage += _viewModel_RequestSettingsPage;
        }
 
        private void HandleIncorrect(object sender, EventArgs args)
        {
            IncorrectPIN();
        }

        void _viewModel_RequestSettingsPage(object sender, EventArgs e)
        {
            GoBack();
        }

        void _viewModel_RequestCreatePINReset(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.CREATE_PIN_RESET_VIEW_KEY);
        }

        protected override PINStep PINType
        {
            get { return PINStep.ENTER_RESET; }
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