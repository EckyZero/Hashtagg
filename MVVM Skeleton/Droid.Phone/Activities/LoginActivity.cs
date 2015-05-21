using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;
using Android.Support.V7.App;
using Droid.Activities;
using Droid.Controls;
using Shared.BL;

namespace Droid.Phone.Activities
{
    [Activity(Label = "LoginActivity", WindowSoftInputMode = (SoftInput.StateHidden|SoftInput.AdjustResize), ParentActivity = typeof(GetStartedActivity))]
    public class LoginActivity : ActionBarBaseActivity
    {
        private bool _init = false;

        private string _page = string.Empty;

        private LoginViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        { 
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            _viewModel = IocContainer.GetContainer().Resolve<ILoginViewModel>() as LoginViewModel;
            MainApplication.VMStore.LoginVM = _viewModel;

            SetupToolbar();

            string page = _navigationService.GetAndRemoveParameter<string>(Intent);

            if (!string.IsNullOrWhiteSpace(page))
            {
                _page = page;
            }

            InitBindings();

            RelativeLayout loginLayout = FindViewById<RelativeLayout>(Resource.Id.LoginLayout);

            ViewTreeObserver vto = loginLayout.ViewTreeObserver;
            vto.GlobalLayout += vto_GlobalLayout;
        }

        private void vto_GlobalLayout(object sender, EventArgs e)
        {
            if (!_init)
            {
                MainApplication.VMStore.LoginVM.PostInit();

                _init = true;
            }
        }

        private void _viewModel_CanExecute(object sender, CanExecuteEventArgs e)
        {
            Button continueButton = FindViewById<Button>(Resource.Id.loginPage_ContinueButton);
            continueButton.Enabled = e.CanExecute;
        }

        private void SetupToolbar()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.loginPage_toolbar);
            SetSupportActionBar(toolbar);

            if (MainApplication.VMStore.LoginVM.IsLockedOut && !string.IsNullOrWhiteSpace(_page))
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            }
            else
            {
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            }

            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.Title = "Login";
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                GoBack();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public void Dismiss()
        {
            if (!string.IsNullOrWhiteSpace(_page))
            {
                _navigationService.NavigateTo(_page);
            }
            else
            {
                GoBack();
            }
        }

        public override void OnBackPressed()
        {
            if (MainApplication.VMStore.LoginVM.IsLockedOut)
            {
                var intent = new Intent(this, typeof(GetStartedActivity));
                StartActivity(intent);
            }
            else if (IsTaskRoot)
            {
                var intent = new Intent(this, typeof(GetStartedActivity));
                StartActivity(intent);
            }
            else
            {
                base.OnBackPressed();

            }
        }

        private void InitBindings()
        {
            Button continueButton = FindViewById<Button>(Resource.Id.loginPage_ContinueButton);
            if (!string.IsNullOrWhiteSpace(_page))
            {
                continueButton.SetCommand("Click", MainApplication.VMStore.LoginVM.DimissLoginCommand);
            }
            else
            {
                continueButton.SetCommand("Click", MainApplication.VMStore.LoginVM.LoginCommand);
            }

            continueButton.Enabled = false;

            Button forgotUsername = FindViewById<Button>(Resource.Id.loginPage_ForgotUsername);
            forgotUsername.SetCommand("Click", MainApplication.VMStore.LoginVM.ForgotUsernameCommand);

            Button forgotPassword = FindViewById<Button>(Resource.Id.loginPage_ForgotPassword);
            forgotPassword.SetCommand("Click", MainApplication.VMStore.LoginVM.ForgotPasswordCommand);

            FloatLabeledEditText usernameEditText = FindViewById<FloatLabeledEditText>(Resource.Id.loginPage_UsernameField);

            FloatLabeledEditText passwordEditText = FindViewById<FloatLabeledEditText>(Resource.Id.loginPage_PasswordField);
            passwordEditText.SetCommand("ToolTipInvoked", MainApplication.VMStore.LoginVM.PasswordTipCommand);

            usernameEditText.SetBinding(
            () => usernameEditText.Text,
            () => MainApplication.VMStore.LoginVM.Username,
            BindingMode.TwoWay
            ).UpdateSourceTrigger("TextChanged");

            passwordEditText.SetBinding(
            () => passwordEditText.Text,
            () => MainApplication.VMStore.LoginVM.Password,
            BindingMode.TwoWay
            ).UpdateSourceTrigger("TextChanged");

            MainApplication.VMStore.LoginVM.CanExecute += _viewModel_CanExecute;
        }
    }
}