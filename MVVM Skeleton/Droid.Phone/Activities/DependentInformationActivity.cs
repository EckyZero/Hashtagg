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
using Java.Sql;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "DependentInformationActivity", ParentActivity = typeof(DependentPromptActivity))]
    public class DependentInformationActivity : ActionBarBaseActivity
    {

        private IDependentInformationViewModel _viewModel;

        private bool _init = false;

        private RelativeLayout _backgroundContainer;

        private FloatLabeledEditText _firstName;

        private FloatLabeledEditText _lastName;

        private FloatLabeledEditText _email;

        private FloatLabeledEditText _birthdate;

        private FloatLabeledEditText _social;

        private FloatLabeledEditText _gender;

        private FloatLabeledEditText _relationship;

        private CheckBox _sendInviteCheckBox;

        private CheckBox _decisionMakerCheckBox;

        
        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            
            base.OnCreate(bundle);

            _viewModel = IocContainer.GetContainer().Resolve<IDependentInformationViewModel>();
            MainApplication.VMStore.DependentInformationVM = _viewModel;

            var paramaters = _navigationService.GetAndRemoveParameter<DependentInformationControllerParameters>(Intent);

            _viewModel.EditMode = paramaters != null &&  paramaters.EditMode;

            _viewModel.SelectedDependent = paramaters != null ? paramaters.Dependent : new Dependent();

            SetContentView(Resource.Layout.DependentInformation);

            var toolbar = FindViewById<Toolbar>(Resource.Id.DependentInformationToolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.Title = "Add a family member";
            
            _backgroundContainer = FindViewById<RelativeLayout>(Resource.Id.DependentInformationBackgroundContainer);

            ViewTreeObserver vto = _backgroundContainer.ViewTreeObserver;
            vto.GlobalLayout += vto_GlobalLayout;

            InitBindings();

        }

        private void vto_GlobalLayout(object sender, EventArgs e)
        {
            if (!_init)
            {
                View matchbackground = FindViewById<View>(Resource.Id.DependentInformationMatchBackground);

                ViewGroup.LayoutParams param = matchbackground.LayoutParameters;
                param.Height = _backgroundContainer.Height;
                matchbackground.LayoutParameters = param;

                //Set the Data late so as to animate the text widgets
                if (_viewModel.EditMode)
                {

                    _firstName.Text = _viewModel.FirstName;
                    _lastName.Text = _viewModel.LastName;
                    _email.Text = _viewModel.Email;
                    _birthdate.Text = _viewModel.Birthdate;
                    _social.Text = _viewModel.Social;
                    _gender.Text = _viewModel.Gender;
                    _relationship.Text = _viewModel.Relationship;
                }
               
                _init = true;
            }
        }

        private void InitBindings()
        {
             //Get all Widgets we need access to from layout
             _firstName = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationFirstName);
             _lastName = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationLastName);
             _email = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationEmail);
             _birthdate = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationBirthdate);
             _social = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationSocial);
             _gender = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationGender);
             _relationship = FindViewById<FloatLabeledEditText>(Resource.Id.DependentInformationRelationship);
             _sendInviteCheckBox = FindViewById<CheckBox>(Resource.Id.DependentInformationInviteCheckBox);
             _decisionMakerCheckBox = FindViewById<CheckBox>(Resource.Id.DependentInformationDecisionMakerCheckBox);

            //Binding to the Check Box updates the _viewModel
            //Have to set before it is updated and overwriten
            //The rest of the data can be set after and has to be for animation reasons.
             _sendInviteCheckBox.Checked = _viewModel.EditMode ? _viewModel.Invite : _viewModel.SendInvitePlaceholder;
             _decisionMakerCheckBox.Checked = _viewModel.EditMode ? _viewModel.DecisionMaker : _viewModel.PrimaryDecisionPlaceholder;

            _firstName.SetBinding(
                () => _firstName.Text,
                () => MainApplication.VMStore.DependentInformationVM.FirstName
            ).UpdateSourceTrigger("TextChanged");

            _lastName.SetBinding(
                () => _lastName.Text,
                () => MainApplication.VMStore.DependentInformationVM.LastName
            ).UpdateSourceTrigger("TextChanged");

            _email.SetBinding(
                () => _email.Text,
                () => MainApplication.VMStore.DependentInformationVM.Email
            ).UpdateSourceTrigger("TextChanged");

            _sendInviteCheckBox.SetBinding(
                () => _sendInviteCheckBox.Checked,
                () => MainApplication.VMStore.DependentInformationVM.Invite
            ).UpdateSourceTrigger("Click");

            _birthdate.SetBinding(
                () => _birthdate.Text,
                () => MainApplication.VMStore.DependentInformationVM.Birthdate
            ).UpdateSourceTrigger("TextChanged");

            _social.SetBinding(
                () => _social.Text,
                () => MainApplication.VMStore.DependentInformationVM.Social
            ).UpdateSourceTrigger("TextChanged");

            _gender.SetBinding(
                () => _gender.Text,
                () => MainApplication.VMStore.DependentInformationVM.Gender
            ).UpdateSourceTrigger("TextChanged");

            _relationship.SetBinding(
                () => _relationship.Text,
                () => MainApplication.VMStore.DependentInformationVM.Relationship
            ).UpdateSourceTrigger("TextChanged");


            _decisionMakerCheckBox.SetBinding(
                () => _decisionMakerCheckBox.Checked,
                () => MainApplication.VMStore.DependentInformationVM.DecisionMaker
            ).UpdateSourceTrigger("Click");
            

            _birthdate.SetCommand("ToolTipInvoked", MainApplication.VMStore.DependentInformationVM.BirthDateCommand);


            _birthdate.InitDate = DateTime.Now.AddMonths(-1).AddYears(-18);
            _birthdate.MaxDate = DateTime.Now.AddDays(-1).AddYears(-18);

            _social.SetCommand("ToolTipInvoked", MainApplication.VMStore.DependentInformationVM.SSNCommand);

            _gender.SpinnerValues.AddRange(MainApplication.VMStore.DependentInformationVM.GenderData);

            _relationship.SpinnerValues.AddRange(MainApplication.VMStore.DependentInformationVM.RelationshipData);

            Button addButton = FindViewById<Button>(Resource.Id.DependentInformationLetsGoButton);
            
            
            //Set the Add Button Text (Add  / Modify )
            addButton.SetCommand("Click", _viewModel.AddCommand);
            addButton.SetText(_viewModel.AddButtonText, TextView.BufferType.Normal);
            
            //Default Disabled until Changes are made and at least some data is input into all fields
            addButton.Enabled = false;

            Button cancelButton = FindViewById<Button>(Resource.Id.DependentInformationCancelButton);
            cancelButton.SetCommand("Click", MainApplication.VMStore.DependentInformationVM.CancelCommand);

            _viewModel.CanExecute += _viewModel_CanExecute;
        }

        private void _viewModel_CanExecute(object sender, EventArgs e)
        {
            var canExecuteArgs = (CanExecuteEventArgs)e;

            Button addButton = FindViewById<Button>(Resource.Id.DependentInformationLetsGoButton);
            addButton.Enabled = canExecuteArgs.CanExecute;
        }

        public override void OnBackPressed()
        {
            Dismiss();
        }

        public override async void Dismiss()
        {
            var promptViewModel = IocContainer.GetContainer().Resolve<IDependentPromptViewModel>();
            MainApplication.VMStore.DependentPromptVM = promptViewModel;
            
            var promptShouldAppear = await promptViewModel.ViewShouldAppear();
            
            if (promptShouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.DEPENDENTS_PROMPT_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
            }
            else
            {
                _navigationService.NavigateTo(ViewModelLocator.DEPENDENTS_PROMPT_LIST_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
            }

        }
    }
}