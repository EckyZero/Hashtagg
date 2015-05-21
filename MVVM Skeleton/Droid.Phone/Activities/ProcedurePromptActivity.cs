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
    [Activity(Label = "ProcedurePromptActivity")]
    public class ProcedurePromptActivity : GetConnectedPromptBaseActivity<Prescription>
    {
        private IProcedurePromptViewModel _viewModel;

        protected override int MainImageId
        {
            get { return Resource.Drawable.procedures; }
        }

        protected override string TitleText
        {
            get { return "Do you have any upcoming procedures/tests?"; }
        }

        protected override string DescriptionText
        {
            get { return "Surgical procedure, mammogram, MRI, endoscopy, etc."; }
        }

        protected override string SearchHint
        {
            get { return "Ex. Colonoscopy"; }
        }

        protected override string IHaveNoneButtonText
        {
            get { return "Not right now"; }
        }

        protected override bool ShowDoLater
        {
            get { return true; }
        }

        protected async override void Resume()
        {
            bool shouldAppear = await MainApplication.VMStore.ProcedurePromptVM.ViewShouldAppear();
            if (!shouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.PROCEDURE_PROMPT_LIST_VIEW_KEY);
            }
        }

        protected override void InitBindings()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IProcedurePromptViewModel>();
            MainApplication.VMStore.ProcedurePromptVM = _viewModel;

            Button iHaveNoneButton = FindViewById<Button>(Resource.Id.GetConnectedPromptIHaveNone);
            iHaveNoneButton.SetCommand("Click", _viewModel.NotRightNowCommand);

            TextView doctorPromptSearchTextView = FindViewById<TextView>(Resource.Id.GetConnectedPromptSearch);
            doctorPromptSearchTextView.SetCommand("Click", _viewModel.ProcedurePromptCommand);

            Button doThisLaterButton = FindViewById<Button>(Resource.Id.GetConnectedPromptSkipButton);
            doThisLaterButton.SetCommand("Click", _viewModel.SkipCommand);

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.GetConnectedPromptProgressBar);
            progressBar.Progress = (int)Math.Round(_viewModel.Progress * 100);

        }
    }
}