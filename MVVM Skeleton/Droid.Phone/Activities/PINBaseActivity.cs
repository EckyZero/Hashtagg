using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Droid.Controls;
using Shared.BL;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Activities
{
    public enum PINStep
    {
        CREATE,
        CONFIRM,
        ENTER,
        CREATE_RESET,
        CONFIRM_RESET,
        ENTER_RESET
    }

    public abstract class PINBaseActivity : BaseActivity
    {
        protected Pincode _pin;

        protected abstract PINStep PINType { get; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.GenericPIN);

            SetupScreen();
        }

        protected override void OnResume()
        {
            base.OnResume();

            _pin.PIN = string.Empty;
        }

        private void SetupScreen()
        {
            var email = FindViewById<PSTextView>(Resource.Id.genericPINPage_email);
            var title = FindViewById<PSTextView>(Resource.Id.genericPINPage_TitleText);
            var exp = FindViewById<PSTextView>(Resource.Id.genericPINPage_ExplanationText);
            var inst = FindViewById<PSTextView>(Resource.Id.genericPINPage_InstructionsText);
            _pin = FindViewById<Pincode>(Resource.Id.genericPINPage_PIN);
            var opt1 = FindViewById<PSButton>(Resource.Id.genericPINPage_OptionOne);
            var pipeL = FindViewById<TextView>(Resource.Id.genericPINPage_NavigationLeftPipe);
            var opt2 = FindViewById<PSButton>(Resource.Id.genericPINPage_OptionTwo);
            var pipeR = FindViewById<TextView>(Resource.Id.genericPINPage_NavigationRightPipe);
            var opt3 = FindViewById<PSButton>(Resource.Id.genericPINPage_OptionThree);

            string emailTxt = string.Empty;
            Member member = IocContainer.GetContainer().Resolve<IMemberBL>().GetCurrentMember();
            if (member != null)
            {
                emailTxt = member.Username;
            }

            if (PINType == PINStep.CREATE)
            {
                email.Visibility = ViewStates.Invisible;
                title.Text = "Great!";
                exp.Text = "Create a 4 - Digit PIN";
                inst.Text = "Create a PIN for fast, secure access";
                opt1.Visibility = ViewStates.Invisible;
                pipeL.Visibility = ViewStates.Invisible;
                opt2.Text = "Cancel";
                pipeR.Visibility = ViewStates.Invisible;
                opt3.Visibility = ViewStates.Invisible;
            }

            else if (PINType == PINStep.CONFIRM)
            {
                email.Visibility = ViewStates.Invisible;
                title.Text = "One more time please.";
                exp.Text = "Confirm 4 - Digit PIN";
                inst.Text = "So we can make sure they match.";
                opt1.Visibility = ViewStates.Invisible;
                pipeL.Visibility = ViewStates.Invisible;
                opt2.Text = "Start Over";
                pipeR.Visibility = ViewStates.Invisible;
                opt3.Visibility = ViewStates.Invisible;
            }

            else if (PINType == PINStep.ENTER)
            {
                email.Text = emailTxt;
                title.Text = "Welcome back!";
                exp.Text = "Enter Your 4 - Digit PIN";
                inst.Text = "Enter your PIN to get started";
                opt1.Text = "Forgot PIN";
                opt2.Text = "Use Password";
                opt3.Text = "Take Tour";
            }
            else if (PINType == PINStep.CREATE_RESET)
            {
                email.Visibility = ViewStates.Invisible;
                title.Text = "New PIN";
                exp.Text = "Enter Your New 4 - Digit PIN";
                inst.Visibility = ViewStates.Invisible;
                opt1.Visibility = ViewStates.Invisible;
                pipeL.Visibility = ViewStates.Invisible;
                opt2.Text = "Cancel";
                pipeR.Visibility = ViewStates.Invisible;
                opt3.Visibility = ViewStates.Invisible;
            }

            else if (PINType == PINStep.CONFIRM_RESET)
            {
                email.Visibility = ViewStates.Invisible;
                title.Text = "Confirm New PIN";
                exp.Text = "Confirm Your New 4 - Digit PIN";
                inst.Visibility = ViewStates.Invisible;
                opt1.Visibility = ViewStates.Invisible;
                pipeL.Visibility = ViewStates.Invisible;
                opt2.Text = "Start Over";
                pipeR.Visibility = ViewStates.Invisible;
                opt3.Visibility = ViewStates.Invisible;
            }

            else if (PINType == PINStep.ENTER_RESET)
            {
                email.Text = emailTxt;
                title.Text = "Old PIN";
                exp.Text = "Enter Your Old 4 - Digit PIN";
                inst.Visibility = ViewStates.Invisible;
                opt1.Visibility = ViewStates.Invisible;
                pipeL.Visibility = ViewStates.Invisible;
                opt2.Text = "Cancel";
                pipeR.Visibility = ViewStates.Invisible;
                opt3.Visibility = ViewStates.Invisible;
            }

            _pin.PinCodeComplete += PinCodeComplete;
            opt1.Click += OptionOneClick;
            opt2.Click += OptionTwoClick;
            opt3.Click += OptionThreeClick;
        }
 
        private void OptionThreeClick(object sender, EventArgs args)
        {
            OnOptionThreeInvoked();
        }
 
        private void OptionTwoClick(object sender, EventArgs args)
        {
            OnOptionTwoInvoked();
        }
 
        private void OptionOneClick(object sender, EventArgs args)
        {
            OnOptionOneInvoked();
        }
 
        private void PinCodeComplete(object sender, PinCodeEventArgs args)
        {
            OnPINCompleted(_pin.PIN);
        }

        protected abstract void OnPINCompleted(string pin);

        protected virtual void OnOptionOneInvoked()
        {
        }

        protected abstract void OnOptionTwoInvoked();

        protected virtual void OnOptionThreeInvoked()
        {
        }

        protected void IncorrectPIN()
        {
            _pin.OnIncorrectPIN();
        }
    }
}