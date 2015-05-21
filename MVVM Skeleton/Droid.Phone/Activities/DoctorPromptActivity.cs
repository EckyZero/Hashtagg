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
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "DoctorPromptActivity")]
    public class DoctorPromptActivity : GetConnectedPromptBaseActivity<Provider>
    {
        private IDoctorPromptViewModel _viewModel;

        protected override void InitBindings()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IDoctorPromptViewModel>();
            MainApplication.VMStore.DoctorPromptVM = _viewModel;

            Button iHaveNoneButton = FindViewById<Button>(Resource.Id.GetConnectedPromptIHaveNone);
            iHaveNoneButton.SetCommand("Click", _viewModel.IDontHaveOneCommand);

            TextView doctorPromptSearchTextView = FindViewById<TextView>(Resource.Id.GetConnectedPromptSearch);
            doctorPromptSearchTextView.SetCommand("Click", _viewModel.DoctorPromptCommand);

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.GetConnectedPromptProgressBar);
            progressBar.Progress = (int)Math.Round(_viewModel.Progress*100);
        }


        protected override int MainImageId
        {
            get { return Resource.Drawable.Stethicon; }
        }

        protected override string TitleText
        {
            get { return "Tell us about your Primary Care Doctor"; }
        }

        protected override string DescriptionText
        {
            get { return "This helps us to better understand your healthcare needs"; }
        }

        protected override string SearchHint
        {
            get { return "My Doctor's name is..."; }
        }

        protected override bool ShowDoLater
        {
            get { return false; }
        }

        protected override string IHaveNoneButtonText
        {
            get { return "I don't have one"; }
        }
        protected async override void Resume()
        {
            bool shouldAppear = await MainApplication.VMStore.DoctorPromptVM.ViewShouldAppear();
            if (!shouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.DOCTOR_PROMPT_LIST_VIEW_KEY);
            }
        }
    }
}