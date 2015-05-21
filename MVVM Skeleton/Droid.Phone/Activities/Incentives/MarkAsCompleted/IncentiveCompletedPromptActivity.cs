using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight.Command;

namespace Droid.Phone.Activities.Incentives.MarkAsCompleted
{
    [Activity(Label = "IncentivesCompletedPromptActivity")]
    public class IncentiveCompletedPromptActivity : PromptBaseActivity
    {
        private IncentiveCompletedPromptViewModel _viewModel;
        
        private TextView _searchButton;

        private FloatLabeledEditText _picker;

        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            
            base.OnCreate(bundle);
            
            var toolbar = FindViewById<Toolbar>(Resource.Id.BasePromptToolbar);
            
            SetSupportActionBar(toolbar);
            
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            
            SupportActionBar.Title = ApplicationResources.DoctorName;
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
            //From Params
            _viewModel = new IncentiveCompletedPromptViewModel(_navigationService.GetAndRemoveParameter<IncentiveAction>(Intent));

            //Search Button which calls lookup, hidden if picker is shown until a object is selected
            _searchButton  = FindViewById<TextView>(Resource.Id.BasePromptSearch);
            
            _searchButton.SetCommand("Click", _viewModel.LookupCommand);
            
            FindViewById<Button>(Resource.Id.BasePromptButtonTwo).SetCommand("Click", _viewModel.CancelCommand);
            
            _picker = FindViewById<FloatLabeledEditText>(Resource.Id.BasePromptDropDown);

            _picker.ItemSelected += picker_ItemSelected;

            _picker.TextChanged += picker_TextChanged;
        }

        void picker_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_picker.Text))
            {
                _searchButton.Visibility = ViewStates.Gone;
            }
        }

        void picker_ItemSelected(PopupSpinnerEventArgs args)
        {
            _viewModel.SelectedText = args.ItemSelected.ToString();

            _searchButton.Visibility = ViewStates.Visible;
        }

        protected override int MainImageId
        {
            get { return Resource.Drawable.Stethicon; }
        }

        protected override string TitleText
        {
            get { return ApplicationResources.CongratsOnCompleting; }
        }

        protected override string DescriptionText
        {
            get { return ApplicationResources.LetsGetVerificationDetails; }
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
            string selectedProcedureName = _viewModel.SelectedText;
			
            IncentiveActionProcedure procedure = null;
			
            if(!string.IsNullOrWhiteSpace(selectedProcedureName))
			{
				procedure = _viewModel.IncentiveAction.Procedures.Single(p => p.Description == selectedProcedureName);
			}
            
            var nextParams = new IncentiveCalendarPromptParameters()
            {
                SelectedIncentiveAction = _viewModel.IncentiveAction, 
                SelectedIncentiveActionProcedure = procedure,
                SelectedProvider = _viewModel.Model
            };
            
            _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_COMPLETED_INFORMATION_PAGE, nextParams);
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