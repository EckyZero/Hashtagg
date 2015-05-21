using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Shared.VM;
using Shared.BL;
using System.Collections.Generic;
using Shared.Common.Models.TuringTree;

namespace iOS.Phone
{
	[Register ("SurveyDoctorPromptController")]
	partial class SurveyDoctorPromptController : UIViewController
	{
		public SurveyDoctorPromptViewModel ViewModel { get; private set; }

		public SurveyDoctorPromptController (IntPtr handle) : base (handle) 
		{
			ViewModel = new SurveyDoctorPromptViewModel ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			Title = ViewModel.Title;
			PromptButton.Text = ApplicationResources.SearchADoctorsName;
		}

		private async void viewModel_RequestNoAddPage()
		{
			if (ViewModel.DestinationViewModel != null && ViewModel.DestinationViewModel.Node != null) {
				var controller = new BaseSurveyTableController (ViewModel.DestinationViewModel);

				NavigationController.PushViewController (controller, true);
			} else {
				ExtendedDialogService dialogService = IocContainer.GetContainer ().Resolve<IExtendedDialogService> () as ExtendedDialogService;
				await dialogService.ShowMessage(ApplicationResources.GenericError, "Error Occured");
			}
		}

		private void viewModel_RequestSkipPage()
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);
			var controller = storyboard.InstantiateViewController ("DoctorPreferencesController") as DoctorPreferencesController;

			controller.FlowData = ViewModel.DestinationViewModel.FlowData;
			controller.TreeMap = ViewModel.DestinationViewModel.Map;

			NavigationController.PushViewController (controller, true);
		}

		private void viewModel_RequestCancelPage()
		{
			var controller = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");
			NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { controller }, true);
		}

		private void viewModel_RequestLookupPage(IList<ILookupFilter> filters)
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController("DoctorLookupListController") as DoctorLookupListController;

			controller.Filters = filters;

			controller.OnSelectAndReturnProvider = OnSelectAndReturnProvider;

			var navController = new UINavigationController(controller);
			NavigationController.PresentViewController (navController, true, null);
		}

		private void OnSelectAndReturnProvider(Provider provider)
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);
			var controller = storyboard.InstantiateViewController ("SurveyDoctorPromptListController") as SurveyDoctorPromptListController;

			controller.ViewModel.Providers.Add (provider);
			controller.ViewModel.DestinationViewModel = ViewModel.DestinationViewModel;
			controller.ViewModel.DoctorLookupFilters = ViewModel.DoctorLookupFilters;
			controller.ViewModel.Title = ViewModel.Title;

			NavigationController.PushViewController (controller, false);
		}

		private void InitBindings ()
		{
			ViewModel.RequestLookupPage = viewModel_RequestLookupPage;
			PromptButton.SetCommand ("Clicked", ViewModel.DoctorPromptCommand);

			ViewModel.RequestNoAddPage = viewModel_RequestNoAddPage;
			_iDontHaveOneButton.SetCommand ("TouchUpInside", ViewModel.NoDoctorsToAddCommand);

			ViewModel.RequestSkipPage = viewModel_RequestSkipPage;
			SkipThisPartButton.SetCommand ("TouchUpInside", ViewModel.SkipCommand);

			ViewModel.RequestCancelPage = viewModel_RequestCancelPage;
			CancelButton.SetCommand ("TouchUpInside", ViewModel.CancelCommand);

			ProgressView.Progress = ViewModel.Progress;
		}
	}
}
