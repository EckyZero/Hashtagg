using System;
using UIKit;
using Shared.VM;
using System.Collections.Generic;

namespace iOS.Phone
{
	public abstract class BaseRecommendationDetailCell : UITableViewCell
	{
		protected List<UILabel> _expandingLabels = new List<UILabel>();

		protected BaseRecommendationDetailCell (IntPtr handle) : base (handle)
		{
		}

		protected BaseRecommendationDetailCell () : base ()
		{
		}

		public virtual nfloat CalculateHeight (BaseRecommendationDetailListItemViewModel itemViewModel) 
		{
			nfloat height = 0;

			if(Device.OS < 8)
			{
				Configure (itemViewModel);

				height = ContentView.SystemLayoutSizeFittingSize (UILayoutFittingCompressedSize).Height;

				foreach(UILabel label in _expandingLabels)
				{
					height += label.HeightToFitContent ();
				}	
			}
			else
			{
				height = UITableView.AutomaticDimension;
			}

			return height;
		}

		public abstract void Configure (BaseRecommendationDetailListItemViewModel viewModel);

		public static string GetIdentifier (BaseRecommendationDetailListItemViewModel viewModel)
		{
			var identifier = "";

			switch(viewModel.DetailType)
			{
			case RecommendationDetailType.DoctorSummary:
				identifier = DrBioSummaryCell.Key;
				break;
			case RecommendationDetailType.Superlative:
				identifier = SuperlativeCell.Key;
				break;
			case RecommendationDetailType.Availability:
				var vm = (DoctorRecommendationDetailAvailabilityViewModel)viewModel;
				identifier = vm.IsPreferredAddress ? AvailabilityCell.Key : AddressCell.Key;
				break;
			case RecommendationDetailType.Staff:
				identifier = StaffCell.Key;
				break;
			case RecommendationDetailType.Action:
				identifier = DrActionCell.Key;
				break;
			case RecommendationDetailType.Header:
				identifier = BasicHeaderCell.Key;
				break;
			case RecommendationDetailType.Facility:
				identifier = FacilityPrivilegesCell.Key;
				break;
			case RecommendationDetailType.Pricing:
				identifier = PricingCell.Key;
				break;
			case RecommendationDetailType.FacilitySummary:
				identifier = FacilityBioSummaryCell.Key;
				break;
			case RecommendationDetailType.ProcedureCost:
				identifier = CostCell.Key;
				break;
			case RecommendationDetailType.FacilityToDoctorRecommendation:
				identifier = FacilityToDoctorRecommendationCell.Key;
				break;
			}
			return identifier;
		}
	}
}

