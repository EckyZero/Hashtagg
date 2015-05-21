
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
using Droid.Phone.UIHelpers;
using Droid.UIHelpers;

namespace Droid.Phone
{
	[Activity (Label = "BiometricResultDetailActivity")]			
	public class BiometricResultDetailActivity : ActionBarBaseActivity
	{
		private BiometricResultDetailViewModel _viewModel;

		protected override void OnCreate (Bundle bundle)
		{
			SetTheme(Resource.Style.ToolbarPageTheme);
			base.OnCreate (bundle);

			SetContentView(Resource.Layout.BiometricResultDetail);

			_viewModel = _navigationService.GetAndRemoveParameter<BiometricResultDetailViewModel>(Intent);

			SetupToolbar ();

			HydrateData ();
		}

		private void SetupToolbar()
		{
			var toolbar = FindViewById<Toolbar>(Resource.Id.incentive_biometricResultDetail_toolbar);
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

		private void HydrateData()
		{
			LinearLayout resultsLayout = FindViewById<LinearLayout>(Resource.Id.incentive_biometricResultDetail_layout);

			PSTextView measurementValue = FindViewById<PSTextView>(Resource.Id.biometric_resultDetailRow_title);
			measurementValue.Text = _viewModel.MeasurementValue;
			measurementValue.SetTextColor (_viewModel.SummaryStatusColor.ToDroidColor());

			PSTextView measurementDate = FindViewById<PSTextView>(Resource.Id.biometric_resultDetailRow_subtitle);
			measurementDate.Text = _viewModel.MeasurementDate;

			PSTextView statusText = FindViewById<PSTextView>(Resource.Id.biometric_resultDetailRow_status_txt);
			statusText.Text = _viewModel.SummaryStatusText;

			View statusBar = FindViewById<View> (Resource.Id.biometric_resultDetailRow_Bar);
			statusBar.SetBackgroundColor (_viewModel.SummaryStatusColor.ToDroidColor());

			PSTextView definitionText = FindViewById<PSTextView>(Resource.Id.incentive_biometricResultDetail_definition_txt);
			definitionText.Text = _viewModel.DefinitionText;

			PSTextView descriptionText = FindViewById<PSTextView>(Resource.Id.incentive_biometricResultDetail_description_txt);
			descriptionText.Text = _viewModel.Description;


			foreach (BiometricResultCellViewModel step in _viewModel.BiometricResults)
			{
				resultsLayout.AddView(step.ToView(LayoutInflater, true));
			}
		}
	}
}

