
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
using Droid.Activities;
using Android.Support.V7.Widget;
using Shared.VM;
using Droid.Phone.UIHelpers.Incentives;
using Shared.Common;
using Droid.Controls;
using Microsoft.Practices.Unity;
using Shared.BL;
using Droid.Phone.Activities;

namespace Droid.Phone
{
	[Activity (Label = "BiometricResultsActivity")]			
	public class BiometricResultsActivity : ActionBarBaseActivity
	{
		private BiometricResultsViewModel _viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			SetTheme(Resource.Style.ToolbarPageTheme);
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.BiometricResults);

			_viewModel = _navigationService.GetAndRemoveParameter<BiometricResultsViewModel>(Intent);

			_viewModel.RequestUpdateHeaderToReflectEmptyResults += UpdatePageToEmptyBiometricResultsView;

			SetupToolbar ();

			HydrateData ();
		}

		private void SetupToolbar()
		{
			var toolbar = FindViewById<Toolbar>(Resource.Id.incentive_biometricResults_toolbar);
			SetSupportActionBar(toolbar);

			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(true);
			SupportActionBar.Title = _viewModel.Title;
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home)
			{
				GoBack ();
				return true;
			}
			return base.OnOptionsItemSelected(item);
		}

		private void UpdatePageToEmptyBiometricResultsView()
		{
			RelativeLayout resultsLayout = FindViewById<RelativeLayout>(Resource.Id.incentive_biometricResults_summary);
			resultsLayout.Visibility = ViewStates.Gone;
		}

		private void UpdatePageToEmptyBiometricResultsView(object sender, EventArgs e)
		{
			UpdatePageToEmptyBiometricResultsView ();
		}


		private void NavigateToDetailsPage(object sender, BiometricResult biometricResult)
		{
			var detailsViewModel = new BiometricResultDetailViewModel ();
			detailsViewModel.OrganizeHistoricalData(_viewModel.GetBiometricHistoryFor(biometricResult));
			_navigationService.NavigateTo(ViewModelLocator.BIOMETRIC_RESULT_DETAIL_VIEW_KEY, detailsViewModel);
		}

		private void NavigateToMyIncentivesPage(object sender, BiometricResult biometricResult)
		{
			_navigationService.NavigateTo(ViewModelLocator.HOME_CONTAINER_KEY, new HamburgerMenuParameters(
				IocContainer.GetContainer().Resolve<IMenuItemsBL>().HasIncentives
				? MenuActionType.Incentives
				: MenuActionType.Recommendations));
		}

		private void HydrateData()
		{
			if(_viewModel.ShouldSummaryBeEmpty){
				UpdatePageToEmptyBiometricResultsView ();
			}

			LinearLayout resultsLayout = FindViewById<LinearLayout>(Resource.Id.incentive_biometricResults_layout);

			TextView description = FindViewById<TextView>(Resource.Id.incentive_biometricResults_description_txt);
			description.Text = _viewModel.Description;

			PSTextView riskFactorCount = FindViewById<PSTextView>(Resource.Id.biometric_detailRow_riskFactorCount);
			riskFactorCount.Text = _viewModel.RiskFactorCount;


			foreach (BiometricResultCellViewModel step in _viewModel.BiometricResults)
			{
				if (step.IsBiometricResult) {
					step.MoveToDetailsPageHandler -= NavigateToDetailsPage;
					step.MoveToDetailsPageHandler += NavigateToDetailsPage;
				} else {
					step.MoveToDetailsPageHandler -= NavigateToMyIncentivesPage;
					step.MoveToDetailsPageHandler += NavigateToMyIncentivesPage;
				}

				resultsLayout.AddView(step.ToView(LayoutInflater));
			}
		}
	}
}

