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
using Shared.Common;

namespace Droid.Phone.Activities.Incentives
{
    public abstract class IncentiveAttestationBaseActivity : ActionBarBaseActivity
    {
        protected abstract string HeaderText { get; }
        
        protected abstract string BodyText { get; }
        
        protected abstract string FooterText { get; }
        
        protected abstract string FullNameHintText { get; }

		protected abstract string PhoneHintText { get; }
        
        protected abstract string MainButtonText { get; }
        
        protected abstract string CancelButtonText { get; }

		protected virtual bool ShowPhoneControl {
			get {
				return false;
			}
		}


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.IncentiveAttestationConfirmation);

            InitBindings();

            SubscribeToEvents();

            SetupScreen();
        }

        protected abstract void SubscribeToEvents();

        private void SetupScreen()
        {

            FindViewById<TextView>(Resource.Id.IncentiveAttestationConfirmationHeader).Text = HeaderText;

            FindViewById<TextView>(Resource.Id.IncentiveAttestationConfirmationBody).Text = BodyText;

            FindViewById<TextView>(Resource.Id.IncentiveAttestationConfirmationFooter).Text = FooterText;

            FindViewById<FloatLabeledEditText>(Resource.Id.IncentiveAttestationConfirmationFullName).SetHint(FullNameHintText);

			if (ShowPhoneControl) {
				FindViewById<FloatLabeledEditText> (Resource.Id.IncentiveAttestationConfirmationPhone).SetHint (PhoneHintText);
			} else {
				FindViewById<RelativeLayout> (Resource.Id.IncentiveAttestationConfirmationPhoneLayout).Visibility = ViewStates.Gone;
			}

            FindViewById<Button>(Resource.Id.IncentiveAttestationConfirmationSubmitButton).Text = MainButtonText;

            FindViewById<Button>(Resource.Id.IncentiveAttestationConfirmationCancelButton).Text = CancelButtonText;

        }

        protected abstract void InitBindings();
    }
}