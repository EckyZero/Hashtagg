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
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace Droid.Phone.Activities.Incentives.MarkAsCompleted
{
    [Activity(Label = "IncentiveCompletedInformationActivity")]
    public class IncentiveCompletedInformationActivity : IncentiveAttestationInformationBaseActivity
    {
        private FloatLabeledEditText _date;

        private IncentiveCalendarPromptViewModel _viewModel;

        private Button _submitButton;

        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            
            base.OnCreate(bundle);
            
            var toolbar = FindViewById<Toolbar>(Resource.Id.IncentiveAttestationInformationToolbar);
            
            SetSupportActionBar(toolbar);
            
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            
            SupportActionBar.Title = _viewModel.Title;
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

        protected override int MainImageId
        {
            get { return Resource.Drawable.Calendar; }
        }

        protected override string TitleText
        {
            get { return _viewModel.ContentTitle; }
        }

        protected override bool ShouldShowDescription
        {
            get { return string.IsNullOrWhiteSpace(_viewModel.Subtitle); }
        }

        protected override string DescriptionText
        {
            get { return _viewModel.Subtitle; }
        }

        protected override string MainButtonText
        {
            get { return ApplicationResources.Submit; }
        }

        protected override string DateHint
        {
            get { return ApplicationResources.SelectDate; }
        }

        protected override bool ShouldShowDate
        {
            get { return true; }
        }

        protected override string CellHeader
        {
            get { return _viewModel.ContentHeader; }
        }

        protected override string CellBody
        {
            get { return _viewModel.ContentBody; }
        }

        protected override string CellFooterOne
        {
            get { return _viewModel.ContentFooter; }
        }

        protected override string CellFooterTwo
        {
            get { return _viewModel.ContentFooterTwo; }
        }

        protected override void SubscribeToEvents()
        {
            _viewModel.RequestChangePage += _viewModel_RequestChangePage;

            _viewModel.RequestPreviousPage += _viewModel_RequestPreviousPage;

            _viewModel.CanExecute += _viewModel_CanExecute;

            _viewModel.RequestNextPage += _viewModel_RequestNextPage;
        }

        void _viewModel_RequestNextPage(object sender, object e)
        {
            var parameters = new IncentiveCompletedAttestationParameters(_viewModel.Model, _viewModel.IncentiveAction,
                _viewModel.CalendarDate, _viewModel.IncentiveActionProcedure);

            _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_COMPLETED_ATTESTATION_PAGE, parameters);
        }

        void _viewModel_CanExecute(object sender, CanExecuteEventArgs e)
        {
            _submitButton.Enabled = e.CanExecute;
        }

        void _viewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            GoBack();
        }

        void _viewModel_RequestChangePage(object sender, EventArgs e)
        {
            Action<Provider> changeLookupOnDismiss = provider =>
            {
                var nextParams = new IncentiveCalendarPromptParameters()
                {
                    SelectedIncentiveActionProcedure = _viewModel.IncentiveActionProcedure,
                    SelectedIncentiveAction = _viewModel.IncentiveAction,
                    SelectedProvider = provider
                };

                _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_COMPLETED_INFORMATION_PAGE,nextParams);

            };

            _navigationService.NavigateTo(ViewModelLocator.DOCTOR_LOOKUP_VIEW_KEY, new DoctorLookupControllerParameters()
            {
                DoctorName = _viewModel.ContentBody,

                OnPatientSelectedAndReturn = changeLookupOnDismiss
            });
        }

        protected override void InitBindings()
        {
            var parameters = _navigationService.GetAndRemoveParameter<IncentiveCalendarPromptParameters>(Intent);

            _viewModel = new IncentiveCalendarPromptViewModel(parameters.SelectedProvider,
                parameters.SelectedIncentiveAction, parameters.SelectedIncentiveActionProcedure);

            _date = FindViewById<FloatLabeledEditText>(Resource.Id.IncentiveAttestationDate);
            
            _date.Touched += DateOnTouched;

            FindViewById<Button>(Resource.Id.IncentiveAttestationInformationCellChangeButton).SetCommand("Click",_viewModel.ChangeCommand);
            
            _submitButton = FindViewById<Button>(Resource.Id.IncentiveAttestationInformationSubmitButton);
            
            _submitButton.SetCommand("Click", _viewModel.NextCommand);
            
            _submitButton.Enabled = false;
            
            FindViewById<Button>(Resource.Id.IncentiveAttestationInformationCancelButton).SetCommand("Click", _viewModel.CancelCommand);
        }

        private void DateOnTouched(object sender, EventArgs eventArgs)
        {
            _navigationService.NavigateTo(ViewModelLocator.CALENDAR_VIEW_KEY, new CalendarParameters()
            {
					MinDate = DateTime.Now.Date.AddYears(-10),
					SelectedDate = DateTime.Now.Date.AddMonths (-1),
					MaxDate = DateTime.Now.Date,
                OnDateSelected = dt =>
                {
                    _viewModel.CalendarDate = Convert.ToDateTime(dt);
                    _date.Text = dt.ToCompassDate();
                }
            });
		}
    }
}