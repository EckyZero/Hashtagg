using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Droid.Phone.Activities;
using System.Threading.Tasks;
using Android.Util;
using Shared.BL;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;


namespace Droid.Phone.Activities
{
    [Activity(Label = "Compass Mobile", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity: Activity

    {
        private ILifecycleService _lifecycleService;
        private IMemberBL _memberBL;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _lifecycleService = IocContainer.GetContainer().Resolve<ILifecycleService>();
            _memberBL = IocContainer.GetContainer().Resolve<IMemberBL>();

            int pageKey = -1;

            var intentBundle = Intent.GetBundleExtra(CloudNotificationConstants.KEY);

            if (intentBundle != null)
            {
                var action = intentBundle.GetString(CloudNotificationConstants.ACTION_KEY);
                var personKey = intentBundle.GetInt(CloudNotificationConstants.PERSON_KEY);

                //Not currently used, but avaialble in the bundle for use if needed
                //var message = intentBundle.GetString(CloudNotificationConstants.MESSAGE_KEY);
                //var recordId = intentBundle.GetString(CloudNotificationConstants.RECORD_ID_KEY);

                Member currentMember = _memberBL.GetCurrentMember();

                if (personKey != -1 && currentMember != null && currentMember.PersonKey == personKey)
                {
                    if (action == CloudNotificationConstants.INCENTIVES)
                    {
                        pageKey = (int)StartupPage.IncentiveSummary;
                    }
                    else if (action == CloudNotificationConstants.HOME)
                    {
                        pageKey = (int) StartupPage.Home;
                    }
                }
            }

            //If Splash is relaunched for any reason where the bundle is null then show animation
            if (_lifecycleService.IsFirstRun || intentBundle == null)
            {
                SetContentView(Resource.Layout.SplashPage);
                _lifecycleService.IsFirstRun = false;
                await Task.Delay(2000);    
            }

            Navigate(pageKey);

        }

        private void Navigate(int pageKey = -1)
        {
            if (_lifecycleService.CurrentState == AppState.NEW_USER && !_memberBL.HasMemberCompletedLogin())
            {
                var newUserIntent = new Intent(this, typeof(GetStartedActivity));
                StartActivity(newUserIntent);
                return;
            }

            string page = ViewModelLocator.HOME_CONTAINER_KEY;

            //Default startupPage when logged in
            var startupPage = StartupPage.Home;

            if (pageKey != -1)
            {
                startupPage = (StartupPage) pageKey;
            }
            else
            {
                try
                {
                    startupPage = _lifecycleService.GetStartupPage();
                }
                catch 
                {
                   startupPage = StartupPage.Home;
                }
            }



            switch (startupPage)
            {
                case StartupPage.GCProvider:
                    page = ViewModelLocator.DOCTOR_PROMPT_VIEW_KEY;
                    break;

                case StartupPage.GCPrescription:
                    page = ViewModelLocator.PRESCRIPTION_PROMPT_VIEW_KEY;
                    break;

                case StartupPage.GCProcedure:
                    page = ViewModelLocator.PROCEDURE_PROMPT_VIEW_KEY;
                    break;

                case StartupPage.GCDependent:
                    page = ViewModelLocator.DEPENDENTS_PROMPT_VIEW_KEY;
                    break;
                case StartupPage.IncentiveSummary:
                case StartupPage.Home:
                    page = ViewModelLocator.HOME_CONTAINER_KEY;
                    break;
                default:
                    page = ViewModelLocator.HOME_CONTAINER_KEY;
                    break;
            }

            if (_lifecycleService.CurrentState == AppState.LOCKED_OUT)
            {
                var lockedIntent = new Intent(this, typeof(EnterPINActivity));
                lockedIntent.PutExtra("Activity", page);
                lockedIntent.PutExtra("PageKey", pageKey);
                lockedIntent.SetFlags(ActivityFlags.SingleTop);
                StartActivity(lockedIntent);
                return;
            }

            if (_lifecycleService.CurrentState == AppState.RETURNING)
            {
                var navigationService = (ExtendedNavigationService)IocContainer.GetContainer().Resolve<IExtendedNavigationService>();
                navigationService.NavigateTo(page, navigationService.GenerateParameters((StartupPage)pageKey));
                return;
            }

            var intent = new Intent(this, typeof(EnterPINActivity));
            intent.PutExtra("Activity", page);
            intent.PutExtra("PageKey", pageKey);
            intent.SetFlags(ActivityFlags.SingleTop);
            StartActivity(intent);    
        }

    }
}