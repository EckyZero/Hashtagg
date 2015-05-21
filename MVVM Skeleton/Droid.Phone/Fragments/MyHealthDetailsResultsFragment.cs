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
using Shared.VM;

namespace Droid.Phone.Fragments
{
    public class MyHealthDetailsResultsFragment : BaseFragment
    {
//        private ViewGroup _container;
//        
//		private LayoutInflater _inflater;

		private bool _isFirstTab;

		private bool _isFragmentResume;

		private HealthDetailsResultPromptViewModel _viewModel;

		public static string RESULTS_PROMPT_BACK_KEY = "results_prompt_back_key";

		public MyHealthDetailsResultsFragment(HealthDetailsResultPromptViewModel viewModel, bool isFirstTab){
			_viewModel = viewModel;
			_isFirstTab = isFirstTab;
		}
			
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
//            _container = container;
//            _inflater = inflater;
			BindEvents();

			//Call this before displaying screen to navigate as early as possible if needed 
			StayOnPromptOrNavigateToListView();


             var resultsPromptView = inflater.Inflate(Resource.Layout.MyHealthDetails_BasePrompt, container, false);

			resultsPromptView.FindViewById<ImageView>(Resource.Id.HealthDetailsBasePromptImage)
				.SetImageResource(Resource.Drawable.procedures);

			resultsPromptView.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptTitle).Text =
				_viewModel.ContentTitle;

			resultsPromptView.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptDescription).Text =
				_viewModel.ContentDetail;

			resultsPromptView.FindViewById<TextView>(Resource.Id.HealthDetailsBasePromptFooter).Text =
				_viewModel.ContentFooter;

			var lookupButton = resultsPromptView.FindViewById<Button>(Resource.Id.HealthDetailsBasePromptButtonOne);
			lookupButton.Text = _viewModel.PromptTitle;


			return resultsPromptView;
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
		}

		void _viewModel_RequestLookupPage(object sender, EventArgs e)
		{
			//TODO: add when Setup a screening is available in a future sprint
		}

		void _viewModel_RequestNextPage(object sender, object e)
		{
			var nextViewModel = new HealthDetailsResultListViewModel();

			var nextPage = new MyHealthDetailsResultsListFragment(nextViewModel);

			var transaction = ChildFragmentManager.BeginTransaction();

			transaction.Replace(Resource.Id.HealthDetailsBasePromptLayout, nextPage);

			transaction.AddToBackStack(RESULTS_PROMPT_BACK_KEY);

			transaction.Commit();
		}

    }
}