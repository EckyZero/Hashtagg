using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Threading.Tasks;
using iOS.Phone.Controllers.Generic;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.BL;

namespace iOS.Phone
{
	partial class SplashController : UIViewController
	{
		ILifecycleService _lifecycleService;

		private bool _skipLoad = false;

		public bool IsFirstLoad {get;set;}

		public int NotificationStartupPage { get{ return _startupPage;} set{ _startupPage = value; } }

		public bool SkipAnimation { get; set; }

		private int _startupPage = -1;

		public async Task SetBackgroundState(bool enteringBackground)
		{
			IsFirstLoad = false;
			if (enteringBackground)
			{
				_skipLoad = true;
			}
			else
			{
				_skipLoad = false;
				await Load();
			}
		}

		public SplashController (IntPtr handle) : base (handle) 
		{
			_lifecycleService = IocContainer.GetContainer ().Resolve<ILifecycleService> ();
			IsFirstLoad = false;
		}

		private async Task Load()
		{
			switch (_lifecycleService.CurrentState) 
			{

			case AppState.LOCKED_OUT:
				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("EnterPINController") as EnterPINController;
				NavigationController.PushViewController (controller, false);
				break;
			
			case AppState.NEW_USER:
				await DismissViewControllerAsync (false);
				break;

			case AppState.RETURNING:
				await DismissViewControllerAsync (false);
				break;
			}
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            // temp
//			var tempStoryboard = UIStoryboard.FromName ("MainStoryboard", null);
//			var tempController = tempStoryboard.InstantiateViewController ("ProcedurePromptInformationController") as ProcedurePromptInformationController;

//			tempController.SelectedPatientProcedure = new PatientProcedure ();

//			NavigationController.PushViewController(tempController, false);
//			var delteMeCon = new GenericTelerikCalendar ();
//            NavigationController.PushViewController(delteMeCon, false);
//		    return;
            // temp

			NavigationController.SetNavigationBarHidden (true, false);

			if (IsFirstLoad) {
				IsFirstLoad = false;
				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("MainNavigationController") as UINavigationController;
				var splash = controller.ChildViewControllers [0] as SplashController;
				splash.SkipAnimation = SkipAnimation;
				this.PresentViewController (controller, false, FirstLoadNavigate);
			}else if (SkipAnimation && !_skipLoad){
				await Load ();
			} else if (!_skipLoad ) {
				await Task.Delay (2000);
				await Load ();
			}
		}

		private void FirstLoadNavigate()
		{
			if (_lifecycleService.CurrentState == AppState.NEW_USER) {

				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("GetStartedPageController") as GetStartedPageController;
				NavigationController.PushViewController (controller, false);
			}
			else {
				UnlockedNavigate ();
			}
		}

		private void UnlockedNavigate()
		{
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			UIViewController controller = null;

            //Notifications Can Navigate to a Specific Page, We determine if it sent us a page by checking that the int is != -1 then cast to enum
            //This is the enum that would be assigned to the int if not -1
			var startupPage = NotificationStartupPage != -1 ? (StartupPage) NotificationStartupPage : _lifecycleService.GetStartupPage ();

			switch (startupPage)
			{
			case StartupPage.GCProvider:
				controller = storyboard.InstantiateViewController ("DoctorPromptController") as DoctorPromptController;
				break;

			case StartupPage.GCPrescription:
				controller = storyboard.InstantiateViewController ("PrescriptionPromptController") as PrescriptionPromptController;
				break;

			case StartupPage.GCProcedure:
				controller = storyboard.InstantiateViewController ("ProcedurePromptController") as ProcedurePromptController;
				break;

			case StartupPage.GCDependent:
				controller = storyboard.InstantiateViewController ("DependentPromptController") as DependentPromptController;
				break;

			case StartupPage.Home:
				controller = storyboard.InstantiateViewController ("HomeContainerController") as HomeContainerController;
				break;
            case StartupPage.IncentiveSummary:
                var incentiveController = storyboard.InstantiateViewController ("HomeContainerController") as HomeContainerController;
                incentiveController.InitialContainerPage = (int)MenuActionType.Incentives;
			    controller = incentiveController;
				break;
            default:
                controller = storyboard.InstantiateViewController("HomeContainerController") as HomeContainerController;
			    break;
			}
				
			if (controller != null) {
				NavigationController.PushViewController (controller, false);
			}
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}

    }
}
