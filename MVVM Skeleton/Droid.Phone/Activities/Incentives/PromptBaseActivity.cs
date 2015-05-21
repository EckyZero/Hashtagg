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
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight.Command;
using Java.Lang;

namespace Droid.Phone.Activities.Incentives
{
    public abstract class PromptBaseActivity : ActionBarBaseActivity
    {
        protected abstract int MainImageId { get; }

        protected abstract string TitleText { get; }

        protected abstract string DescriptionText { get; }

        protected virtual string SearchHint { get { return null; } }

        protected virtual string BottomButtonOneText { get { return null; } }

        protected abstract string BottomButtonTwoText { get; }

        protected virtual string PickerHint { get { return null; } }

        protected virtual string OtherFieldHint { get { return null; } }

        protected abstract IList<string> PickerData { get; }

        protected virtual bool ShouldShowPicker { get { return false; } }

        protected virtual bool ShouldShowLookupButton { get { return false; } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.BasePrompt);

            InitBindings();

            SubscribeToEvents();

            SetupScreen();
        }

        protected abstract void SubscribeToEvents();

        private void SetupScreen()
        {
            FindViewById<TextView>(Resource.Id.BasePromptTitle).Text = TitleText;

            FindViewById<TextView>(Resource.Id.BasePromptDescription).Text = DescriptionText;

            FindViewById<ImageView>(Resource.Id.BasePromptImage).SetImageResource(MainImageId);

            //The layout that contains otherField and Picker, Has to be shown if either are shown, hidden otherwise, based on picker status
            var fletLayout = FindViewById<LinearLayout>(Resource.Id.BasePromptFLETLayout);

            //Float Labeled Edit Text used if Text Input is taken
            var otherField = FindViewById<FloatLabeledEditText>(Resource.Id.BasePromptOtherField);

            //Generic Lookup Button with search hint and on click action
            var lookupButton = FindViewById<TextView>(Resource.Id.BasePromptSearch);

            //Top Button, Standard Orange
            var buttonOne = FindViewById<Button>(Resource.Id.BasePromptButtonOne);

            //Bottom Button, Clear, Cancel
            var buttonTwo = FindViewById<Button>(Resource.Id.BasePromptButtonTwo);

            //Set all Button Text, even if null
            lookupButton.Text = SearchHint;

            buttonOne.Text = BottomButtonOneText;

            buttonTwo.Text = BottomButtonTwoText;

            //This is a FLET, for user input if needed
            otherField.SetHint(OtherFieldHint);

            //Hide any buttons with no text (Screens show a variant of all 3)
            lookupButton.Visibility = ShouldShowLookupButton ? ViewStates.Visible : ViewStates.Gone;

            buttonOne.Visibility = string.IsNullOrWhiteSpace(BottomButtonOneText) ? ViewStates.Gone : ViewStates.Visible;

            buttonTwo.Visibility = string.IsNullOrWhiteSpace(BottomButtonTwoText) ? ViewStates.Gone : ViewStates.Visible;

            //We default this to gone, enable as needed in Child (Remember to enable fletLayout if not using picker with this)
            otherField.Visibility = ViewStates.Gone;

            //This is the generic picker, can start enabled or disabled and with values/hint passed in
            var picker = FindViewById<FloatLabeledEditText>(Resource.Id.BasePromptDropDown);

            if (ShouldShowPicker)
            {
                fletLayout.Visibility = ViewStates.Visible;
                picker.Visibility = ViewStates.Visible;
                picker.SetHint(PickerHint);
                picker.SpinnerValues.AddRange(PickerData);

            }
            else
            {
                picker.Visibility = ViewStates.Gone;
                fletLayout.Visibility = ViewStates.Gone;
            }

        }

        protected abstract void InitBindings();

    }
}