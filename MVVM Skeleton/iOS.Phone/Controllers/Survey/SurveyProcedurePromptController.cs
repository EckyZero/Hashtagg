using System;
using UIKit;
using Shared.VM;
using Microsoft.Practices.Unity;
using Shared.Common;
using Shared.BL;
using GalaSoft.MvvmLight.Helpers;
using Foundation;

namespace iOS.Phone
{
	[Register ("SurveyProcedurePromptController")]
	public partial class SurveyProcedurePromptController : UIViewController
	{
		public SurveyProcedurePromptViewModel ViewModel { get; private set;}

		public SurveyProcedurePromptController (IntPtr handle) : base (handle)
		{
			ViewModel = new SurveyProcedurePromptViewModel (
				IocContainer.GetContainer().Resolve<IMemberBL>(),
				IocContainer.GetContainer().Resolve<IProcedureBL>()
			);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			Title = ViewModel.Title;

			PromptButton.Text = ViewModel.Placeholder;
		}

		public void ViewModel_RequestProcedureLookup()
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);

			var controller = storyboard.InstantiateViewController("ProcedureLookupListController") as ProcedureLookupListController;

			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults ();

			controller.OnSelectAndReturn = (procedure) => {
				NavigationController.DismissViewController(true, null);
				ViewModel.ProcedureSelected(procedure);
			};

			NavigationController.PresentViewController (navController, true, null);
		}

		public async void ViewModel_RequestPostSelectionPage()
		{
			if (ViewModel.DestinationViewModel != null && ViewModel.DestinationViewModel.Node != null) {
				var controller = new BaseSurveyTableController (ViewModel.DestinationViewModel);

				NavigationController.PushViewController (controller, true);
			} else {
				ExtendedDialogService dialogService = IocContainer.GetContainer ().Resolve<IExtendedDialogService> () as ExtendedDialogService;
				await dialogService.ShowMessage(ApplicationResources.GenericError, "Error Occured");
			}
		}

		private void ViewModel_RequestSkipPage()
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);

			var controller = storyboard.InstantiateViewController ("DoctorPreferencesController") as DoctorPreferencesController;

			controller.FlowData = ViewModel.DestinationViewModel.FlowData;
			controller.TreeMap = ViewModel.DestinationViewModel.Map;

			NavigationController.PushViewController (controller, true);
		}

		private void ViewModel_RequestCancelPage()
		{
			var controller = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");

			NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { controller }, true);
		}


		private void InitBindings ()
		{
			ViewModel.RequestProcedureLookupPage = ViewModel_RequestProcedureLookup;

			ViewModel.RequestPostSelectionPage = ViewModel_RequestPostSelectionPage;

			ViewModel.RequestCancelPage = ViewModel_RequestCancelPage;

			PromptButton.SetCommand ("Clicked", ViewModel.ProcedurePromptCommand);

			CancelButton.SetCommand ("TouchUpInside", ViewModel.CancelCommand);

			ProgressView.Progress = ViewModel.Progress;
		}
	}
}
