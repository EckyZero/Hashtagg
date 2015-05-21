using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Droid.Activities;
using Droid.Phone.UIHelpers.Incentives;
using Shared.Common;
using Shared.VM;
using Android.Graphics;
using Droid.Phone.UIHelpers.Navigation;
using Shared.BL;
using Shared.DAL;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    [Activity(Label = "IncentiveDetail")]
    public class IncentiveDetailActivity : ActionBarBaseActivity
    {
        private IncentiveDetailViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Incentive_Detail);

            _viewModel = _navigationService.GetAndRemoveParameter<IncentiveDetailViewModel>(Intent);

            _viewModel.RequestIncentiveActionDetailPage += ViewModelOnRequestIncentiveActionDetailPage;

            SetupToolbar();

            StyleSummary();

            HydrateData();
        }

        private async void ViewModelOnRequestIncentiveActionDetailPage(object sender, IncentiveAction incentiveAction)
        {
            IncentiveActionDetailViewModel viewModel = new IncentiveActionDetailViewModel();

            viewModel.IncentiveAction = incentiveAction;

            viewModel.RefreshData(() => _navigationService.NavigateTo(ViewModelLocator.INCENTIVE_ACTION_DETAIL_KEY, new IncentiveActionDetailsNavigationParameters(viewModel)));
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                OnBackPressed();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void SetupToolbar()
        {
            var toolbar = FindViewById<Toolbar>(Resource.Id.incentive_detail_toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
            SupportActionBar.Title = _viewModel.Incentive.Name;
        }

        private void StyleSummary()
        {
            View summary = FindViewById<View>(Resource.Id.incentive_detail_summary);

            if (_viewModel.Incentive.IsRequired)
            {

                View sideBar = summary.FindViewById<View>(Resource.Id.incentive_detailRow_Bar);
                TextView totalAmount = summary.FindViewById<TextView>(Resource.Id.incentive_detailRow_totalAmount);
                TextView totalType = summary.FindViewById<TextView>(Resource.Id.incentive_detailRow_totalType);
                TextView earnedDollar = summary.FindViewById<TextView>(Resource.Id.incentive_detailRow_dollar2);
                TextView earnedAmount = summary.FindViewById<TextView>(Resource.Id.incentive_detailRow_earnedAmount);

                // case 1: Active
                if (_viewModel.Incentive.IsActive && !_viewModel.Incentive.IsComplete && !_viewModel.Incentive.IsExpired && !_viewModel.Incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Active);
                    earnedDollar.SetTextColor(IncentiveColors.Active);
                    earnedAmount.SetTextColor(IncentiveColors.Active);
                }
                // case 2: Complete
                else if (!_viewModel.Incentive.IsActive && _viewModel.Incentive.IsComplete && !_viewModel.Incentive.IsExpired && !_viewModel.Incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Complete);
                    earnedDollar.SetTextColor(IncentiveColors.Complete);
                    earnedAmount.SetTextColor(IncentiveColors.Complete);

                }
                // case 3: Expired
                else if (!_viewModel.Incentive.IsActive && !_viewModel.Incentive.IsComplete && _viewModel.Incentive.IsExpired && !_viewModel.Incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Expired);
                    earnedDollar.SetTextColor(IncentiveColors.Expired);
                    earnedAmount.SetTextColor(IncentiveColors.Expired);

                }
                // case 4: Urgent
                else if (!_viewModel.Incentive.IsActive && !_viewModel.Incentive.IsComplete && !_viewModel.Incentive.IsExpired && _viewModel.Incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Urgent);
                    earnedDollar.SetTextColor(IncentiveColors.Urgent);
                    earnedAmount.SetTextColor(IncentiveColors.Urgent);
                }

                totalAmount.Text = _viewModel.AvailableAmount.ToString();

                totalType.Text = _viewModel.AvailableAmountDetail;

                earnedAmount.Text = _viewModel.EarnedAmount.ToString();
            }

            else
            {
                summary.Visibility = ViewStates.Gone;
            }
        }

        private void HydrateData()
        {
            LinearLayout actionsLayout = FindViewById<LinearLayout>(Resource.Id.incentive_detail_action_layout);

            TextView aboutDescription = FindViewById<TextView>(Resource.Id.incentive_detail_about_description_txt);
            TextView dateText = FindViewById<TextView>(Resource.Id.incentive_detail_date_txt);
            TextView dateDescription = FindViewById<TextView>(Resource.Id.incentive_detail_date_description_txt);

            aboutDescription.Text = _viewModel.AboutText;

            if (_viewModel.Incentive.IsRequired)
            {
                foreach (IncentiveAction action in _viewModel.Incentive.Actions)
                {
                    actionsLayout.AddView(action.ToView(LayoutInflater,
                        () => _viewModel.IncentiveActionCommand.Execute(action)));
                }
            }
            else
            {
                actionsLayout.Visibility = ViewStates.Gone;
            }

            if (_viewModel.Incentive.IsExpired || !_viewModel.Incentive.IsRequired)
            {
                dateText.Visibility = ViewStates.Gone;
                dateDescription.Visibility = ViewStates.Gone;
            }
            else
            {
                dateDescription.Text = _viewModel.RewardText;
            }
        }
    }
}