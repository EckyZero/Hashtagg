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
using Droid.Phone.UIHelpers.Incentives;
using Shared.Common;
using Shared.VM;
using System.Threading.Tasks;
using Android.Graphics;
using Droid.Phone.UIHelpers.Navigation;

namespace Droid.Phone.Fragments
{
    public class IncentivesSummaryFragment : BaseFragment
    {
        enum Subsections { UncompletedIncentives, CompletedIncentives, RecommendedActions };

        private IncentiveSummaryViewModel _viewModel;

        private LayoutInflater _inflater;

        private ViewGroup _container;


        public IncentivesSummaryFragment(IncentiveSummaryViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.RequestIncentiveDetailPage += ViewModelOnRequestIncentiveDetailPage;
            _viewModel.RequestIncentiveActionDetailPage += ViewModelOnRequestIncentiveActionDetailPage;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _inflater = inflater;
            _container = container;

            View incentivesView = _inflater.Inflate(Resource.Layout.IncentivesSummary, container, false);

            LinearLayout incentivesLayout = incentivesView.FindViewById<LinearLayout>(Resource.Id.incentiveSummary_Layout);

            View header = GenerateStyledHeaderSummary();
            if (header != null)
            {
                incentivesLayout.AddView(header);
            }

            List<View> subsections = GenerateHydratedSubsections();

            subsections.ForEach(x => incentivesLayout.AddView(x));

            return incentivesView;
        }


        private void ViewModelOnRequestIncentiveDetailPage(object sender, Incentive incentive)
        {
            IncentiveDetailViewModel viewModel = new IncentiveDetailViewModel();
            viewModel.Incentive = incentive;
            NavigationService.NavigateTo(ViewModelLocator.INCENTIVE_DETAIL_KEY, viewModel);
        }

        private void ViewModelOnRequestIncentiveActionDetailPage(object sender, IncentiveAction incentiveAction)
        {
            IncentiveActionDetailViewModel viewModel = new IncentiveActionDetailViewModel();
            viewModel.IncentiveAction = incentiveAction;
            viewModel.RefreshData(() => NavigationService.NavigateTo(ViewModelLocator.INCENTIVE_ACTION_DETAIL_KEY, new IncentiveActionDetailsNavigationParameters(viewModel)));
        }

        /// <summary>
        /// Builds out the ordered subsections, with data, that will appear on the incentives page
        /// </summary>
        private List<View> GenerateHydratedSubsections()
        {
            List<View> subsections = new List<View>();

            List<View> incentivesUI = new List<View>();
            List<View> reccomendedActionUI = new List<View>();

            foreach (Incentive incentive in _viewModel.OrderedIncentivesAndRecommendedActions)
            {

                if (incentive.IsRequired)
                {
                    View view = incentive.ToView(_inflater, () => _viewModel.IncentiveCommand.Execute(incentive));
                    incentivesUI.Add(view);
                    if (!incentive.IsComplete)
                    {
                        foreach (IncentiveAction action in incentive.Actions)
                        { 
                            incentivesUI.Add(action.ToView(_inflater,
                                () => _viewModel.IncentiveActionCommand.Execute(action)));
                        }
                    }
                }
                else
                {
                    View view = incentive.ToView(_inflater, () => _viewModel.IncentiveActionCommand.Execute(incentive.Actions[0]));
                    reccomendedActionUI.Add(view);
                }
            }


            View completed = GenerateStyledSubsectionSkeleton(Subsections.CompletedIncentives);
            LinearLayout completedContent =
                completed.FindViewById<LinearLayout>(Resource.Id.incentiveSummary_subsection_content);
            
            View uncompleted = GenerateStyledSubsectionSkeleton(Subsections.UncompletedIncentives);
            LinearLayout uncompletedContent =
                uncompleted.FindViewById<LinearLayout>(Resource.Id.incentiveSummary_subsection_content);

            View recommended = GenerateStyledSubsectionSkeleton(Subsections.RecommendedActions);
            LinearLayout recommendedContent =
                recommended.FindViewById<LinearLayout>(Resource.Id.incentiveSummary_subsection_content);

            // case 1: only completed incentives
            if (_viewModel.Incentives.Count > 0 && _viewModel.Incentives.All(x => x.IsComplete) && 
                !_viewModel.RecommendedActions.Any(x => x.IsActive || x.IsComplete))
            {
                incentivesUI.ForEach(x => completedContent.AddView(x));

                subsections.Add(completed);
            }

            // case 2: at least one uncompleted incentive
            else if (_viewModel.Incentives.Any(x => x.IsActive) &&
                     !_viewModel.RecommendedActions.Any(x => x.IsActive || x.IsComplete))
            {
                incentivesUI.ForEach(x => uncompletedContent.AddView(x));

                subsections.Add(uncompleted);
            }

            // case 3: only recommended actions
            else if (_viewModel.RecommendedActions.Any(x => x.IsActive || x.IsComplete) && 
                !_viewModel.Incentives.Any(x => x.IsActive || x.IsComplete))
            {
                reccomendedActionUI.ForEach(x => recommendedContent.AddView(x));

                subsections.Add(recommended);            
            }

            // case 4: all completed incentives and at least one recommended action
            else if (_viewModel.Incentives.Count > 0 && _viewModel.Incentives.All(x => x.IsComplete) && 
                _viewModel.RecommendedActions.Any(x => x.IsActive || x.IsComplete))
            {
                reccomendedActionUI.ForEach(x => recommendedContent.AddView(x));
                incentivesUI.ForEach(x => completedContent.AddView(x));

                subsections.Add(recommended);
                subsections.Add(completed);
            }

            // case 5: at least one uncompleted incentive and at least one recommended action
            else if (_viewModel.Incentives.Any(x => x.IsActive) && 
                _viewModel.RecommendedActions.Any(x => x.IsActive || x.IsComplete))
            {
                incentivesUI.ForEach(x => uncompletedContent.AddView(x));
                reccomendedActionUI.ForEach(x => recommendedContent.AddView(x));

                subsections.Add(uncompleted);
                subsections.Add(recommended);
            }

            return subsections;
        }

        /// <summary>
        /// Generates a correctly styled subsection based on the type
        /// </summary>
        private View GenerateStyledSubsectionSkeleton(Subsections ssType)
        {
            View subsection = _inflater.Inflate(Resource.Layout.Incentives_Summary_Subsection, null, false);

            ImageView icon = subsection.FindViewById<ImageView>(Resource.Id.incentiveSummary_subsection_icon);
            TextView title = subsection.FindViewById<TextView>(Resource.Id.incentiveSummary_subsection_title);
            TextView description = subsection.FindViewById<TextView>(Resource.Id.incentiveSummary_subsection_description);
            RelativeLayout rect = subsection.FindViewById<RelativeLayout>(Resource.Id.incentiveSummary_subsection_rect);
            RelativeLayout tri = subsection.FindViewById<RelativeLayout>(Resource.Id.incentiveSummary_subsection_tri);

            switch (ssType)
            {
                case Subsections.UncompletedIncentives:
                    icon.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.IncentiveCoins));
                    title.Text = ApplicationResources.IncentiveSummaryUncompletedTitle;
                    title.SetTextColor(Color.ParseColor("#848745"));
                    description.Text = ApplicationResources.IncentiveSummaryUncompletedSubtitle;
                    rect.Background = Resources.GetDrawable(Resource.Drawable.greenRectangle);
                    tri.Background = Resources.GetDrawable(Resource.Drawable.greenTriangle);
                    break;
                case Subsections.CompletedIncentives:
                    icon.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.IncentiveCoins));
                    title.Text = ApplicationResources.IncentiveSummaryCompletedTitle;
                    title.SetTextColor(Color.ParseColor("#848745"));
                    description.Visibility = ViewStates.Gone;
                    rect.Background = Resources.GetDrawable(Resource.Drawable.greenRectangle);
                    tri.Background = Resources.GetDrawable(Resource.Drawable.greenTriangle);
                    break;
                case Subsections.RecommendedActions:
                    icon.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.RecommendedHeart));
                    title.Text = ApplicationResources.RecommendationSummaryTitle;
                    title.SetTextColor(Color.ParseColor("#FFFFFF"));
                    description.Text = ApplicationResources.RecommendationSummarySubtitle;
                    rect.Background = Resources.GetDrawable(Resource.Drawable.blueRectangle);
                    tri.Background = Resources.GetDrawable(Resource.Drawable.blueTriangle);
                    break;
            }

            return subsection;
        }

        /// <summary>
        /// Generates the header at the top, with data
        /// </summary>
        private View GenerateStyledHeaderSummary()
        {
            // case 1: at least one uncompleted incentive
            if (_viewModel.Incentives.Any(x => x.IsActive))
            {
                View header = _inflater.Inflate(Resource.Layout.Incentives_Summary_Uncompleted_Header, null, false);

                TextView incentiveCount = header.FindViewById<TextView>(Resource.Id.incentives_uncompletedHeader_incentiveActionCount);
                TextView differentialAmount = header.FindViewById<TextView>(Resource.Id.incentives_uncompletedHeader_premiumDifferentialAmount);
                TextView accountCreditAmount = header.FindViewById<TextView>(Resource.Id.incentives_uncompletedHeader_accountCreditAmount);

                incentiveCount.Text = _viewModel.UncompletedActionCount.ToString();
                differentialAmount.Text = String.Format("${0}", _viewModel.EarnedOffPremiumAmount.ToString());
                accountCreditAmount.Text = String.Format("${0}", _viewModel.EarnedPayoutAmout.ToString());

                return header;
            }
            else
            {
                View header = _inflater.Inflate(Resource.Layout.Incentives_Summary_Completed_Header, null, false);

                TextView recommendationCount = header.FindViewById<TextView>(Resource.Id.incentives_completedHeader_recActionCount);
                TextView differentialAmount = header.FindViewById<TextView>(Resource.Id.incentives_completedHeader_premiumDifferentialAmount);
                TextView accountCreditAmount = header.FindViewById<TextView>(Resource.Id.incentives_completedHeader_accountCreditAmount);
                LinearLayout incentiveSection = header.FindViewById<LinearLayout>(Resource.Id.incentives_completedHeader_topSection);
                View seperator = header.FindViewById<View>(Resource.Id.incentives_completedHeader_seperator);
                LinearLayout recommendedSection = header.FindViewById<LinearLayout>(Resource.Id.incentives_completedHeader_bottomSection);

                recommendationCount.Text = _viewModel.UncompletedActionCount.ToString();
                differentialAmount.Text = String.Format("${0}", _viewModel.EarnedOffPremiumAmount.ToString());
                accountCreditAmount.Text = String.Format("${0}", _viewModel.EarnedPayoutAmout.ToString());

                // case 2: only completed incentives
                if (_viewModel.Incentives.Count > 0 && _viewModel.Incentives.All(x => x.IsComplete) &&
                    !_viewModel.RecommendedActions.Any(x => x.IsActive || x.IsComplete))
                {
                    seperator.Visibility = ViewStates.Gone;
                    recommendedSection.Visibility = ViewStates.Gone;
                }

                // case 3: only recommended actions
                else if (!_viewModel.Incentives.Any(x => x.IsActive || x.IsComplete))
                {
                    incentiveSection.Visibility = ViewStates.Gone;
                    seperator.Visibility = ViewStates.Gone;
                }
                
                return header;
            }

            return null;
        }
    }
}