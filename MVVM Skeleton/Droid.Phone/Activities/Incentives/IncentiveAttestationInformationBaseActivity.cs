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
using Droid.Controls;

namespace Droid.Phone.Activities.Incentives
{
    public abstract class IncentiveAttestationInformationBaseActivity : ActionBarBaseActivity
    {
        protected abstract int MainImageId { get; }

        protected abstract string TitleText { get; }

        protected abstract bool ShouldShowDescription { get; }

        protected abstract string DescriptionText { get; }

        protected abstract string MainButtonText { get; }

        protected virtual string DateHint {
            get { return null; }
        }

        protected virtual bool ShouldShowDate
        {
            get { return false; }
        }

        protected abstract string CellHeader { get; }

        protected abstract string CellBody { get; }

        protected abstract string CellFooterOne { get; }

        protected abstract string CellFooterTwo { get; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.IncentiveAttestationInformation);

            InitBindings();

            SubscribeToEvents();

            SetupScreen();
        }

        protected abstract void SubscribeToEvents();

        private void SetupScreen()
        {
            FindViewById<TextView>(Resource.Id.IncentiveAttestationInformationTitle).Text = TitleText;

            var descriptionText = FindViewById<TextView>(Resource.Id.IncentiveAttestationInformationDescription);

            if (ShouldShowDescription)
            {
                descriptionText.Visibility = ViewStates.Visible;
                descriptionText.Text = DescriptionText;
            }
            else
            {
                descriptionText.Visibility = ViewStates.Gone;
            }

            FindViewById<ImageView>(Resource.Id.IncentiveAttestationInformationImage).SetImageResource(MainImageId);
            FindViewById<Button>(Resource.Id.IncentiveAttestationInformationSubmitButton).Text = MainButtonText;
            var picker = FindViewById<FloatLabeledEditText>(Resource.Id.IncentiveAttestationDate);

            if (ShouldShowDate)
            {
                picker.Visibility = ViewStates.Visible;
                picker.SetHint(DateHint);
            }
            else
            {
                picker.Visibility = ViewStates.Gone;
            }

            FindViewById<TextView>(Resource.Id.IncentiveAttestationInformationCellHeader).Text = CellHeader;
            FindViewById<TextView>(Resource.Id.IncentiveAttestationInformationCellBody).Text = CellBody;
            FindViewById<TextView>(Resource.Id.IncentiveAttestationInformationCellFooter).Text = CellFooterOne;
            FindViewById<TextView>(Resource.Id.IncentiveAttestationInformationCellFooterTwo).Text = CellFooterTwo;

        }


        protected abstract void InitBindings();
    }

}