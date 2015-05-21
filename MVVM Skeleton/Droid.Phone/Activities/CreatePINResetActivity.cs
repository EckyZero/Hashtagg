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
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "CreatePINResetActivity", WindowSoftInputMode = (SoftInput.AdjustResize|SoftInput.StateAlwaysVisible))]
    public class CreatePINResetActivity : PINBaseActivity
    {
        private CreatePINResetViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = new CreatePINResetViewModel();

            _viewModel.RequestConfirmPINReset += ViewModelOnRequestConfirmPinReset;

            _viewModel.RequestSettingsPage += ViewModelOnRequestSettingsPage;
        }

        private void ViewModelOnRequestSettingsPage(object sender, EventArgs eventArgs)
        {
            _navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, new HamburgerMenuParameters(MenuActionType.Settings,0));
        }

        private void ViewModelOnRequestConfirmPinReset(object sender, string s)
        {
            _navigationService.NavigateTo(ViewModelLocator.CONFIRM_PIN_RESET_VIEW_KEY,s);
        }

        protected override PINStep PINType
        {
            get { return PINStep.CREATE_RESET; }
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