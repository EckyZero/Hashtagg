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
using Droid.Controls;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "GenericProcedureInformationActivity", WindowSoftInputMode = (SoftInput.StateHidden | SoftInput.AdjustPan))]
    public class GenericProcedureInformationActivity : BaseActivity
    {
        private Button _addItButton;

        private GenericProcedurePromptInformationViewModel _viewModel;

        private bool _init = false;

        private FloatLabeledEditText _date;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ProcedureInformation);
            InitBindings();
        }

        private void InitBindings()
        {
            _viewModel = _navigationService.GetAndRemoveParameter<GenericProcedurePromptInformationViewModel>(this.Intent);

            _viewModel.RequestCancelPage += _viewModel_RequestCancelPage;

            _viewModel.RequestPostSaveReturnPage += _viewModel_RequestPostSaveReturnPage;

            _date = FindViewById<FloatLabeledEditText>(Resource.Id.ProcedureInformationDate);

            _date.Touched += DateOnTouched;

            _addItButton = FindViewById<Button>(Resource.Id.ProcedureInformationAddItButton);
            
            var cancelButton = FindViewById<Button>(Resource.Id.ProcedureInformationCancelButton);
            
            var changeButton = FindViewById<Button>(Resource.Id.ProcedureInformationChangeButton);

            var procedureLabel = FindViewById<TextView>(Resource.Id.ProcedureInformationProcedureName);

            procedureLabel.Text = _viewModel.ProcedureName;

            _date.TextChanged += DateTextChanged;

            cancelButton.SetCommand("Click", _viewModel.CancelCommand);
            
            _addItButton.SetCommand("Click", _viewModel.SaveCommand);
            
            changeButton.SetCommand("Click", _viewModel.ChangeCommand);

            _date.SetHint(_viewModel.DatePlaceholder);

            _addItButton.Text = _viewModel.SaveButtonText;

            _addItButton.Enabled = !string.IsNullOrWhiteSpace(_date.Text);

            changeButton.Visibility = _viewModel.EditMode ? ViewStates.Gone : ViewStates.Visible;
            LinearLayout formLayout = FindViewById<LinearLayout>(Resource.Id.ProcedureInformationFormLayout);

            //If used in get connected, remember to set a flag to show Progress Bar or not
            //Certain Pages need the progress bar to be gone when using this generic
            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.ProcedureInformationProgressBar);
            progressBar.Visibility = ViewStates.Gone;
            TextView progressBarTextView = FindViewById<TextView>(Resource.Id.ProcedureInformationProgressBarText);
            progressBarTextView.Visibility = ViewStates.Gone;

            ViewTreeObserver vto = formLayout.ViewTreeObserver;
            vto.GlobalLayout += vto_GlobalLayout;
        }
 
        private void DateTextChanged(object sender, EventArgs args)
        {
            _viewModel.DateTime = _date.Text;
            _addItButton.Enabled = !string.IsNullOrWhiteSpace(_date.Text);
        }

        void _viewModel_RequestPostSaveReturnPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null, new ActivityFlags[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }

        void _viewModel_RequestCancelPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY,null, new ActivityFlags[]{ActivityFlags.SingleTop, ActivityFlags.ClearTop});
        }

        private void DateOnTouched(object sender, EventArgs eventArgs)
        {
            _navigationService.NavigateTo(ViewModelLocator.CALENDAR_VIEW_KEY, new CalendarParameters()
            {
					MinDate = DateTime.Now.Date,
					SelectedDate = DateTime.Now.Date.AddMonths(1),
					MaxDate = DateTime.Now.Date.AddYears(10),
                	OnDateSelected = dt =>
                	{
                    	_date.Text = dt.ToCompassDate();
					}
            });
        }

        private void vto_GlobalLayout(object sender, EventArgs e)
        {
            if (!_init)
            {
                _date.Text = _viewModel.SelectedPatientProcedure.Date.ToCompassDate();
                _init = true;
            }
        }
    }
}