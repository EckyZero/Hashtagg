using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.BL;
using Shared.Common;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsConditionsFragment : BaseFragment
    {
        private LayoutInflater _inflater;
        
        private HealthDetailsConditionPromptViewModel _viewModel;

        private bool _isFirstTab;

        private bool _isFragmentResume;

        public static string CONDITIONS_PROMPT_BACK_KEY = "conditions_prompt_back_key";

        public MyHealthDetailsConditionsFragment(HealthDetailsConditionPromptViewModel viewModel, bool isFirstTab)
        {
            _isFirstTab = isFirstTab;

            _viewModel = viewModel;
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
        private async void StayOnPromptOrNavigateToListView()
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
            //We never navigate to previous page
        }

        void _viewModel_RequestLookupPage(object sender, EventArgs e)
        {
            var nextViewModel = new GenericConditionLookupViewModel(
                IocContainer.GetContainer().Resolve<IConditionBL>(),
                IocContainer.GetContainer().Resolve<IGeolocator>());

            nextViewModel.RequestPostSelectionPage = PostSelectionPage;

            NavigationService.NavigateTo(ViewModelLocator.GENERIC_CONDITIONS_LOOKUP_PAGE, nextViewModel);
        }

        //Here we set what we want to do when a item in the lookup is selected
        protected void PostSelectionPage(IIdentifiable procedure)
        {
            NavigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, null, new ActivityFlags[] { ActivityFlags.ClearTop, ActivityFlags.SingleTop });
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState){


            BindEvents();

            //Call this before displaying screen to navigate as early as possible if needed 
            StayOnPromptOrNavigateToListView();

            var procedurePrompt = inflater.Inflate(Resource.Layout.MyHealthDetails_BasePrompt, container, false);

            procedurePrompt.FindViewById<ImageView>(Resource.Id.HealthDetailsBasePromptImage)
                .SetImageResource(Resource.Drawable.Conditions);

            procedurePrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptTitle).Text =
                _viewModel.ContentTitle;

            procedurePrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptDescription).Text =
                _viewModel.ContentDetail;

            procedurePrompt.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptFooter).Text =
                _viewModel.ContentFooter;

            var promptButton = procedurePrompt.FindViewById<Button>(Resource.Id.HealthDetailsBasePromptButtonOne);

            promptButton.Text = _viewModel.PromptTitle;

            promptButton.SetCommand("Click", _viewModel.LookupCommand);

            return procedurePrompt;
        }

        void _viewModel_RequestNextPage(object sender, object e)
        {
            var nextViewModel = new HealthDetailsConditionListViewModel();

            var nextPage = new MyHealthDetailsConditionsListFragment(nextViewModel);

            var transaction = ChildFragmentManager.BeginTransaction();

            transaction.Replace(Resource.Id.HealthDetailsBasePromptLayout, nextPage);

            transaction.AddToBackStack(MyHealthDetailsConditionsFragment.CONDITIONS_PROMPT_BACK_KEY);

            transaction.Commit();
        }
    }
} 