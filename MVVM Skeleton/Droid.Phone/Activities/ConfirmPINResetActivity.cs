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
using Droid.Activities;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "ConfirmPINResetActivity", WindowSoftInputMode = (SoftInput.AdjustResize | SoftInput.StateAlwaysVisible))]
    public class ConfirmPINResetActivity : PINBaseActivity
    {
        private ConfirmPINResetViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = new ConfirmPINResetViewModel();
            _viewModel.Incorrect += HandleIncorrect;

            _viewModel.PIN = _navigationService.GetAndRemoveParameter<string>(Intent);

            _viewModel.RequestCreatePINReset += _viewModel_RequestCreatePINReset;
            _viewModel.RequestSettingsPage += _viewModel_RequestSettingsPage;
        }
 
        private void HandleIncorrect(object sender, EventArgs args)
        {
            IncorrectPIN();
        }

        void _viewModel_RequestCreatePINReset(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.CREATE_PIN_RESET_VIEW_KEY);
        }

        void _viewModel_RequestSettingsPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, new HamburgerMenuParameters(MenuActionType.Settings, 0));
        }

        protected override PINStep PINType
        {
            get { return PINStep.CONFIRM_RESET; }
        }

        protected override void OnPINCompleted(string pin)
        {
            _viewModel.ConfirmPIN = _pin.PIN;
            _viewModel.ComparePINCommand.Execute(null);
        }

        protected override void OnOptionTwoInvoked()
        {
            _viewModel.StartOverCommand.Execute(null);
        }

    }
}