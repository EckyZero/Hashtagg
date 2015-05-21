using System;
using Shared.Common;
using UIKit;
using Shared.VM;
using Shared.Common.Models.TuringTree;

namespace iOS.Phone
{
	public class FacilityRecommendationController : BaseRecommendationController
	{
		public FacilityRecommendationController (FacilityRecommendation recommendation, FlowData flowData, TreeMap treeMap, PatientPreferences patientPreferences, GeoLocation preferredLocation = null) : base()
		{
			_viewModel = new FacilityRecommendationListViewModel (recommendation, flowData, treeMap, patientPreferences, preferredLocation);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ChildController.ShowReviewLogo = false;
			ChildController.TableViewHeaderText = ApplicationResources.MyFacilities;
			ChildController.ShowMoreText = _viewModel.ShowMoreText;
		}

		public override UITableViewSource InitDataSource ()
		{
			return new FacilityRecommendationTableSource (_viewModel as FacilityRecommendationListViewModel, ChildController);
		}
			
		public override void OnRequestFullMap ()
		{
			var controller = new FacilityRecommendationsMapController (_viewModel.MapMarkers, _viewModel.FlowData, _viewModel.PatientPreferences, OnComeBackFromFullMap, _viewModel.PreferredLocation);
			ChildController.ParentViewController.NavigationController.PushViewController (controller, true);
		}

		public void OnComeBackFromFullMap(IRecommendation docRec, Location newLocation)
		{
			if(newLocation.Address != null){
				_viewModel.PreferredLocation = newLocation.Address.Geolocation;
			}

			RefreshData (docRec);
		}

		public override void HydrateMap()
		{
			ChildController.Map.Delegate = new GenericRecommendationsMapDelegate ();

			ChildController.Map.RemoveAnnotations (ChildController.Map.Annotations);

			foreach(var marker in _viewModel.MapMarkers)
			{
				var annotation = new FacilityRecommendationAnnotation (marker);
				ChildController.Map.AddAnnotation (annotation);
			}

			ChildController.Map.CenterAroundAnnotations ();
		}

	}

	#region TableView Datasource

	public class FacilityRecommendationTableSource : BaseRecommendationsTableSource
	{
		public FacilityRecommendationTableSource(FacilityRecommendationListViewModel viewModel, GenericRecommendationController childController) : base (viewModel, childController) { }

		public override void OnRequestActionPage (BaseRecommendationListItemViewModel baseViewModel)
		{
			var viewModel = baseViewModel as FacilityRecommendationListItemViewModel;
			var actionSheet = new UIActionSheet();

			actionSheet.AddButton(ApplicationResources.CallFacility);
			actionSheet.AddButton(ApplicationResources.ScheduleApptWithHealthPro);
			actionSheet.AddButton(ApplicationResources.Cancel);
			actionSheet.DestructiveButtonIndex = 2;
			actionSheet.WillDismiss += (object sender, UIButtonEventArgs e) => {
				switch (e.ButtonIndex)
				{
				case 0:
					viewModel.CallCommand.Execute(null);
					break;
				case 1:
					viewModel.ScheduleCommand.Execute(null);
					break;
				default:
					break;
				}
			};
			actionSheet.ShowInView (_childController.ParentViewController.View);
		}

		public override void OnRequestDetailPage (BaseRecommendationDetailListViewModel baseVM, PatientPreferences patientPreferences)
		{
			var controller = new FacilityRecommendationDetailController ((FacilityRecommendationDetailListViewModel)baseVM, patientPreferences);
			_childController.ParentViewController.NavigationController.PushViewController (controller, true);
		}
	}

	#endregion
}

