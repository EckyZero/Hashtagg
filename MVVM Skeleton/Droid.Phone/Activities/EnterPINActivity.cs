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
    [Activity(Label = "EnterPINActivity", WindowSoftInputMode = (SoftInput.AdjustResize|SoftInput.StateAlwaysVisible))]
    public class EnterPINActivity : PINBaseActivity
    {
        private IEnterPINViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _viewModel = IocContainer.GetContainer().Resolve<IEnterPINViewModel>();
            _viewModel.Incorrect += HandleIncorrect;
            _viewModel.OnPINSuccess += _viewModel_OnPINSuccess;

            if (_viewModel.IsLockedOut)
            {
                _navigationService.NavigateTo(ViewModelLocator.LOGIN_VIEW_KEY);
            }
        }
 
        private void HandleIncorrect(object sender, EventArgs args)
        {
            IncorrectPIN();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            if (_viewModel.IsLockedOut)
            {
                _navigationService.NavigateTo(ViewModelLocator.LOGIN_VIEW_KEY);
            }
        }

        private void _viewModel_OnPINSuccess(object sender, EventArgs e)
        {
            string page = Intent.GetStringExtra("Activity");
            int parameterPageKey = Intent.GetIntExtra("PageKey", -1);

            if (parameterPageKey != -1 && !string.IsNullOrWhiteSpace(page))
            {
                _navigationService.NavigateTo(page, _navigationService.GenerateParameters((StartupPage)parameterPageKey));
                return;
            }

            if (!string.IsNullOrWhiteSpace(page))
            {
                _navigationService.NavigateTo(page);
                return;
            }

            _navigationService.DismissPage();
        }

        protected override PINStep PINType
        {
            get { return PINStep.ENTER; }
        }

        protected override void OnPINCompleted(string pin)
        {
            _viewModel.PIN = _pin.PIN;
            _viewModel.EnterPINCommand.Execute(null);
        }

        protected override void OnOptionOneInvoked()
        {
            string page = Intent.GetStringExtra("Activity");
            if (!string.IsNullOrWhiteSpace(page))
            {
                _viewModel.ForgetPIN();
                _navigationService.NavigateTo(ViewModelLocator.LOGIN_VIEW_KEY, page);
            }
            else
            {
                _navigationService.NavigateTo(ViewModelLocator.LOGIN_VIEW_KEY, string.Empty);
            }
        }

        protected override void OnOptionTwoInvoked()
        {
            string page = Intent.GetStringExtra("Activity");
            if (!string.IsNullOrWhiteSpace(page))
            {
                _navigationService.NavigateTo(ViewModelLocator.LOGIN_VIEW_KEY, page);
            }
            else
            {
                _navigationService.NavigateTo(ViewModelLocator.LOGIN_VIEW_KEY, string.Empty);
            }
        }

        protected override void OnOptionThreeInvoked()
        {
            _viewModel.TakeTourCommand.Execute(null);
        }

        //Dissable Hardware Back
        public override void OnBackPressed() { }

        public override void Dismiss()
        {
            base.OnBackPressed();
        }
    }
}