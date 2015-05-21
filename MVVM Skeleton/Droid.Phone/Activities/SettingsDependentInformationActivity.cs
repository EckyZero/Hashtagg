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
    [Activity(Label = "SettingsDependentInformationActivity")]
    public class SettingsDependentInformationActivity : ActionBarBaseActivity
    {

        private SettingsDependentInformationViewModel _viewModel;

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
        private Button _addButton;


        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);

            base.OnCreate(bundle);

            _viewModel = _navigationService.GetAndRemoveParameter<SettingsDependentInformationViewModel>(Intent);

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

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                _viewModel.CancelCommand.Execute(null);

                return true;
            }
            return base.OnOptionsItemSelected(item);
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
                    _addButton.Enabled = false;
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

            _firstName.TextChanged += FirstNameTextChanged;

            _lastName.TextChanged += LastNameTextChanged;

            _email.TextChanged += EmailTextChanged;

            _sendInviteCheckBox.CheckedChange += SendInviteTextChanged;

            _birthdate.TextChanged += BirthdateTextChanged;

            _social.TextChanged += SocialTextChanged;

            _gender.ItemSelected += GenderItemSelected;

            _relationship.ItemSelected += RelationshipItemSelected;

            _decisionMakerCheckBox.CheckedChange += DecisionMakerCheckChanged;

            _birthdate.SetCommand("ToolTipInvoked", _viewModel.BirthDateCommand);

            _birthdate.InitDate = DateTime.Now.AddMonths(-1).AddYears(-18);
            _birthdate.MaxDate = DateTime.Now.AddDays(-1).AddYears(-18);

            _social.SetCommand("ToolTipInvoked", _viewModel.SSNCommand);

            _gender.SpinnerValues.AddRange(_viewModel.GenderData);

            _relationship.SpinnerValues.AddRange(_viewModel.RelationshipData);

            _addButton = FindViewById<Button>(Resource.Id.DependentInformationLetsGoButton);

            //Set the Add Button Text (Add  / Modify )
            _addButton.SetCommand("Click", _viewModel.AddCommand);
            _addButton.SetText(_viewModel.AddButtonText, TextView.BufferType.Normal);

            //Default Disabled until Changes are made and at least some data is input into all fields
            _addButton.Enabled = false;

            Button cancelButton = FindViewById<Button>(Resource.Id.DependentInformationCancelButton);
            cancelButton.SetCommand("Click", _viewModel.CancelCommand);

            _viewModel.CanExecute += _viewModel_CanExecute;

            _viewModel.RequestReturnPage += _viewModel_RequestReturnPage;

            _viewModel.RequestPreviousPage += _viewModel_RequestPreviousPage;
        }
 
        private void GenderItemSelected(PopupSpinnerEventArgs args)
        {
            _viewModel.Gender = args.ItemSelected.ToString();
        }
 
        private void RelationshipItemSelected(PopupSpinnerEventArgs args)
        {
            _viewModel.Relationship = args.ItemSelected.ToString();
        }
 
        private void DecisionMakerCheckChanged(object sender, CompoundButton.CheckedChangeEventArgs args)
        {
            _viewModel.DecisionMaker = args.IsChecked;
        }
 
        private void SocialTextChanged(object sender, EventArgs args)
        {
            _viewModel.Social = _social.Text;
        }
 
        private void BirthdateTextChanged(object sender, EventArgs args)
        {
            _viewModel.Birthdate = _birthdate.Text;
        }
 
        private void SendInviteTextChanged(object sender, CompoundButton.CheckedChangeEventArgs args)
        {
            _viewModel.Invite = args.IsChecked;
        }
 
        private void EmailTextChanged(object sender, EventArgs args)
        {
            _viewModel.Email = _email.Text;
        }
 
        private void LastNameTextChanged(object sender, EventArgs args)
        {
            _viewModel.LastName = _lastName.Text;
        }
 
        private void FirstNameTextChanged(object sender, EventArgs args)
        {
            _viewModel.FirstName = _firstName.Text;
        }

        void _viewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, new HamburgerMenuParameters(MenuActionType.Settings, 0));
        }

        void _viewModel_RequestReturnPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, new HamburgerMenuParameters(MenuActionType.Settings, 0));
        }

        private void _viewModel_CanExecute(object sender, EventArgs e)
        {
            var canExecuteArgs = (CanExecuteEventArgs)e;

            Button addButton = FindViewById<Button>(Resource.Id.DependentInformationLetsGoButton);
            addButton.Enabled = canExecuteArgs.CanExecute;
        }

        public override void OnBackPressed()
        {
            _viewModel.CancelCommand.Execute(null);
        }
    }
}