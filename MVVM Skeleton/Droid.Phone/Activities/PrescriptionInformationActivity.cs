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
    [Activity(Label = "PrescriptionInformationActivity", WindowSoftInputMode = (SoftInput.StateHidden|SoftInput.AdjustResize))]
    public class PrescriptionInformationActivity : BaseActivity
    {
        private IPrescriptionInformationViewModel _viewModel;
        private bool _init = false;
        private FloatLabeledEditText _dosage;
        private FloatLabeledEditText _frequency;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PrescriptionInformation);
            InitBindings();

        }

        private void InitBindings()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IPrescriptionInformationViewModel>();
            MainApplication.VMStore.PrescriptionInformationVM = _viewModel;
            var parameters = _navigationService.GetAndRemoveParameter<PrescriptionInformationControllerParameters>(Intent);
            _viewModel.EditMode = parameters.EditMode;
            _viewModel.SelectedPrescription = parameters.PatientPrescription;

            _dosage = FindViewById<FloatLabeledEditText>(Resource.Id.PrescriptionInformationDosage);
            _frequency = FindViewById<FloatLabeledEditText>(Resource.Id.PrescriptionInformationFrequency);

            var addItButton = FindViewById<Button>(Resource.Id.PrescriptionInformationAddItButton);
            var cancelButton = FindViewById<Button>(Resource.Id.PrescriptionInformationCancelButton);
            var changeButton = FindViewById<Button>(Resource.Id.PrescriptionInformationChangeButton);

            var prescriptionLabel = FindViewById<TextView>(Resource.Id.PrescriptionInformationPrescriptionName);

            _dosage.TextChanged += DosageTextChanged;

            _frequency.TextChanged += FrequencyTextChanged;

            prescriptionLabel.SetBinding(
                () => MainApplication.VMStore.PrescriptionInformationVM.Prescription,
                () => prescriptionLabel.Text
            ).UpdateTargetTrigger(UpdateTriggerMode.PropertyChanged);

            cancelButton.SetCommand("Click", MainApplication.VMStore.PrescriptionInformationVM.CancelCommand);
            addItButton.SetCommand("Click", MainApplication.VMStore.PrescriptionInformationVM.AddCommand);
            changeButton.SetCommand("Click", MainApplication.VMStore.PrescriptionInformationVM.ChangeCommand);

            _dosage.SetHint(MainApplication.VMStore.PrescriptionInformationVM.DosagePlaceholder);
            _frequency.SetHint(MainApplication.VMStore.PrescriptionInformationVM.FrequencyPlaceholder);
            addItButton.Text = MainApplication.VMStore.PrescriptionInformationVM.AddButtonText;
            changeButton.Visibility = MainApplication.VMStore.PrescriptionInformationVM.IsChangeButtonHidden ? ViewStates.Gone : ViewStates.Visible;

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.PrescriptionInformationProgressBar);
            progressBar.Progress = (int)Math.Round(IocContainer.GetContainer().Resolve<IPrescriptionPromptViewModel>().Progress * 100);

            _viewModel.RequestPostSaveReturnPage += ViewModelRequestPostSaveReturnPage;
            LinearLayout formLayout = FindViewById<LinearLayout>(Resource.Id.PrescriptionInformationFormLayout);

            ViewTreeObserver vto = formLayout.ViewTreeObserver;
            vto.GlobalLayout += vto_GlobalLayout;
        }
 
        private void FrequencyTextChanged(object sender, EventArgs args)
        {
            MainApplication.VMStore.PrescriptionInformationVM.Frequency = _frequency.Text;
        }
 
        private void DosageTextChanged(object sender, EventArgs args)
        {
            MainApplication.VMStore.PrescriptionInformationVM.Dosage = _dosage.Text;
        }

        private void vto_GlobalLayout(object sender, EventArgs e)
        {
            if (!_init)
            {
                _dosage.Text = MainApplication.VMStore.PrescriptionInformationVM.Dosage;
                _frequency.Text = MainApplication.VMStore.PrescriptionInformationVM.Frequency;

                _init = true;
            }
        }
        private void ViewModelRequestPostSaveReturnPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.PRESCRIPTION_PROMPT_LIST_VIEW_KEY, null, new ActivityFlags[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
        }

        public async override void Dismiss()
        {
            var promptViewModel = IocContainer.GetContainer().Resolve<IPrescriptionPromptViewModel>();
            MainApplication.VMStore.PrescriptionPromptVM = promptViewModel;

            var promptShouldAppear = await promptViewModel.ViewShouldAppear();

            if (promptShouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.PRESCRIPTION_PROMPT_VIEW_KEY,null,new ActivityFlags[]{ActivityFlags.SingleTop, ActivityFlags.ClearTop});
            }
            else
            {
                _navigationService.NavigateTo(ViewModelLocator.PRESCRIPTION_PROMPT_LIST_VIEW_KEY, null, new ActivityFlags[] { ActivityFlags.SingleTop, ActivityFlags.ClearTop });
            }
        }
    }
}