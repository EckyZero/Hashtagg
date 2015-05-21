using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Activities;
using Droid.Phone.UIHelpers.Incentives;
using Shared.Common;
using Shared.VM;
using Android.Graphics;
using Droid.Phone.UIHelpers;
using Droid.Phone.UIHelpers.Navigation;
using Droid.UIHelpers;
using Shared.BL;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "IncentiveActionDetail")]
    public class IncentiveActionDetailActivity : ActionBarBaseActivity
    {
        private IncentiveActionDetailViewModel _viewModel;

        private IIncentiveBL _incentiveBL;
        private Incentive _incentive;

        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Incentive_Action_Detail);

            _incentiveBL = IocContainer.GetContainer().Resolve<IIncentiveBL>();

            IncentiveActionDetailsNavigationParameters param = _navigationService.GetAndRemoveParameter<IncentiveActionDetailsNavigationParameters>(Intent);
            _viewModel = param.ViewModel;

            _viewModel.RequestCannotCompletePage += _viewModel_RequestCannotCompletePage;
            _viewModel.RequestMarkAsCompletedPage +=_viewModel_RequestMarkAsCompletedPage;
			_viewModel.RequestMarkAsCompletedCustomPage +=_viewModel_RequestMarkAsCompletedCustomPage;
            _viewModel.RequestScheduleDoctorAppointmentPage +=_viewModel_RequestScheduleDoctorAppointmentPage;
            _viewModel.RequestViewBiometricResultsPage += _viewModel_RequestViewBiometricResultsPage;

            SetupToolbar();

            StyleSummary();

            HydrateData();

        }

        protected override void OnNewIntent(Intent intent)
        {
            IncentiveActionDetailsNavigationParameters param = _navigationService.GetAndRemoveParameter<IncentiveActionDetailsNavigationParameters>(intent);
            _viewModel = param.ViewModel;

            SetupToolbar();
            StyleSummary();
            HydrateData();
        }

        private void _viewModel_RequestViewBiometricResultsPage(object sender, IncentiveAction e)
        {
			BiometricResultsViewModel viewModel = new BiometricResultsViewModel();
			viewModel.LoadBiometricsData (() => _navigationService.NavigateTo(ViewModelLocator.BIOMETRIC_RESULTS_VIEW_KEY, viewModel));
        }

        private void _viewModel_RequestScheduleDoctorAppointmentPage(object sender, IncentiveAction e)
        {
            //TODO
        }

        private void _viewModel_RequestMarkAsCompletedPage(object sender, IncentiveAction e)
        {
            _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_COMPLETED_PROMPT_VIEW_KEY, e);
        }

		private void _viewModel_RequestMarkAsCompletedCustomPage(object sender, IncentiveAction e)
		{
			var parameters = new IncentiveCompletedAttestationParameters(null, _viewModel.IncentiveAction,
				DateTime.Now, null, showPhoneControl: true);

			_navigationService.NavigateTo(ViewModelLocator.INCENTIVE_COMPLETED_ATTESTATION_PAGE, parameters);
		}

        private void _viewModel_RequestCannotCompletePage(object sender, IncentiveActionCannotCompleteEventArgs args)
        {
            var nextViewModel = new IncentiveCantCompletePickerPromptViewModel(args.Action, args.Step );
            _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_CANT_COMPLETE_REASON_PROMPT_PAGE, nextViewModel);
        }
		
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }


        private void SetupToolbar()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.incentive_actionDetail_toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.Title = _viewModel.Title;
        }

        private void StyleSummary()
        {
            View summary = FindViewById<View>(Resource.Id.incentive_actionDetail_summary);

            View sideBar = summary.FindViewById<View>(Resource.Id.incentive_actionDetailRow_Bar);
            TextView date = summary.FindViewById<TextView>(Resource.Id.incentive_actionDetailRow_date);
            TextView dateDescription = summary.FindViewById<TextView>(Resource.Id.incentive_actionDetailRow_dateDescription);

            sideBar.SetBackgroundColor(_viewModel.StatusBarColor.ToDroidColor());
            date.Text = _viewModel.DateText;
            dateDescription.Text = _viewModel.DateSubtitle;
        }

        private void HydrateData()
        {
            LinearLayout buttonsLayout = FindViewById<LinearLayout>(Resource.Id.incentive_actionDetail_button_layout);

            buttonsLayout.RemoveAllViews();

            TextView description = FindViewById<TextView>(Resource.Id.incentive_actionDetail_description_txt);

            description.Text = _viewModel.DescriptionText;

            foreach (IncentiveActionStepViewModel step in _viewModel.IncentiveActionStepViewModels)
            {
                buttonsLayout.AddView(step.ToView(LayoutInflater));
            }
        }
    }
}