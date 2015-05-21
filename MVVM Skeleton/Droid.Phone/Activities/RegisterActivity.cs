using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Activities;
using Droid.Controls;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "RegisterActivity",ParentActivity = typeof(GetStartedActivity))]
    public class RegisterActivity : ActionBarBaseActivity
    {

        private IRegistrationViewModel _viewModel;

        private bool _init = false;

        private RelativeLayout _backgroundContainer;

        protected override void OnCreate(Bundle bundle)
        {
            SetTheme(Resource.Style.ToolbarPageTheme);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Registration);

            _viewModel = IocContainer.GetContainer().Resolve<IRegistrationViewModel>();
            MainApplication.VMStore.RegistrationVM = _viewModel;

            var toolbar = FindViewById<Toolbar>(Resource.Id.registrationPage_toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.Title = "Registration";

            _backgroundContainer = FindViewById<RelativeLayout>(Resource.Id.registrationPage_BackgroundContainer);

            ViewTreeObserver vto = _backgroundContainer.ViewTreeObserver;
            vto.GlobalLayout += vto_GlobalLayout;

            InitBindings();
        }

        private void vto_GlobalLayout(object sender, EventArgs e)
        {
            if (!_init)
            {
                View matchbackground = FindViewById<View>(Resource.Id.registrationPage_MatchBackground);

                ViewGroup.LayoutParams param = matchbackground.LayoutParameters;
                param.Height = _backgroundContainer.Height;
                matchbackground.LayoutParameters = param;

                _init = true;
            }
        }

        private void InitBindings()
        {
            var legalFirstName = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_LegalFirstName);
            var preferredFirstName = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_PreferredFirstName);
            var lastName = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_LastName);
            var email = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_Email);
            var createPassword = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_CreatePassword);
            var confirmPassword = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_ConfirmPassword);
            var birthdate = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_Birthdate);
            var social = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_Social);
            var gender = FindViewById<FloatLabeledEditText>(Resource.Id.registrationPage_Gender);

            legalFirstName.SetBinding(
                () => legalFirstName.Text,
                () => MainApplication.VMStore.RegistrationVM.LegalFirstName
            ).UpdateSourceTrigger("TextChanged");

            preferredFirstName.SetBinding(
                () => preferredFirstName.Text,
                () => MainApplication.VMStore.RegistrationVM.PreferredFirstName,
                BindingMode.TwoWay
            ).UpdateSourceTrigger("TextChanged");

            lastName.SetBinding(
                () => lastName.Text,
                () => MainApplication.VMStore.RegistrationVM.LastName
            ).UpdateSourceTrigger("TextChanged");

            email.SetBinding(
                () => email.Text,
                () => MainApplication.VMStore.RegistrationVM.Email
            ).UpdateSourceTrigger("TextChanged");

            createPassword.SetBinding(
                () => createPassword.Text,
                () => MainApplication.VMStore.RegistrationVM.Password
            ).UpdateSourceTrigger("TextChanged");

            confirmPassword.SetBinding(
                () => confirmPassword.Text,
                () => MainApplication.VMStore.RegistrationVM.ConfirmPassword
            ).UpdateSourceTrigger("TextChanged");

            birthdate.SetBinding(
                () => birthdate.Text,
                () => MainApplication.VMStore.RegistrationVM.Birthdate
            ).UpdateSourceTrigger("TextChanged");

            social.SetBinding(
                () => social.Text,
                () => MainApplication.VMStore.RegistrationVM.Social
            ).UpdateSourceTrigger("TextChanged");

            gender.SetBinding(
                () => gender.Text,
                () => MainApplication.VMStore.RegistrationVM.Gender
            ).UpdateSourceTrigger("TextChanged");

            preferredFirstName.SetCommand("ToolTipInvoked", MainApplication.VMStore.RegistrationVM.PreferredFirstNameCommand);

            createPassword.SetCommand("ToolTipInvoked", MainApplication.VMStore.RegistrationVM.CreatePasswordCommand);

            birthdate.SetCommand("ToolTipInvoked", MainApplication.VMStore.RegistrationVM.BirthDateCommand);

			birthdate.InitDate = MainApplication.VMStore.RegistrationVM.InitBirthDate;

			birthdate.MaxDate = MainApplication.VMStore.RegistrationVM.MaxBirthDate;

            social.SetCommand("ToolTipInvoked", MainApplication.VMStore.RegistrationVM.SSNCommand);

            gender.SpinnerValues.AddRange(MainApplication.VMStore.RegistrationVM.GenderData);

            Button letsGoButton = FindViewById<Button>(Resource.Id.registrationPage_LetsGoButton);
            letsGoButton.SetCommand("Click",_viewModel.RegisterCommand);
            letsGoButton.Enabled = false;

            Button cancelButton = FindViewById<Button>(Resource.Id.registrationPage_CancelButton);
            cancelButton.SetCommand("Click", MainApplication.VMStore.RegistrationVM.CancelCommand);

            _viewModel.CanExecute += _viewModel_CanExecute;
        }

        private void _viewModel_CanExecute(object sender, CanExecuteEventArgs e)
        {
            Button letsGoButton = FindViewById<Button>(Resource.Id.registrationPage_LetsGoButton);
            letsGoButton.Enabled = e.CanExecute;
        }
    }
}