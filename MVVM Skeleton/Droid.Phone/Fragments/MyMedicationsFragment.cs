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
    public class MyMedicationsFragment : BaseFragment
    {
        private MedicationPromptViewModel _viewModel;

        private bool _isFragmentResume;

        public static string MEDICATIONS_PROMPT_BACK_KEY = "medication_prompt_back_key";

        public MyMedicationsFragment(MedicationPromptViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private async void StayOnPromptOrNavigateToListView()
        {
            await _viewModel.WillAppear();
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




        //Here we set what we want to do when a item in the lookup is selected
        #region Lookup

        void _viewModel_RequestLookupPage(object sender, EventArgs e)
        {
            var nextViewModel = new GenericPrescriptionLookupViewModel(
                IocContainer.GetContainer().Resolve<IPrescriptionBL>(),
                IocContainer.GetContainer().Resolve<IGeolocator>());

            nextViewModel.RequestPostSelectionPage = RequestPostSelectionPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PRESCRIPTION_LOOKUP_PAGE, nextViewModel);
        }

        private void RequestPostSelectionPage(Prescription prescription)
        {
            var postLookupSelectViewModel =
                new GenericPrescriptionInformationViewModel(
                    IocContainer.GetContainer().Resolve<IPrescriptionBL>(),
                    new PatientPrescription(prescription));

            postLookupSelectViewModel.RequestPostSaveReturnPage += postLookupSelectViewModel_RequestPostSaveReturnPage;

            postLookupSelectViewModel.RequestCancelPage += postLookupSelectViewModel_RequestCancelPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_PRESCRIPTION_INFORMATION_PAGE,
                postLookupSelectViewModel);
        }

        private void postLookupSelectViewModel_RequestCancelPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null,
                new ActivityFlags[] {ActivityFlags.ClearTop, ActivityFlags.SingleTop});
        }

        private void postLookupSelectViewModel_RequestPostSaveReturnPage(object sender, EventArgs e)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null,
                new ActivityFlags[] {ActivityFlags.ClearTop, ActivityFlags.SingleTop});
        }
        #endregion

        void _viewModel_RequestNextPage(object sender, object e)
        {
            var nextViewModel = new MedicationListViewModel();

            var nextPage = new MedicationListFragment(nextViewModel);

            var transaction = ChildFragmentManager.BeginTransaction();

            transaction.Replace(Resource.Id.HealthDetailsBasePromptLayout, nextPage);

            transaction.AddToBackStack(MEDICATIONS_PROMPT_BACK_KEY);

            transaction.Commit();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            BindEvents();

            StayOnPromptOrNavigateToListView();

            //Just In case we will show, main thread keep getting UI Ready

            var medicationPrompt = inflater.Inflate(Resource.Layout.MyHealthDetails_BasePrompt, container, false);

            medicationPrompt.FindViewById<ImageView>(Resource.Id.HealthDetailsBasePromptImage)
                .SetImageResource(Resource.Drawable.Med1);

            medicationPrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptTitle).Text =
                _viewModel.ContentTitle;

            medicationPrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptDescription).Text =
                _viewModel.ContentDetail;

            medicationPrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptFooter).Text =
                _viewModel.ContentFooter;

            var lookupButton = medicationPrompt.FindViewById<Button>(Resource.Id.HealthDetailsBasePromptButtonOne);
            lookupButton.Text = _viewModel.PromptTitle;

            lookupButton.SetCommand("Click", _viewModel.LookupCommand);

            return medicationPrompt;
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

                StayOnPromptOrNavigateToListView();
            }

            base.OnResume();
        }
    }
}