using System;
using Shared.Common;
using Shared.VM;
using System.Collections.Generic;
using UIKit;

namespace iOS.Phone
{
	public class FacilityRecommendationsMapController : BaseRecommendationsMapController
	{
		public FacilityRecommendationsMapController (IList<MapMarkerViewModel> currentMarkers, FlowData flowData, PatientPreferences patientPreferences, Action<IRecommendation, Location> refreshCallback, GeoLocation preferredLocation)
		{
			_viewModel = new FacilityRecommendationsMapViewModel ();
			_viewModel.RequestLocationLookupPage = OnRequestLocationLookupPage;
			_viewModel.PatientPreferences = patientPreferences;
			_viewModel.FlowData = flowData;

			if(preferredLocation != null && patientPreferences.PreferredLocation != null && !preferredLocation.Equals(patientPreferences.PreferredLocation.Geolocation)){
				_viewModel.PreferredLocation = new LocationLookupCellViewModel (){ Location = new Location{ Address = new Address{ Geolocation = preferredLocation } } };
			}

			_viewModel.RequestRefreshMap = OnRequestMapRefresh;
			_viewModel.RequestPreviousPage = refreshCallback;
			_viewModel.RequestKebobMenu = OnRequestActionPage;

			_viewModel.Markers = currentMarkers;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Map.Delegate = new FacilityRecommendationsFullMapDelegate (Map, ChildController.RedoSearchView, ChildController.View);

			HydrateMap ();
		}


		public override void HydrateMap()
		{
			Map.RemoveAnnotations (Map.Annotations);

			foreach(var marker in _viewModel.Markers)
			{
				var annotation = new FacilityRecommendationAnnotation (marker);
				Map.AddAnnotation (annotation);
			}

			Map.CenterAroundAnnotations ();
			ChildController.MapLoadedWithMarkers ();
		}

		public override void OnRequestLocationLookupPage ()
		{
			var controller = new DrPreferencesLocationLookupController();
			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults ();

			controller.OnSelect = async (locationVm) => {
				NavigationController.DismissViewController(true, null);

				_viewModel.PreferredLocation = locationVm;

				GeoLocation preferredCenter = null;
				if(locationVm != null && locationVm.Location != null && locationVm.Location.Address != null){
					preferredCenter = locationVm.Location.Address.Geolocation;
				}

				//TODO: do we need the distance so we can search within the zoomed in radius?
				await _viewModel.GetRecommendation(preferredCenter);

			};

			NavigationController.PresentViewController (navController, true, null);
		}

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
			actionSheet.ShowInView (ChildController.View);
		}

		public override void OnRequestMapRefresh ()
		{
			HydrateMap ();
		}

	}
}

