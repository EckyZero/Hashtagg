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
    [Activity(Label = "ProcedureInformationActivity", WindowSoftInputMode = (SoftInput.StateHidden|SoftInput.AdjustPan))]
    public class ProcedureInformationActivity : BaseActivity
    {
        private Button _addItButton;
        private IProcedurePromptInformationViewModel _viewModel;
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
            _viewModel = IocContainer.GetContainer().Resolve<IProcedurePromptInformationViewModel>();
            MainApplication.VMStore.ProcedurePromptInfoVM = _viewModel;
            var parameters = _navigationService.GetAndRemoveParameter<ProcedureInformationControllerParameters>(Intent);
            _viewModel.EditMode = parameters.EditMode;
            _viewModel.SelectedPatientProcedure = parameters.PatientProcedure;

            _date = FindViewById<FloatLabeledEditText>(Resource.Id.ProcedureInformationDate);
            _date.Touched += DateOnTouched;

            _addItButton = FindViewById<Button>(Resource.Id.ProcedureInformationAddItButton);
            var cancelButton = FindViewById<Button>(Resource.Id.ProcedureInformationCancelButton);
            var changeButton = FindViewById<Button>(Resource.Id.ProcedureInformationChangeButton);

            var prescriptionLabel = FindViewById<TextView>(Resource.Id.ProcedureInformationProcedureName);

            _date.SetBinding(
                () => _date.Text,
               () => MainApplication.VMStore.ProcedurePromptInfoVM.DateTime
            ).UpdateSourceTrigger("TextChanged");

            _date.TextChanged += DateTextChanged;

            prescriptionLabel.SetBinding(
                () => MainApplication.VMStore.ProcedurePromptInfoVM.ProcedureName,
                () => prescriptionLabel.Text
            ).UpdateTargetTrigger(UpdateTriggerMode.PropertyChanged);

            cancelButton.SetCommand("Click", _viewModel.CancelCommand);
            _addItButton.SetCommand("Click", _viewModel.SaveCommand);
            changeButton.SetCommand("Click", _viewModel.ChangeCommand);

            _date.SetHint(_viewModel.DatePlaceholder);

            _addItButton.Text = _viewModel.SaveButtonText;

            _addItButton.Enabled = !string.IsNullOrWhiteSpace(_date.Text);

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.ProcedureInformationProgressBar);
            progressBar.Progress = (int)Math.Round(IocContainer.GetContainer().Resolve<IProcedurePromptViewModel>().Progress * 100);

            changeButton.Visibility = _viewModel.EditMode ? ViewStates.Gone : ViewStates.Visible;
            LinearLayout formLayout = FindViewById<LinearLayout>(Resource.Id.ProcedureInformationFormLayout);

            ViewTreeObserver vto = formLayout.ViewTreeObserver;
            vto.GlobalLayout += vto_GlobalLayout;
        }
 
        private void DateTextChanged(object sender, EventArgs args)
        {
            _addItButton.Enabled = !string.IsNullOrWhiteSpace(_date.Text); 
        }

        private void DateOnTouched(object sender, EventArgs eventArgs)
        {
            _navigationService.NavigateTo(ViewModelLocator.CALENDAR_VIEW_KEY,new CalendarParameters()
            {
					MinDate = DateTime.Today.Date,
					SelectedDate = DateTime.Today.Date.AddMonths(1),
					MaxDate = DateTime.Today.Date.AddYears(10),
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

        public async override void Dismiss()
        {
            var promptViewModel = IocContainer.GetContainer().Resolve<IProcedurePromptViewModel>();
            MainApplication.VMStore.ProcedurePromptVM = promptViewModel;

            var promptShouldAppear = await promptViewModel.ViewShouldAppear();

            if (promptShouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.PROCEDURE_PROMPT_VIEW_KEY, null, new []{ActivityFlags.SingleTop, ActivityFlags.ClearTop});
            }
            else
            {
                _navigationService.NavigateTo(ViewModelLocator.PROCEDURE_PROMPT_LIST_VIEW_KEY, null, new[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
            }
        }
    }
}