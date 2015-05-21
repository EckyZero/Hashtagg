using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight.Command;

namespace Droid.Phone.Activities.Incentives.MarkCannotComplete
{
    [Activity(Label = "IncentivesCantCompletePromptActivity", WindowSoftInputMode = (SoftInput.StateHidden|SoftInput.AdjustResize))]
    public class IncentivesCantCompletePromptActivity : PromptBaseActivity
    {
        private IncentiveCantCompletePromptViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);

            base.OnCreate(bundle);

            var toolbar = FindViewById<Toolbar>(Resource.Id.BasePromptToolbar);

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

        protected override void InitBindings()
        {
            _viewModel = _navigationService.GetAndRemoveParameter<IncentiveCantCompletePromptViewModel>(Intent);
            
            FindViewById<TextView>(Resource.Id.BasePromptSearch).SetCommand("Click", _viewModel.LookupCommand);
            
            FindViewById<Button>(Resource.Id.BasePromptButtonTwo).SetCommand("Click", _viewModel.CancelCommand);
        }

        protected override int MainImageId
        {
            get { return Resource.Drawable.Stethicon; }
        }

        protected override string TitleText
        {
            get { return _viewModel.ContentTitle; }
        }

        protected override string DescriptionText
        {
            get { return _viewModel.ContentDetail; }
        }

        protected override string SearchHint
        {
            get { return ApplicationResources.MyDoctorsNameIs; }
        }

        protected override string BottomButtonTwoText
        {
            get { return ApplicationResources.Cancel; }
        }

        protected override void SubscribeToEvents()
        {
            _viewModel.RequestLookupPage += _viewModel_RequestLookupPage;

            _viewModel.RequestPreviousPage += _viewModel_RequestPreviousPage;

            _viewModel.RequestNextPage += _viewModel_RequestNextPage;
        }

        void _viewModel_RequestNextPage(object sender, object e)
        {
            var nextViewModel = new IncentiveCantCompletePromptResponseViewModel(_viewModel.Model,
                _viewModel.IncentiveAction, _viewModel.Attest);

            _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_CANT_COMPLETE_DOCTOR_VERIFY_PAGE, nextViewModel);
        }

        private void _viewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            GoBack();
        }

        private void _viewModel_RequestLookupPage(object sender, EventArgs e)
        {
            _navigationService.NavigateTo(ViewModelLocator.DOCTOR_LOOKUP_VIEW_KEY, new DoctorLookupControllerParameters()
            {
                OnPatientSelected = null,
                OnPatientSelectedAndReturn = _viewModel.OnLookupDismissed
            });
        }

        protected override string PickerHint
        {
            get { return _viewModel.PickerPlaceholder; }
        }

        protected override IList<string> PickerData
        {
            get { return _viewModel.PickerData; }
        }

        protected override bool ShouldShowPicker
        {
           get { return _viewModel.ShouldShowPicker ; }
        }

        protected override bool ShouldShowLookupButton
        {
            get { return !_viewModel.ShouldShowPicker; }
        }
    }
}