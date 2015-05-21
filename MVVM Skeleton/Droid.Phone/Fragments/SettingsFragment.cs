using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using Android.Graphics;
using Droid.Phone.Activities;
using Droid.Phone.UIHelpers.ViewHolders;

namespace Droid.Phone.Fragments
{
    public class SettingsFragment : BaseFragment
    {
        private ViewGroup _container;

        private SettingsViewModel _viewModel;

        private LayoutInflater _inflater;

        private View _settingsMain;

        private bool _isFirstRun = true;

        #region UIInputs

        private Button _editUsernameButton;

        private RelativeLayout _resetPasswordButton;

        private RelativeLayout _resetPINButton;

        private RelativeLayout _addFamilyMemberButton;

        private RelativeLayout _logoutButton;

		private RelativeLayout _supportLayout;

        private TextView _username;

        private SettingsDependentListViewModel _dependentListViewModel;
        
        #endregion

        public SettingsFragment(SettingsDependentListViewModel vm)
        {
            _dependentListViewModel = vm;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //Export our container and inflater out of this method to this Fragment Scope
            _container = container;

            _inflater = inflater;

            InitBindings();

            BindUI();

            SetListeners();

            return _settingsMain;
        }

        private void SetListeners()
        {
            _viewModel.RequestLogin += ViewModelRequestLogin;

            _viewModel.RequestResetPIN += ViewModelOnRequestResetPin;
        }

        private void ViewModelOnRequestResetPin(object sender, EventArgs eventArgs)
        {
            NavigationService.NavigateTo(ViewModelLocator.ENTER_PIN_RESET_VIEW_KEY);
        }

        void ViewModelRequestLogin(object sender, EventArgs e)
        {
            var intent = new Intent(_container.Context, typeof(LoginActivity));
            intent.AddFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            StartActivity(intent);
        }

        private void BindUI()
        {
            _username.Text = _viewModel.CurrentUsername;

            _editUsernameButton.SetCommand("Click", _viewModel.EditUsernameCommand);

            _resetPasswordButton.SetCommand("Click", _viewModel.ResetPasswordCommand);

            _resetPINButton.SetCommand("Click", _viewModel.ResetPINCommand);

            _addFamilyMemberButton.SetCommand("Click", _dependentListViewModel.AddCommand);

            _logoutButton.SetCommand("Click", _viewModel.LogoutCommand);

			_supportLayout.SetCommand("Click", _viewModel.SupportCommand);
        }
        
        public override void OnPause()
        {
            _dependentListViewModel.Unsubscribe();
            base.OnPause();
        }

        private async void RetrieveData()
        {
            if (_isFirstRun)
            {
                _isFirstRun = false;
            }
            else
            {
                await _dependentListViewModel.Subscribe();
            }
        }

        public override void OnResume()
        {
            RetrieveData();
            base.OnResume();    
        }

        private void InitBindings()
        {
            _viewModel = new SettingsViewModel();
            _settingsMain = _inflater.Inflate(Resource.Layout.Settings, _container, false);

            ListView familyMembersView = _settingsMain.FindViewById<ListView>(Resource.Id.SettingsFamilyMembersListView);

            View header = _inflater.Inflate(Resource.Layout.SettingsHeader, null);

            View footer = _inflater.Inflate(Resource.Layout.SettingsFooter, null);

            _dependentListViewModel.RequestAddPage += _dependentListViewModel_RequestAddPage;

            _dependentListViewModel.RequestModifyPage += _dependentListViewModel_RequestModifyPage;

            ObservableAdapter<DependentViewModel> adapter = new ObservableAdapter<DependentViewModel>()
            {
                DataSource = _dependentListViewModel.LookupData,
                GetTemplateDelegate = GetPromptListTemplate
            };
            //END TEMPED
            
            familyMembersView.ItemClick += FamilyMembersViewItemClick;
          
            familyMembersView.AddHeaderView(header);
        
            familyMembersView.AddFooterView(footer);

            familyMembersView.Adapter = adapter;

            _username = header.FindViewById<TextView>(Resource.Id.SettingsHeaderAccountUsername);

            _editUsernameButton = header.FindViewById<Button>(Resource.Id.SettingsHeaderAccountUsernameEditButton);

            _resetPasswordButton = header.FindViewById<RelativeLayout>(Resource.Id.SettingsHeaderAccountResetPasswordLayout);

            _resetPINButton = header.FindViewById<RelativeLayout>(Resource.Id.SettingsHeaderAccountResetPINLayout);

            _addFamilyMemberButton = footer.FindViewById<RelativeLayout>(Resource.Id.SettingsFooterFamilyMembersInviteAnotherLayout);

            _logoutButton = footer.FindViewById<RelativeLayout>(Resource.Id.SettingsFooterLogoutLayout);

			_supportLayout = footer.FindViewById<RelativeLayout> (Resource.Id.SettingsFooterSupportLayout);
        }
 
        private void FamilyMembersViewItemClick(object s, AdapterView.ItemClickEventArgs e)
        {
        }

        void _dependentListViewModel_RequestModifyPage(object sender, Dependent dependent)
        {
            var nextViewModel = new SettingsDependentInformationViewModel(dependent, true);
            NavigationService.NavigateTo(ViewModelLocator.DEPENDENT_INFORMATION_PAGE, nextViewModel);
        }

        void _dependentListViewModel_RequestAddPage(object sender, EventArgs e)
        {
            var nextViewModel = new SettingsDependentInformationViewModel(null, false);
            NavigationService.NavigateTo(ViewModelLocator.DEPENDENT_INFORMATION_PAGE, nextViewModel);
        }


        private View GetPromptListTemplate(int position, DependentViewModel cellViewModel, View convertView)
        {
            var rowView = convertView;
            rowView = SetupCell(position, cellViewModel, rowView);

            (rowView.Tag as CellDataHolder).Tag = position.ToString();

            rowView.Click -= dots_Click;
            rowView.Click += dots_Click;

            return rowView;
        }

        void dots_Click(object sender, EventArgs e)
        {
            var rowView = sender as RelativeLayout;
            int position = -1;
            int.TryParse((rowView.Tag as CellDataHolder).Tag, out position);
            var cellViewModel = _dependentListViewModel.LookupData[position];
            var dialog = new Dialog(_container.Context);
            dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            View dialogView = _inflater.Inflate(Resource.Layout.GetConnectedPromptListDialog, null);
            dialog.SetContentView(dialogView);

            var editButton = dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogEditButton);
            editButton.Click += (o, eventArgs) =>
            {
                cellViewModel.EditCommand.Execute(null);
                dialog.Dismiss();
            };

            var deleteButton = dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogDeleteButton);
            deleteButton.Click += (o, eventArgs) =>
            {
                var builder = new AlertDialog.Builder(_container.Context)
                    .SetMessage(ApplicationResources.RemoveAlertMessage)
                    .SetNegativeButton(ApplicationResources.Remove, (s, a) => cellViewModel.DeleteCommand.Execute(null))
                    .SetPositiveButton(ApplicationResources.Cancel, (s, a) => { })
                    .SetTitle(ApplicationResources.RemoveAlertTitle);

                dialog.Dismiss();

                var alert = builder.Create();

                alert.Show();

                alert.GetButton((int)DialogButtonType.Negative).SetTextColor(Color.Red);
            };

            dialog.FindViewById<Button>(Resource.Id.GetConnectedPromptListDialogCancelButton).Click +=
                (o, eventArgs) =>
                {
                    dialog.Dismiss();
                };

            dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

            dialog.Show();
        }


        private class ViewHolder : Java.Lang.Object, IViewHolder
        {
            public TextView Name { get; set; }
            public TextView Relationship { get; set; }
            public TextView Email { get; set; }
            public TextView Birthdate { get; set; }
            public TextView Gender { get; set; }
        }

        protected View SetupCell(int position, DependentViewModel cellViewModel, View convertView)
        {
            var dependent = cellViewModel.Model;

            var rowView = convertView;

            ViewHolder viewHolder;

            if (rowView == null)
            {
                rowView = _inflater.Inflate(Resource.Layout.DependentsPromptListCell, null);
                viewHolder = new ViewHolder()
                {
                    Name = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellDependentName),
                    Relationship = rowView.FindViewById<TextView>(Resource.Id.DependentsPromptListCellRelationship),
                    Email = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellEmailAddress),
                    Birthdate = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellLongBirthdate),
                    Gender = rowView.FindViewById<TextView>(Resource.Id.DependentPromptListCellGender)
                };
                rowView.Tag = new CellDataHolder() {ViewHolder = viewHolder};
            }
            else
            {
                viewHolder = (rowView.Tag as CellDataHolder).ViewHolder as ViewHolder;
            }

            viewHolder.Name.Text = String.Format("{0} {1}", dependent.FirstName, dependent.LastName);
            viewHolder.Relationship.Text = dependent.Relationship;
            viewHolder.Email.Text = dependent.Username;
            viewHolder.Birthdate.Text = dependent.DateOfBirth != null
                ? dependent.DateOfBirth.Value.ToString("MMMM dd, yyyy")
                : String.Empty;
            viewHolder.Gender.Text = dependent.Gender;

            return rowView;
        }
    }
}