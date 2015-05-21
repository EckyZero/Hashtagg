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

namespace Droid.Phone.Activities.Incentives.MarkCannotComplete
{
    [Activity(Label = "IncentivesCantCompleteReasonPromptActivity")]
    public class IncentiveCantCompleteReasonPromptActivity : PromptBaseActivity
    {
        private IncentiveCantCompletePickerPromptViewModel _viewModel;

        private Button _buttonOne;

        private FloatLabeledEditText _reasonField;

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
            //From Params
            _viewModel = _navigationService.GetAndRemoveParameter<IncentiveCantCompletePickerPromptViewModel>(Intent);

            _picker = FindViewById<FloatLabeledEditText>(Resource.Id.BasePromptDropDown);

            _picker.ItemSelected += picker_ItemSelected;

            _picker.TextChanged += picker_TextChanged;

            _reasonField = FindViewById<FloatLabeledEditText>(Resource.Id.BasePromptOtherField);

            _reasonField.TextChanged += _reasonField_TextChanged;

            _buttonOne = FindViewById<Button>(Resource.Id.BasePromptButtonOne);
            
            _buttonOne.SetCommand("Click", _viewModel.NextCommand);
            
            _buttonOne.Enabled = false;

            FindViewById<Button>(Resource.Id.BasePromptButtonTwo).SetCommand("Click", _viewModel.CancelCommand);
        }

        //Watch for the user to clear the field, if they do then we need to hide other, as nothing is selcted
        //TODO this will not be needed if we remove the X on pickers
        void picker_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_picker.Text))
            {
                _reasonField.Visibility = ViewStates.Gone;
            }
        }

        void _reasonField_TextChanged(object sender, EventArgs e)
        {
            _viewModel.OtherText = _reasonField.Text;
        }

        void picker_ItemSelected(PopupSpinnerEventArgs obj)
        {
            _viewModel.Model = _viewModel.IncentiveActionStep.AttestationReasons[obj.Index];
            _reasonField.Visibility  = _viewModel.IncentiveActionStep.AttestationReasons.Last().Equals(_viewModel.Model) ? ViewStates.Visible : ViewStates.Gone;
        }
        
        protected override int MainImageId
        {
            get { return Resource.Drawable.Clipboard; }
        }

        protected override string TitleText
        {
            get { return _viewModel.ContentTitle; }
        }

        protected override string DescriptionText
        {
            get { return _viewModel.ContentDetail; }
        }

        protected override string BottomButtonTwoText
        {
            get { return ApplicationResources.Cancel; }
        }

        protected override string BottomButtonOneText
        {
            get { return ApplicationResources.Next; }
        }

        protected override void SubscribeToEvents()
        {
            _viewModel.RequestPreviousPage += _viewModel_RequestPreviousPage;

            _viewModel.RequestNextPage += _viewModel_RequestNextPage;

            _viewModel.CanExecute += _viewModel_CanExecute;
        }

        void _viewModel_CanExecute(object sender, CanExecuteEventArgs e)
        {
            _buttonOne.Enabled = e.CanExecute;
        }

        void _viewModel_RequestNextPage(object sender, object e)
        {
            var nextViewModel = new IncentiveCantCompletePromptViewModel(_viewModel.IncentiveAction, _viewModel.Attest);

            _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_CANT_COMPLETE_PROMPT_PAGE, nextViewModel);
        }

        private void _viewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            GoBack();
        }

        protected override string PickerHint
        {
            get { return _viewModel.PromptPlaceholder; }
        }
        protected override IList<string> PickerData
        {
            get { return _viewModel.Options; }
        }

        protected override bool ShouldShowPicker
        {
           get { return true; }
        }

        protected override string OtherFieldHint
        {
            get { return "Other Reason"; }
        }
    }
}