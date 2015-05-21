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
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "PrescriptionPromptActivity")]
    public class PrescriptionPromptActivity : GetConnectedPromptBaseActivity<Prescription>
    {
        private IPrescriptionPromptViewModel _viewModel;

        protected override void InitBindings()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IPrescriptionPromptViewModel>();
            MainApplication.VMStore.PrescriptionPromptVM = _viewModel;

            Button iHaveNoneButton = FindViewById<Button>(Resource.Id.GetConnectedPromptIHaveNone);
            iHaveNoneButton.SetCommand("Click", _viewModel.IDontHaveOneCommand);

            TextView doctorPromptSearchTextView = FindViewById<TextView>(Resource.Id.GetConnectedPromptSearch);
            doctorPromptSearchTextView.SetCommand("Click", _viewModel.PrescriptionPromptCommand);

            Button doThisLaterButton = FindViewById<Button>(Resource.Id.GetConnectedPromptSkipButton);
            doThisLaterButton.SetCommand("Click", _viewModel.SkipCommand);

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.GetConnectedPromptProgressBar);
            progressBar.Progress = (int)Math.Round(_viewModel.Progress * 100);
        }


        protected override int MainImageId
        {
            get { return Resource.Drawable.Med1; }
        }

        protected override string TitleText
        {
            get { return "Are you currently taking any Medications"; }
        }

        protected override string DescriptionText
        {
            get { return "This helps us identify generics or clinical alternatives."; }
        }

        protected override string SearchHint
        {
            get { return "Ex. Ezetimbe (Zetia)"; }
        }

        protected override bool ShowDoLater
        {
            get { return true; }
        }

        protected override string IHaveNoneButtonText
        {
            get { return "I am not taking any"; }
        }
        protected async override void Resume()
        {
            bool shouldAppear = await MainApplication.VMStore.PrescriptionPromptVM.ViewShouldAppear();
            if (!shouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.PRESCRIPTION_PROMPT_LIST_VIEW_KEY);
            }
        }
    }
}