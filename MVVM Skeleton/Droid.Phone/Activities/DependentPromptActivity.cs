using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
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
    [Activity(Label = "DependentPromptActivity")]
    public class DependentPromptActivity : BaseActivity
    {
        private IDependentPromptViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.DependentPrompt);

            InitBindings();

            Resume();


        }

        protected void InitBindings()
        {
            _viewModel = IocContainer.GetContainer().Resolve<IDependentPromptViewModel>();
            MainApplication.VMStore.DependentPromptVM = _viewModel;

            Button iHaveNoneButton = FindViewById<Button>(Resource.Id.DependentPromptIHaveNone);
            iHaveNoneButton.SetCommand("Click", _viewModel.NotRightNowCommand);

            Button addDependentInformationButton = FindViewById<Button>(Resource.Id.DependentPromptYesButton);
            addDependentInformationButton.SetCommand("Click", _viewModel.DependentInformationCommand);

            Button skipButton = FindViewById<Button>(Resource.Id.DependentDoThisLaterButton);
            skipButton.SetCommand("Click", _viewModel.SkipCommand);

            ProgressBar progressBar = FindViewById<ProgressBar>(Resource.Id.DependentPromptProgressBar);
            progressBar.Progress = (int)Math.Round(_viewModel.Progress * 100);

        }

        protected async void Resume()
        {
            bool shouldAppear = await MainApplication.VMStore.DependentPromptVM.ViewShouldAppear();
            
            if (!shouldAppear)
            {
                _navigationService.NavigateTo(ViewModelLocator.DEPENDENTS_PROMPT_LIST_VIEW_KEY);
            }
        }
    }
}