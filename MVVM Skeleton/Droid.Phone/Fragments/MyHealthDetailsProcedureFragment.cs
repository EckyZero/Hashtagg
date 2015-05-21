using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.BL;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using Fragment = Android.Support.V4.App.Fragment;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsProcedureFragment : BaseFragment
    {   
        private HealthDetailsProcedurePromptViewModel _viewModel;

        private bool _isFragmentResume;

        private bool _isFirstTab;

        public static string PROCEDURE_PROMPT_BACK_KEY = "procedure_prompt_back_key";
        
        public MyHealthDetailsProcedureFragment(HealthDetailsProcedurePromptViewModel viewModel, bool isFirstTab)
        {
            _viewModel = viewModel;

            _isFirstTab = isFirstTab;
        }

        private async void VerifyWillShow()
        {
            await _viewModel.WillAppear(_isFirstTab);
            
        }

        private void BindEvents()
        {
            _viewModel.RequestNextPage += _viewModel_RequestNextPage;

            _viewModel.RequestLookupPage += _viewModel_RequestLookupPage;

            _viewModel.RequestPreviousPage += _viewModel_RequestPreviousPage;
        }

        void _viewModel_RequestPreviousPage(object sender, EventArgs e)
        {
            //We never navigate before this page
        }

        void _viewModel_RequestLookupPage(object sender, EventArgs e)
        {
            var nextViewModel = new GenericProcedureLookupViewModel(
                IocContainer.GetContainer().Resolve<IProcedureBL>(), IocContainer.GetContainer().Resolve<IGeolocator>());

            nextViewModel.OnSelect = OnSelect;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PROCEDURE_LOOKUP_PAGE, nextViewModel);
        }

        //Here we set what we want to do when a item in the lookup is selected
        #region OnSelect
        protected void OnSelect(IIdentifiable procedure)
        {
            var postLookupSelectViewModel =
                new GenericProcedurePromptInformationViewModel(IocContainer.GetContainer().Resolve<IProcedureBL>(), new PatientProcedure(procedure as Procedure));

            postLookupSelectViewModel.RequestPostSaveReturnPage += postLookupSelectViewModel_RequestPostSaveReturnPage;

            postLookupSelectViewModel.RequestCancelPage += postLookupSelectViewModel_RequestCancelPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PROCEDURE_INFORMATION_PAGE, postLookupSelectViewModel);
        }

        private void postLookupSelectViewModel_RequestCancelPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null, new ActivityFlags[] { ActivityFlags.ClearTop, ActivityFlags.SingleTop });
        }

        private void postLookupSelectViewModel_RequestPostSaveReturnPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null, new ActivityFlags[] { ActivityFlags.ClearTop, ActivityFlags.SingleTop });
        }
        #endregion

        void _viewModel_RequestNextPage(object sender, object e)
        {
            var nextViewModel = new HealthDetailsProcedureListViewModel();

            var nextPage = new MyHealthDetailsProcedureListFragment(nextViewModel);

            var transaction = ChildFragmentManager.BeginTransaction();

            transaction.Replace(Resource.Id.HealthDetailsBasePromptLayout, nextPage);

            transaction.AddToBackStack(MyHealthDetailsProcedureFragment.PROCEDURE_PROMPT_BACK_KEY);

            transaction.Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            BindEvents();

            VerifyWillShow();

            //Just In case we will show, main thread keep getting UI Ready

            var procedurePrompt = inflater.Inflate(Resource.Layout.MyHealthDetails_BasePrompt, container, false);

            procedurePrompt.FindViewById<ImageView>(Resource.Id.HealthDetailsBasePromptImage)
                .SetImageResource(Resource.Drawable.procedures);

            procedurePrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptTitle).Text =
                _viewModel.ContentTitle;

            procedurePrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptDescription).Text =
                _viewModel.ContentDetail;

            procedurePrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptFooter).Text =
                _viewModel.ContentFooter;

            var lookupButton = procedurePrompt.FindViewById<Button>(Resource.Id.HealthDetailsBasePromptButtonOne);
            lookupButton.Text = _viewModel.PromptTitle;

            lookupButton.SetCommand("Click",_viewModel.LookupCommand);

            return procedurePrompt;
        }

        public override void OnPause()
        {
            _isFragmentResume = true;
            base.OnPause();
        }

        public override void OnResume()
        {

            if (_isFragmentResume)
            {
                _isFragmentResume = false;

                VerifyWillShow();
            }

            base.OnResume();
        }
    }
}