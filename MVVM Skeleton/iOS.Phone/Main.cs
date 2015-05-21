
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Microsoft.Practices.Unity;
using Shared.Common;
using Xamarin;
using System.Threading.Tasks;
using System.Threading;
using Shared.Bootstrapper;
using Shared.Common.Logging;
using Shared.Common.Mock;
using Shared.VM;
using Shared.DAL;

namespace iOS.Phone
{
	public class Application
    {
        private static ILogger _logger;

		private static ViewModelLocator _locator;

		private static ViewModelStore _store;

		public static ViewModelLocator VMLocator { get { return _locator; } }

		public static ViewModelStore VMStore { get { return _store; } }

		static Application()
		{
			Init ();
		}

		// This is the main entry point of the application.
		static void Main (string[] args)
        {
            Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Insights.PurgePendingCrashReports().Wait();
                }
            };
			Insights.Initialize(Settings.XamarinInsightsApiKey);

            _logger = new Logger();

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                _logger.Log(e.ExceptionObject as Exception, LogType.ERROR);
            };

		    try
		    {
                UIApplication.Main(args, null, "AppDelegate");
		    }
		    catch (Exception e)
		    {
                _logger.Log(e, LogType.ERROR);
		        throw;
		    }
		}

		private static void Init()
		{
			IocBootstrapper.RegisterTypes(IocContainer.GetContainer());
			AutoMapperBootstrapper.MapTypes ();

			_locator = new ViewModelLocator();
			_store = new ViewModelStore ();

            var appUpgradeService = new AppUpgradeService();
            appUpgradeService.OnAppStart();
            IocContainer.GetContainer().RegisterInstance<IAppUpgradeService>(appUpgradeService);

			IocContainer.GetContainer().RegisterInstance<ISecureDatabase>(new iOSSecureDatabase());
			IocContainer.GetContainer ().RegisterType<ISecureKeyValueStore, iOSSecureKeyValueStore> ();
#if TESTCLOUD
		    if (Settings.MockServices)
            {
                IocContainer.GetContainer().RegisterInstance<IOAuth>(new MockOAuth("compassphs.auth0.com", "PgE9NpJY3t9X3lS9JcO1JXejDA5JtKYP", "TestDatabase"));
		    }
		    else
            {
                IocContainer.GetContainer().RegisterInstance<IOAuth>(new AuthenticationService("compassphs.auth0.com", "PgE9NpJY3t9X3lS9JcO1JXejDA5JtKYP", "TestDatabase"));
		    }
#else
			IocContainer.GetContainer().RegisterInstance<IOAuth>(new AuthenticationService ("compassphs.auth0.com", "PgE9NpJY3t9X3lS9JcO1JXejDA5JtKYP", "TestDatabase"));
#endif
            IocContainer.GetContainer().RegisterType<IHttpClientHelper, HttpClientHelper>();
			IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService> (ConfigureNav());
			IocContainer.GetContainer().RegisterType<IExtendedDialogService, ExtendedDialogService>();
			IocContainer.GetContainer().RegisterInstance<IHudService> (new HudService());
			IocContainer.GetContainer().RegisterType<IBrowserService, iOSBrowserService> ();
			IocContainer.GetContainer().RegisterInstance<IPhoneService> (new PhoneService());
			IocContainer.GetContainer().RegisterInstance<IMapService> (new MapService());
            IocContainer.GetContainer().RegisterInstance<IGeolocator>(new Geolocator());
            IocContainer.GetContainer().RegisterInstance<IConnectivityService>(new ConnectivityService());
			IocContainer.GetContainer().RegisterInstance<INotificationsService> (new NotificationsService ());
			IocContainer.GetContainer().RegisterInstance<IDispatcherService>( new DispatcherService(new NSObject()));
			IocContainer.GetContainer().RegisterInstance<IEmailService> (new EmailService());
		}

		private static ExtendedNavigationService ConfigureNav()
		{
			var nav = new ExtendedNavigationService ();

			nav.Configure (
				ViewModelLocator.HOME_CONTAINER_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("HomeContainerController") as HomeContainerController;

					return controller;
				}
			);

			// configure
			nav.Configure (
				ViewModelLocator.CREATE_PIN_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("CreatePINController") as CreatePINController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.TOOL_TIP_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("ToolTipController") as ToolTipController;

					var key = (x != null ? x.ToString() : string.Empty);

					controller.TipKey = key;
					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.REGISTER_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("RegisterController") as RegisterController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.LOGIN_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("LoginController") as LoginController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.CONFIRM_PIN_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("ConfirmPINController") as ConfirmPINController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.ENTER_PIN_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("EnterPINController") as EnterPINController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.DOCTOR_PROMPT_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("DoctorPromptController") as DoctorPromptController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.DOCTOR_PROMPT_LIST_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("DoctorPromptListController") as DoctorPromptListController;
					return controller;
				}
			);
			nav.Configure (
				ViewModelLocator.DOCTOR_LOOKUP_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("DoctorLookupListController") as DoctorLookupListController;

					var lookupParams = x as DoctorLookupControllerParameters;

					if(x != null){
						var onSelectPatientProvider = lookupParams.OnPatientSelected;
						controller.OnSelectPatientProvider = onSelectPatientProvider;
						controller.Search = lookupParams.DoctorName;
					}
					var navController = new UINavigationController(controller);
					return navController;
				}
			);

			nav.Configure (
				ViewModelLocator.PATIENT_PROVIDER_MODIFY_ALERT_VIEW_KEY,
				x => {
					var controller = new ModifyAlertController<PatientProvider>((IPatientProviderViewModel)x);

					if(Device.OS >= 8)
					{
						return controller.Alert;
					}
					else
					{
						controller.Show();
						return null;
					}
				}
			);

			nav.Configure (
				ViewModelLocator.FACILITY_MODIFY_ALERT_VIEW_KEY,
				x => {
					var controller = new ModifyAlertController<Facility>((IFacilityViewModel)x);

					if(Device.OS >= 8)
					{
						return controller.Alert;
					}
					else
					{
						controller.Show();
						return null;
					}
				}
			);

			nav.Configure (
				ViewModelLocator.PROVIDER_MODIFY_ALERT_VIEW_KEY,
				x => {
					var controller = new ModifyAlertController<Provider>((IProviderViewModel)x);
	
					if(Device.OS >= 8)
					{
						return controller.Alert;
					}
					else
					{
						controller.Show();
						return null;
					}
				}
			);

			nav.Configure (
				ViewModelLocator.PRESCRIPTION_PROMPT_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("PrescriptionPromptController") as PrescriptionPromptController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.PRESCRIPTION_PROMPT_LIST_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("PrescriptionPromptController") as PrescriptionPromptListController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.PRESCRIPTION_LOOKUP_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("PrescriptionLookupListController") as PrescriptionLookupListController;
					var navController = new UINavigationController(controller);

					return navController;
				}
			);

			nav.Configure (
				ViewModelLocator.PRESCRIPTION_MODIFY_ALERT_VIEW_KEY,
				x => {
					var controller = new ModifyAlertController<PatientPrescription>((IPatientPrescriptionViewModel)x);

					if(Device.OS >= 8)
					{
						return controller.Alert;
					}
					else
					{
						controller.Show();
						return null;
					}
				}
			);

			nav.Configure (
				ViewModelLocator.PROCEDURE_PROMPT_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("ProcedurePromptController") as ProcedurePromptController;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.PROCEDURE_LOOKUP_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("ProcedureLookupListController") as ProcedureLookupListController;
					var navController = new UINavigationController(controller);

					return navController;
				}
			);

			nav.Configure (
				ViewModelLocator.PROCEDURE_PROMPT_INFO_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("ProcedurePromptInformationController") as ProcedurePromptInformationController;

					ProcedureInformationControllerParameters procedureResult = x as ProcedureInformationControllerParameters;


					if(procedureResult != null)
					{
						if(procedureResult.PatientProcedure != null)
						{
							controller.SelectedPatientProcedure = procedureResult.PatientProcedure;
						}

						controller.EditMode = procedureResult.EditMode;

						if(procedureResult.EditMode){
							var navController = new UINavigationController(controller);
							return navController;
						}

					}

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.PROCEDURE_PROMPT_LIST_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("ProcedurePromptListController") as ProcedurePromptListController;

					return controller;
				}
			);


			nav.Configure (
				ViewModelLocator.PROCEDURE_MODIFY_ALERT_VIEW_KEY,
				x => {
					var controller = new ModifyAlertController<PatientProcedure>((IPatientProcedureViewModel)x);

					if(Device.OS >= 8)
					{
						return controller.Alert;
					}
					else
					{
						controller.Show();
						return null;
					}
				}
			);

			nav.Configure (
				ViewModelLocator.PRESCRIPTION_INFO_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("PrescriptionInformationController") as PrescriptionInformationController;

					PrescriptionInformationControllerParameters prescriptionResult = x as PrescriptionInformationControllerParameters;


					if(prescriptionResult != null)
					{
						if(prescriptionResult.PatientPrescription != null)
						{
							controller.SelectedPrescription = prescriptionResult.PatientPrescription;
						}

						controller.EditMode = prescriptionResult.EditMode;

						if(prescriptionResult.EditMode){
							var navController = new UINavigationController(controller);
							return navController;
						}

					}


					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.DEPENDENTS_MODIFY_ALERT_VIEW_KEY,
				x => {
					var controller = new ModifyAlertController<Dependent>((IDependentViewModel)x);

					if(Device.OS >= 8)
					{
						return controller.Alert;
					}
					else
					{
						controller.Show();
						return null;
					}
				}
			);

			nav.Configure (
				ViewModelLocator.CALENDAR_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("CalendarController") as CalendarController;
					var parameters = (CalendarParameters)x;

					controller.Parameters = parameters;

					return controller;
				}
			);

			nav.Configure (
				ViewModelLocator.DEPENDENTS_PROMPT_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController("DependentPromptController") as DependentPromptController;

					return controller;
				}
			);
			nav.Configure (
				ViewModelLocator.DEPENDENTS_PROMPT_INFO_VIEW_KEY,
				x => {
					UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
					var controller = storyboard.InstantiateViewController ("DependentInformationController") as DependentInformationController;
					DependentInformationControllerParameters dependentResult = x as DependentInformationControllerParameters;


					if(dependentResult != null)
					{
						if(dependentResult.Dependent != null)
						{
							controller.SelectedDependent = dependentResult.Dependent;
						}

						controller.EditMode = dependentResult.EditMode;
					}
					else{
						controller.SelectedDependent = new Dependent();
					}

					var navController = new UINavigationController(controller);
					navController.SetNavigationBarHidden (false, true);
					navController.NavigationBar.Translucent = false;
					navController.NavigationBar.BarTintColor = SharedColors.CompassBlue.ToUIColor ();
					navController.NavigationBar.TintColor = SharedColors.White.ToUIColor ();
					navController.NavigationBar.TitleTextAttributes = new UIStringAttributes {
						ForegroundColor = UIColor.White,
						Font = UIFont.FromName("FuturaStd-Bold", 16f)
					};

					return navController;


//					return controller;
				}
			);
			return nav;
		}
	}
}
