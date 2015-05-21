using System;
using Shared.VM;
using Shared.Common;
using UIKit;

namespace iOS.Phone
{
	public class FacilityRecommendationDetailController : BaseRecommendationDetailController
	{
        public FacilityRecommendationDetailController(BaseRecommendationDetailListViewModel viewModel, PatientPreferences patientPreferences) : base(viewModel, patientPreferences) { }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationItem.SetRightBarButtonItem(null, false);
			ChildController.HideFooterView (false);
			ChildController.HideTableHeader ();
			ChildController.IsFooterEnabled = false;
			ChildController.ShowBottomSeparator = true;
		}

		public override UITableViewSource InitDataSource ()
		{
			return new BaseRecommendationsDetailTableSource (_viewModel, ChildController);
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
}

