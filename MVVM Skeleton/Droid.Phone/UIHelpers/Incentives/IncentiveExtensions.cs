using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Droid.Controls;
using Droid.UIHelpers;
using Shared.Common;
using Shared.VM;

namespace Droid.Phone.UIHelpers.Incentives
{
    public static class IncentiveExtensions
    {
        public static View ToView(this Incentive incentive, LayoutInflater inflater, Action action)
        {
            if (incentive.IsRequired)
            {
                View incentiveView = inflater.Inflate(Resource.Layout.Incentives_IncentiveRow, null, false);

                incentiveView.Click += (sender, args) => action.Invoke();

                View sideBar = incentiveView.FindViewById<View>(Resource.Id.incentiveRow_Bar);
                View bottomBar = incentiveView.FindViewById<View>(Resource.Id.incentiveRow_Bottom);
                TextView description = incentiveView.FindViewById<TextView>(Resource.Id.incentiveRow_Description);
                TextView dateInfo = incentiveView.FindViewById<TextView>(Resource.Id.incentiveRow_DateInfo);
                TextView price = incentiveView.FindViewById<TextView>(Resource.Id.incentiveRow_Price);
                TextView payType = incentiveView.FindViewById<TextView>(Resource.Id.incentiveRow_PayType);
                LinearLayout priceLayout = incentiveView.FindViewById<LinearLayout>(Resource.Id.incentiveRow_PriceLayout);
                ImageView arrow = incentiveView.FindViewById<ImageView>(Resource.Id.incentiveRow_Arrow);

                // case 1: Active
                if (incentive.IsActive && !incentive.IsComplete && !incentive.IsExpired && !incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Active);
                }
                // case 2: Complete
                else if (!incentive.IsActive && incentive.IsComplete && !incentive.IsExpired && !incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Complete);

                }
                // case 3: Expired
                else if (!incentive.IsActive && !incentive.IsComplete && incentive.IsExpired && !incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Expired);

                }
                // case 4: Urgent
                else if (!incentive.IsActive && !incentive.IsComplete && !incentive.IsExpired && incentive.IsUrgent())
                {
                    sideBar.SetBackgroundColor(IncentiveColors.Urgent);
                }

                bottomBar.SetBackgroundColor(incentive.IsActive ? Color.ParseColor("#EABE7C") : Color.ParseColor("#ECE4B7"));

                description.Text = incentive.Name;

                dateInfo.Text = incentive.Subtitle;
                dateInfo.Visibility = String.IsNullOrWhiteSpace(incentive.Subtitle)
                    ? ViewStates.Gone
                    : ViewStates.Visible;

                price.Text = incentive.Reward.FormattedAward;

                payType.Text = incentive.Reward.TypeLabel;

                priceLayout.Visibility = string.IsNullOrWhiteSpace(incentive.Reward.FormattedAward)
                    ? ViewStates.Gone
                    : ViewStates.Visible;

                return incentiveView;
            }

            else
            {
                View recommendedView = inflater.Inflate(Resource.Layout.Incentives_RecommendedRow, null, false);

                recommendedView.Click += (sender, args) => action.Invoke();

                ImageView icon = recommendedView.FindViewById<ImageView>(Resource.Id.recommendedRow_Icon);
                TextView description = recommendedView.FindViewById<TextView>(Resource.Id.recommendedRow_Description);
                TextView dateInfo = recommendedView.FindViewById<TextView>(Resource.Id.recommendedRow_DateInfo);
                ImageView arrow = recommendedView.FindViewById<ImageView>(Resource.Id.recommendedRow_Arrow);

                icon.SetImageDrawable(incentive.IsComplete ? 
                    recommendedView.Resources.GetDrawable(Resource.Drawable.GreenCheckBox) : 
                    recommendedView.Resources.GetDrawable(Resource.Drawable.EmptyCheckBox));

                description.Text = incentive.Name;

                dateInfo.Text = incentive.Subtitle;
                dateInfo.Visibility = String.IsNullOrWhiteSpace(incentive.Subtitle)
                    ? ViewStates.Gone
                    : ViewStates.Visible;

                return recommendedView;
            }
        }

        public static View ToView(this IncentiveAction incentiveAction, LayoutInflater inflater, Action action)
        {
            View actionView = inflater.Inflate(Resource.Layout.Incentives_ActionRow, null, false);

            actionView.Click += (sender, args) => action.Invoke();

            ImageView icon = actionView.FindViewById<ImageView>(Resource.Id.actionRow_Icon);
            TextView description = actionView.FindViewById<TextView>(Resource.Id.actionRow_Description);
            TextView dateInfo = actionView.FindViewById<TextView>(Resource.Id.actionRow_DateInfo);
            TextView price = actionView.FindViewById<TextView>(Resource.Id.actionRow_Price);
            TextView payType = actionView.FindViewById<TextView>(Resource.Id.actionRow_PayType);
            LinearLayout priceLayout = actionView.FindViewById<LinearLayout>(Resource.Id.actionRow_PriceLayout);
            ImageView arrow = actionView.FindViewById<ImageView>(Resource.Id.actionRow_Arrow);

            icon.SetImageDrawable(incentiveAction.IsComplete ?
                actionView.Resources.GetDrawable(Resource.Drawable.GreenCheckBox) :
                actionView.Resources.GetDrawable(Resource.Drawable.EmptyCheckBox));

            description.Text = incentiveAction.Name;

            dateInfo.Text = incentiveAction.Subtitle;
            dateInfo.Visibility = String.IsNullOrWhiteSpace(incentiveAction.Subtitle)
                ? ViewStates.Gone
                : ViewStates.Visible;
            description.SetTextColor(incentiveAction.IsUrgent() ? IncentiveColors.Urgent : Color.ParseColor("#555555"));

            price.Text = incentiveAction.Reward.FormattedAward;

            payType.Text = incentiveAction.Reward.TypeLabel;

            priceLayout.Visibility = string.IsNullOrWhiteSpace(incentiveAction.Reward.FormattedAward)
                ? ViewStates.Gone
                : ViewStates.Visible;

            return actionView;
        }

        public static View ToView(this IncentiveActionStepViewModel step, LayoutInflater inflater)
        {
            View actionView = inflater.Inflate(Resource.Layout.Incentives_Action_Button, null, false);

            Button standardButton = actionView.FindViewById<Button>(Resource.Id.incentive_action_button);
            LinearLayout textButtonLayout = actionView.FindViewById<LinearLayout>(Resource.Id.incentive_actionDetail_TextButton);
            Button textButton1 = actionView.FindViewById<Button>(Resource.Id.incentive_actionDetail_Action1);
            TextView textButtonPipe = actionView.FindViewById<TextView>(Resource.Id.incentive_actionDetail_ActionMiddlePipe);
            Button textButton2 = actionView.FindViewById<Button>(Resource.Id.incentive_actionDetail_Action2);

            if (step.CellType == IncentiveActionStepCellType.StandardButton)
            {
                standardButton.Text = step.ButtonOneLabel;
                standardButton.Click += (sender, args) => step.ExecuteStepOne(step, new EventArgs());

                textButtonLayout.Visibility = ViewStates.Gone;
            }
            else if (step.CellType == IncentiveActionStepCellType.TextButton)
            {
                textButton1.Text = step.ButtonOneLabel;
                textButton1.Click += (sender, args) => step.ExecuteStepOne(step, new EventArgs());

                standardButton.Visibility = ViewStates.Gone;

                textButtonPipe.Visibility = ViewStates.Gone;
                textButton2.Visibility = ViewStates.Gone;
            }
            else if (step.CellType == IncentiveActionStepCellType.TextTwoButtons)
            {
                textButton1.Text = step.ButtonOneLabel;
                textButton1.Click += (sender, args) => step.ExecuteStepOne(step, new EventArgs());

                textButton2.Text = step.ButtonTwoLabel;
                textButton2.Click += (sender, args) => step.ExecuteStepTwo(step, new EventArgs());

                standardButton.Visibility = ViewStates.Gone;
            }

            return actionView;
        }

		public static View ToView(this BiometricResultCellViewModel biometricVm, LayoutInflater inflater, bool isDetailsView = false)
		{

			if (biometricVm.IsBiometricResult) {
				View biometricResultView = inflater.Inflate (Resource.Layout.BiometricResultCell, null, false);

				PSTextView descriptionText = biometricResultView.FindViewById<PSTextView> (Resource.Id.biometricRow_Description);

				if (isDetailsView) {
					descriptionText.Visibility = ViewStates.Gone;
				} else {
					descriptionText.Text = biometricVm.Title;
				}

				PSTextView statusText = biometricResultView.FindViewById<PSTextView> (Resource.Id.biometricRow_Status);
				statusText.Text = biometricVm.Subtitle;
				statusText.SetTextColor (biometricVm.StatusColor.ToDroidColor ());

				ImageView statusIcon = biometricResultView.FindViewById<ImageView> (Resource.Id.biometricStatusIcon);
				statusIcon.SetImageDrawable (biometricVm.Status == BiometricResultStatus.InRange ? 
					biometricResultView.Resources.GetDrawable (Resource.Drawable.GreenCheckBox) : 
					biometricResultView.Resources.GetDrawable (Resource.Drawable.OrangeWarningTriangle));


				PSTextView measurementText = biometricResultView.FindViewById<PSTextView> (Resource.Id.biometricRow_Measurement);
				measurementText.Text = biometricVm.Measurement;

				PSTextView measurementDateText = biometricResultView.FindViewById<PSTextView> (Resource.Id.biometricRow_Date);
				measurementDateText.Text = biometricVm.MeasurementDate;

				if(isDetailsView){
					ImageView rightArrow = biometricResultView.FindViewById<ImageView> (Resource.Id.biometricResultRow_Arrow);
					rightArrow.Visibility = ViewStates.Gone;
				}

				biometricResultView.Click += biometricVm.RequestViewDetailsPage;

				return biometricResultView;
			} else if (!isDetailsView) {
				View biometricResultView = inflater.Inflate (Resource.Layout.StandardOrangeButton, null, false);

				PSButton button = biometricResultView.FindViewById<PSButton> (Resource.Id.standard_orange_button);
				button.Click += biometricVm.RequestViewDetailsPage;

				return biometricResultView;
			} else {
				View biometricResultView = inflater.Inflate (Resource.Layout.BiometricResultDetailListHeader, null, false);
				return biometricResultView;
			}
		}

    }
}