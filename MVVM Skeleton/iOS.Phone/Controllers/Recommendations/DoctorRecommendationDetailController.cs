
using System;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using System.Linq;

namespace iOS.Phone
{
	public partial class DoctorRecommendationDetailController : BaseRecommendationDetailController
	{
        public DoctorRecommendationDetailController(BaseRecommendationDetailListViewModel viewModel, PatientPreferences patientPreferences) : base(viewModel, patientPreferences) { }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var vm = (DoctorRecommendationDetailListViewModel)_viewModel;

			ChildController.IsYourDoctor = vm.IsMyDoctor;
			ChildController.SaveBarButton.Title = vm.AddRemoveText;
			ChildController.SaveBarButton.Clicked += (sender, e) => vm.ActionViewModel.AddRemoveCommand.Execute (null);

			vm.ActionViewModel.RequestHealthProSchedulePage = OnRequestHealthProSchedulePage;
			vm.ActionViewModel.RequestAddRemovePageUpdate += OnRequestAddRemovePageUpdate;
			vm.RequestFacilityPage = OnRequestFacilityPage;

			// Add footer
			var cell = ChildController.Deqeue(DrActionCell.Key) as DrActionCell;
			cell.Configure (vm.ActionViewModel);
			cell.Frame = new CoreGraphics.CGRect (0, 0, this.View.Frame.Width, cell.CalculateHeight (vm.ActionViewModel));

			ChildController.AddFooterView (cell);

			// Hide on first load
			ChildController.HideFooterView(false);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override UITableViewSource InitDataSource ()
		{
			return new DoctorRecommendationDetailTableSource (_viewModel, ChildController);
		}

		private void OnRequestAddRemovePageUpdate (object sender, AddRemoveEventArgs args)
		{
			var vm = (DoctorRecommendationDetailListViewModel)_viewModel;

			ChildController.SaveBarButton.Title = vm.AddRemoveText;
			ChildController.IsYourDoctor = vm.IsMyDoctor;
		}

		private void OnRequestFacilityPage (FacilityRecommendationDetailListViewModel viewModel)
		{
			var controller = new FacilityRecommendationDetailController(viewModel, _patientPreferences);
			NavigationController.PushViewController (controller, true);
		}

		public override void HydrateMap()
		{
			ChildController.Map.Delegate = new GenericRecommendationsMapDelegate ();

			ChildController.Map.RemoveAnnotations (ChildController.Map.Annotations);

			foreach(var marker in _viewModel.MapMarkers)
			{
				var annotation = new DoctorRecommendationAnnotation (marker);
				ChildController.Map.AddAnnotation (annotation);
			}
			ChildController.Map.CenterAroundAnnotations ();
		}

		private void OnRequestHealthProSchedulePage(Provider provider)
		{
			HealthProQuestionResponseController controller = UIStoryboard.FromName ("SettingsStoryboard", null).InstantiateViewController ("HealthProQuestionResponseController") as HealthProQuestionResponseController;
			controller.SelectedQuestion = new HealthProQuestionViewModel (HealthProQuestionKeys.Appointment);

			var vm = (DoctorRecommendationDetailListViewModel)_viewModel;
			controller.NonUserVisableComments = TuringTreeHelpers.FlowDataWalkWithMapToHumanReadableText (vm.TreeMap, vm.FlowData);

			if (provider != null) {
				controller.ViewModel.ProviderId = provider.Id.ToString ();
				controller.ViewModel.ProviderName = provider.DoctorName;
				controller.ViewModel.QuestionComment = TuringTreeHelpers.ProviderScheduleComments (vm.TreeMap, vm.FlowData, provider.DoctorName);

			} else {
				controller.ViewModel.QuestionComment = TuringTreeHelpers.ProviderScheduleComments(vm.TreeMap, vm.FlowData, string.Empty);
			}

			var procedureInformation = TuringTreeHelpers.GetCostEstimateProcedureInformationIfAvailableAndCostEstimateFlow (vm.FlowData);
			if (procedureInformation != null) {
				controller.ViewModel.ProcedureId = procedureInformation.Item2;
				controller.ViewModel.ProcedureName = procedureInformation.Item1;			
			}

			//Since we do not have health pro from previous page, ready it up.
			controller.ViewModel.ReadyHealthProFromBL();
			controller.ViewModel.RequestPostSendPage =  () => {
				var returnController = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");
				NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { returnController }, true);
			};
			controller.ViewModel.RequestCancelPage = () => 	{
				var returnController = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");
				NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { returnController }, true);
			};

			NavigationController.PushViewController(controller,true);
		}

	}

	public class DoctorRecommendationDetailTableSource : BaseRecommendationsDetailTableSource
	{
		public DoctorRecommendationDetailTableSource (BaseRecommendationDetailListViewModel viewModel, GenericRecommendationDetailController childController) : base (viewModel, childController)
		{
			
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var vm = (DoctorRecommendationDetailListViewModel)_viewModel;
			var facilityVM = _viewModel.ViewModels[indexPath.Row];

			if(facilityVM.GetType() == typeof(DoctorRecommendationDetailFacilityViewModel))
			{
				vm.FacilityPrivilegesCommand.Execute (facilityVM);
			}
		}
	}
}

