using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Controls;
using Droid.Phone.UIHelpers.Navigation;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Java.Lang;

namespace Droid.Phone.Activities.Incentives.MarkCannotComplete
{
    [Activity(Label = "IncentiveCantCompleteAttestationActivity")]
    public class IncentiveCantCompleteAttestationActivity : IncentiveAttestationBaseActivity
    {
        private IncentiveCantCompleteAttestationViewModel _viewModel;

        private Button _submitButton;

        private FloatLabeledEditText _fullName;

        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);

            base.OnCreate(bundle);

            var toolbar = FindViewById<Toolbar>(Resource.Id.IncentiveAttestationConfirmationToolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetDisplayShowTitleEnabled(true);

            SupportActionBar.Title = ApplicationResources.Attestation;
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

        protected override string HeaderText
        {
            get { return ApplicationResources.ReadAndSignBelow.ToUpper(); }
        }

        protected override string BodyText
        {
            get { return _viewModel.ContentBody; }
        }

        protected override string FooterText
        {
            get { return _viewModel.ContentFooter; }
        }

        protected override string FullNameHintText
        {
            get { return _viewModel.NamePlaceholder; }
        }

		protected override string PhoneHintText
		{
			get { return _viewModel.PhonePlaceholder; }
		}

        protected override string MainButtonText
        {
            get { return ApplicationResources.Submit; }
        }

        protected override string CancelButtonText
        {
            get { return ApplicationResources.Cancel; }
        }

        protected override void SubscribeToEvents()
        {
            _viewModel.RequestPreviousPage += _viewModel_RequestPreviousPage;

            _viewModel.RequestSubmitPage += _viewModel_RequestSubmitPage;

            _viewModel.CanExecute += _viewModel_CanExecute;
        }

        void _viewModel_CanExecute(object sender, CanExecuteEventArgs e)
        {
            _submitButton.Enabled = e.CanExecute;
        }

        void _viewModel_RequestSubmitPage(object sender, EventArgs e)
        {
            IncentiveActionDetailViewModel viewModel = new IncentiveActionDetailViewModel() { IncentiveAction = _viewModel.IncentiveAction };
            viewModel.RefreshData(() =>
            {
                _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_ACTION_DETAIL_KEY, new IncentiveActionDetailsNavigationParameters(viewModel), new []{ActivityFlags.SingleTop, ActivityFlags.ClearTop});
            });
        }

        void _viewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            GoBack();
        }

        protected override void InitBindings()
        {
            _viewModel = _navigationService.GetAndRemoveParameter<IncentiveCantCompleteAttestationViewModel>(Intent);
            
            _submitButton = FindViewById<Button>(Resource.Id.IncentiveAttestationConfirmationSubmitButton);

            _fullName = FindViewById<FloatLabeledEditText>(Resource.Id.IncentiveAttestationConfirmationFullName);

            _fullName.TextChanged += FullNameTextChanged;

            _submitButton.SetCommand("Click", _viewModel.SubmitCommand);

            _submitButton.Enabled = false;

            FindViewById<Button>(Resource.Id.IncentiveAttestationConfirmationCancelButton).SetCommand("Click", _viewModel.CancelCommand);
        }
 
        private void FullNameTextChanged(object sender, EventArgs args)
        {
            _viewModel.Name = _fullName.Text;
        }
    }
}