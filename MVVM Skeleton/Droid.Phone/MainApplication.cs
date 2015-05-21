using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Droid.OS_Services;
using Droid.Phone.Activities;
using Droid.Phone.Activities.Incentives.MarkAsCompleted;
using Droid.Phone.Activities.Incentives.MarkCannotComplete;
using Droid.Phone.OS_Services;
using Shared.BL;
using Shared.BL.BLs;
using Shared.BL.Interfaces;
using Shared.Bootstrapper;
using Shared.Common;
using Shared.Common.Utils;
using Shared.Common.Logging;
using Shared.Common.Mock;
using Shared.DAL;
using Shared.VM;
using Microsoft.Practices.Unity;
using Org.Apache.Http.Conn;
using Xamarin;

namespace Droid.Phone
{
    #if DEBUG
    [Application(Debuggable=true)]
    #else
    [Application(Debuggable = false)]
    #endif
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        private ILogger _logger;

        private int _sessionDepth = 0;

        private LifecycleService _lifecycleService;

        private static ViewModelLocator _locator;

        private static ViewModelStore _store;

        public static ViewModelLocator VMLocator
        {
            get { return _locator; }
        }

        public static ViewModelStore VMStore
        {
            get { return _store; }
        }

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
            Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Insights.PurgePendingCrashReports().Wait();
                }
            };
            Insights.Initialize(Settings.XamarinInsightsApiKey, Application.Context);

            _logger = new Logger();

            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                args.Handled = false;
                _logger.Log(args.Exception as Exception, LogType.ERROR);
            };
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                _logger.Log(e.ExceptionObject as Exception, LogType.ERROR);
            };
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Init();

            RegisterActivityLifecycleCallbacks(this);
        }

        private void Init()
        {
            IocBootstrapper.RegisterTypes(IocContainer.GetContainer());
            AutoMapperBootstrapper.MapTypes();

            _locator = new ViewModelLocator();
            _store = new ViewModelStore();

            IAppUpgradeService appUpgradeService = new AppUpgradeService();
            appUpgradeService.OnAppStart();

			IocContainer.GetContainer().RegisterInstance<ISecureDatabase>(new AndroidSecureDatabase());
#if TESTCLOUD
            if (Settings.MockServices)
            {
                IocContainer.GetContainer()
                    .RegisterInstance<IOAuth>(new MockOAuth("compassphs.auth0.com", "PgE9NpJY3t9X3lS9JcO1JXejDA5JtKYP",
                        "TestDatabase"));
            }
            else
            {
                IocContainer.GetContainer().RegisterInstance<IOAuth>(new AuthenticationService("compassphs.auth0.com", "PgE9NpJY3t9X3lS9JcO1JXejDA5JtKYP", "TestDatabase"));
            }
#else
            IocContainer.GetContainer().RegisterInstance<IOAuth>(new AuthenticationService ("compassphs.auth0.com", "PgE9NpJY3t9X3lS9JcO1JXejDA5JtKYP", "TestDatabase"));
#endif
            IocContainer.GetContainer().RegisterType<IHttpClientHelper, HttpClientHelper>();
            IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService>(ConfigureNav());
            IocContainer.GetContainer().RegisterInstance<IExtendedDialogService>(new ExtendedDialogService());
            IocContainer.GetContainer().RegisterInstance<IHudService>(new HudService());
            IocContainer.GetContainer().RegisterInstance<IConnectivityService>(new ConnectivityService());
            IocContainer.GetContainer().RegisterInstance<IBrowserService>(new BrowserService());
            IocContainer.GetContainer().RegisterInstance<IGeolocator>(new Geolocator());
            IocContainer.GetContainer().RegisterInstance<IDispatcherService>(new DispatcherService());
            IocContainer.GetContainer().RegisterInstance<INotificationsService>(ConfigureNotifications());
			IocContainer.GetContainer().RegisterInstance<IAppUpgradeService>(appUpgradeService);
			IocContainer.GetContainer().RegisterInstance<IPhoneService> (new PhoneService());
			IocContainer.GetContainer().RegisterInstance<IMapService> (new MapService());
			IocContainer.GetContainer().RegisterInstance<IEmailService> (new EmailService());

            IMemberBL memberBl = IocContainer.GetContainer().Resolve<IMemberBL>();
            IAppInfoDAL appInfoDAL = IocContainer.GetContainer().Resolve<IAppInfoDAL>();
            IGetConnectedManager getConnectedManager = IocContainer.GetContainer().Resolve<IGetConnectedManager>();

            _lifecycleService = new LifecycleService(memberBl, appInfoDAL, getConnectedManager, appUpgradeService);

            _lifecycleService.OnApplicationStart += appUpgradeService.BackgroundUpdateAppUpgradeState;
            _lifecycleService.OnApplicationResume += appUpgradeService.BackgroundUpdateAppUpgradeState;

            IocContainer.GetContainer().RegisterInstance<ILifecycleService>(_lifecycleService);

            _lifecycleService.OnStart();

        }

        private static NotificationsService ConfigureNotifications()
        {
            var notif = new NotificationsService();
            notif.RegisterForNotifications();
            return notif;
        }

        private static ExtendedNavigationService ConfigureNav()
        {
            var nav = new ExtendedNavigationService();

            nav.Configure(ViewModelLocator.REGISTER_VIEW_KEY, typeof (RegisterActivity));

            nav.Configure(ViewModelLocator.LOGIN_VIEW_KEY, typeof (LoginActivity));

            nav.Configure(ViewModelLocator.TOOL_TIP_VIEW_KEY, typeof (TooltipActivity));

            nav.Configure(ViewModelLocator.CREATE_PIN_VIEW_KEY, typeof (CreatePINActivity));
            nav.Configure(ViewModelLocator.CONFIRM_PIN_VIEW_KEY, typeof (ConfirmPINActivity));
            nav.Configure(ViewModelLocator.ENTER_PIN_VIEW_KEY, typeof (EnterPINActivity));

            nav.Configure(ViewModelLocator.DOCTOR_PROMPT_VIEW_KEY, typeof (DoctorPromptActivity));
            nav.Configure(ViewModelLocator.DOCTOR_LOOKUP_VIEW_KEY, typeof (DoctorLookupActivity));
            nav.Configure(ViewModelLocator.DOCTOR_PROMPT_LIST_VIEW_KEY, typeof (DoctorPromptListActivity));

            nav.Configure(ViewModelLocator.PRESCRIPTION_PROMPT_VIEW_KEY, typeof(PrescriptionPromptActivity));
            nav.Configure(ViewModelLocator.PRESCRIPTION_LOOKUP_VIEW_KEY, typeof(PrescriptionLookupActivity));
            nav.Configure(ViewModelLocator.PRESCRIPTION_INFO_VIEW_KEY, typeof(PrescriptionInformationActivity));
            nav.Configure(ViewModelLocator.PRESCRIPTION_PROMPT_LIST_VIEW_KEY, typeof(PrescriptionPromptListActivity));

            nav.Configure(ViewModelLocator.PROCEDURE_PROMPT_VIEW_KEY, typeof(ProcedurePromptActivity));
            nav.Configure(ViewModelLocator.PROCEDURE_LOOKUP_VIEW_KEY, typeof(ProcedureLookupActivity));
            nav.Configure(ViewModelLocator.PROCEDURE_PROMPT_INFO_VIEW_KEY, typeof(ProcedureInformationActivity));
            nav.Configure(ViewModelLocator.PROCEDURE_PROMPT_LIST_VIEW_KEY, typeof(ProcedurePromptListActivity));

            nav.Configure(ViewModelLocator.DEPENDENTS_PROMPT_VIEW_KEY, typeof(DependentPromptActivity));
            nav.Configure(ViewModelLocator.DEPENDENTS_PROMPT_INFO_VIEW_KEY, typeof(DependentInformationActivity));
            nav.Configure(ViewModelLocator.DEPENDENTS_PROMPT_LIST_VIEW_KEY, typeof(DependentPromptListActivity));

            nav.Configure(ViewModelLocator.INCENTIVE_COMPLETED_PROMPT_VIEW_KEY, typeof(IncentiveCompletedPromptActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_COMPLETED_INFORMATION_PAGE, typeof(IncentiveCompletedInformationActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_COMPLETED_ATTESTATION_PAGE, typeof(IncentiveCompletedAttestationActivity));

            nav.Configure(ViewModelLocator.INCENTIVE_CANT_COMPLETE_REASON_PROMPT_PAGE, typeof(IncentiveCantCompleteReasonPromptActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_CANT_COMPLETE_PROMPT_PAGE, typeof(IncentivesCantCompletePromptActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_CANT_COMPLETE_DOCTOR_VERIFY_PAGE, typeof(InventiveCantCompleteDoctorVerifyActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_CANT_COMPLETE_ATTESTATION_PAGE, typeof(IncentiveCantCompleteAttestationActivity));

            nav.Configure(ViewModelLocator.CALENDAR_VIEW_KEY, typeof(CalendarActivity));

            nav.Configure(ViewModelLocator.HOME_CONTAINER_KEY, typeof(HamburgerMenuActivity));

			nav.Configure(ViewModelLocator.INCENTIVE_SUMMARY_KEY, typeof(IncentiveSummaryActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_DETAIL_KEY, typeof(IncentiveDetailActivity));
            nav.Configure(ViewModelLocator.INCENTIVE_ACTION_DETAIL_KEY, typeof(IncentiveActionDetailActivity));

            nav.Configure(ViewModelLocator.CREATE_PIN_RESET_VIEW_KEY, typeof(CreatePINResetActivity));
            nav.Configure(ViewModelLocator.CONFIRM_PIN_RESET_VIEW_KEY, typeof(ConfirmPINResetActivity));
            nav.Configure(ViewModelLocator.ENTER_PIN_RESET_VIEW_KEY, typeof(EnterPINResetActivity));


            nav.Configure(ViewModelLocator.GENERIC_CONDITIONS_LOOKUP_PAGE, typeof(GenericConditionsLookupActivity));
            nav.Configure(ViewModelLocator.GENERIC_PRESCRIPTION_LOOKUP_PAGE, typeof(GenericPrescriptionLookupActivity));
            nav.Configure(ViewModelLocator.GENERIC_PROCEDURE_LOOKUP_PAGE, typeof(GenericProcedureLookupActivity));


            nav.Configure(ViewModelLocator.DEPENDENT_INFORMATION_PAGE, typeof(SettingsDependentInformationActivity));
            nav.Configure(ViewModelLocator.GENERIC_PROCEDURE_INFORMATION_PAGE, typeof(GenericProcedureInformationActivity));
            nav.Configure(ViewModelLocator.GENERIC_PRESCRIPTION_INFORMATION_PAGE, typeof(GenericPrescriptionInformationActivity));


			nav.Configure(ViewModelLocator.BIOMETRIC_RESULTS_VIEW_KEY, typeof(BiometricResultsActivity));
			nav.Configure(ViewModelLocator.BIOMETRIC_RESULT_DETAIL_VIEW_KEY, typeof(BiometricResultDetailActivity)); 

			return nav;
		}

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            activity.RequestedOrientation = ScreenOrientation.Portrait;

			#if !TESTCLOUD && !DEBUG
                //TODO renable for final release!! Disabled for testing and allowing screenshots.
				//activity.Window.AddFlags(WindowManagerFlags.Secure);
            #endif
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            if (_sessionDepth == 0)
            {
                _lifecycleService.OnResume();

                if (activity.GetType() != typeof(SplashActivity) & activity.GetType() != typeof(EnterPINActivity) & _lifecycleService.CurrentState == AppState.LOCKED_OUT)
                {
                    var intent = new Intent(this, typeof(EnterPINActivity));
                    activity.StartActivity(intent);
                }
            }
            _sessionDepth++;
        }

        public void OnActivityStopped(Activity activity)
        {
            if (_sessionDepth > 0)
            {
                _sessionDepth--;
            }
            if (_sessionDepth == 0)
            {
                _lifecycleService.OnSleep();
            }
        }
    }
}
