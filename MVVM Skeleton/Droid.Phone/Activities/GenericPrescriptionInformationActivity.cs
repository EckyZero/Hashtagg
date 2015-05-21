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
    [Activity(Label = "GenericPrescriptionInformationActivity", WindowSoftInputMode = (SoftInput.StateHidden | SoftInput.AdjustResize))]
    public class GenericPrescriptionInformationActivity : BaseActivity
    {
        private GenericPrescriptionInformationViewModel _viewModel;

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
            _viewModel = _navigationService.GetAndRemoveParameter<GenericPrescriptionInformationViewModel>(Intent);

            _dosage = FindViewById<FloatLabeledEditText>(Resource.Id.PrescriptionInformationDosage);

            _frequency = FindViewById<FloatLabeledEditText>(Resource.Id.PrescriptionInformationFrequency);

            var addItButton = FindViewById<Button>(Resource.Id.PrescriptionInformationAddItButton);

            var cancelButton = FindViewById<Button>(Resource.Id.PrescriptionInformationCancelButton);

            var changeButton = FindViewById<Button>(Resource.Id.PrescriptionInformationChangeButton);

            var prescriptionLabel = FindViewById<TextView>(Resource.Id.PrescriptionInformationPrescriptionName);

            _dosage.TextChanged += DosageTextChanged;

            _frequency.TextChanged += FrequencyTextChanged;

            prescriptionLabel.Text = _viewModel.Prescription;

            cancelButton.SetCommand("Click", _viewModel.CancelCommand);

            addItButton.SetCommand("Click", _viewModel.AddCommand);

            changeButton.SetCommand("Click", _viewModel.ChangeCommand);

            _dosage.SetHint(_viewModel.DosagePlaceholder);

            _frequency.SetHint(_viewModel.FrequencyPlaceholder);

            addItButton.Text = _viewModel.AddButtonText;

            //If used in get connected, remember to set a flag to show Progress Bar or not
            //Certain Pages need the progress bar to be gone when using this generic
            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.PrescriptionInformationProgressBar);
            progressBar.Visibility = ViewStates.Gone;
            TextView progressBarTextView = FindViewById<TextView>(Resource.Id.PrescriptionInformationProgressBarText);
            progressBarTextView.Visibility = ViewStates.Gone;

            changeButton.Visibility = _viewModel.IsChangeButtonHidden ? ViewStates.Gone : ViewStates.Visible;

            LinearLayout formLayout = FindViewById<LinearLayout>(Resource.Id.PrescriptionInformationFormLayout);

            ViewTreeObserver vto = formLayout.ViewTreeObserver;

            vto.GlobalLayout += vto_GlobalLayout;
        }
 
        private void FrequencyTextChanged(object sender, EventArgs args)
        {
            _viewModel.Frequency = _frequency.Text;
        }
 
        private void DosageTextChanged(object sender, EventArgs args)
        {
            _viewModel.Dosage = _dosage.Text;
        }

        private void vto_GlobalLayout(object sender, EventArgs e)
        {
            if (!_init)
            {
                _dosage.Text = _viewModel.Dosage;

                _frequency.Text = _viewModel.Frequency;

                _init = true;
            }
        }
    }
}