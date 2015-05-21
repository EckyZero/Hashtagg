
using System;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using System.Linq;

namespace iOS.Phone
{
	public abstract class BaseRecommendationDetailController : UIViewController
	{
		#region Private Variables

		protected BaseRecommendationDetailListViewModel _viewModel;
		protected PatientPreferences _patientPreferences;

		#endregion

		#region Properties

		public GenericRecommendationDetailController ChildController { get; set; }

		#endregion

		#region Constructors

		public BaseRecommendationDetailController () : base ()  { }

        public BaseRecommendationDetailController(BaseRecommendationDetailListViewModel viewModel, PatientPreferences patientPreferences)
            : base()  
		{ 
			_viewModel = viewModel;
			_patientPreferences = patientPreferences;
		}

		#endregion

		#region Methods

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ChildController = GenericRecommendationDetailController.Create (_viewModel, this);

			// Set the tooltip action listener
			var pricingVM = _viewModel.ViewModels.FirstOrDefault ( avm => avm.DetailType == RecommendationDetailType.Pricing) as DoctorRecommendationDetailPricingViewModel;
			var costVM = _viewModel.ViewModels.FirstOrDefault (avm => avm.DetailType == RecommendationDetailType.ProcedureCost) as ProcedureCostViewModel;

			if(pricingVM != null)
			{
				pricingVM.RequestTooltipPage = OnRequestPricingTooltipPage;	
			}
			if(costVM != null)
			{
				costVM.RequestTooltipPage = OnRequestCostTooltipPage;
			}
			HydrateMap ();
		}

		public abstract UITableViewSource InitDataSource ();
		public abstract void HydrateMap();

		private void OnRequestPricingTooltipPage (CommonProcedure commonProcedure)
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("ToolTipController") as ToolTipController;

			controller.Title = commonProcedure.Procedure.Description;
			controller.Text = commonProcedure.Summary;

			ChildController.ParentViewController.NavigationController.PushViewController (controller, true);
		}

		private void OnRequestCostTooltipPage (ProcedureCostItem item)
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("ToolTipController") as ToolTipController;

			controller.Title = item.ItemName;
			controller.Text = item.Summary;

			ChildController.ParentViewController.NavigationController.PushViewController (controller, true);
		}

		#endregion

	}

	public class BaseRecommendationsDetailTableSource : UITableViewSource
	{
		protected BaseRecommendationDetailListViewModel _viewModel;
		protected GenericRecommendationDetailController _childController;

		public BaseRecommendationsDetailTableSource (BaseRecommendationDetailListViewModel viewModel, GenericRecommendationDetailController childController)
		{
			_viewModel = viewModel;
			_childController = childController;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			var count = _viewModel.ViewModels.Count;

			return count;
		}

		public override void Scrolled (UIScrollView scrollView)
		{
			if(scrollView.ContentSize.Height > scrollView.Frame.Height)
			{

				if(scrollView.ContentOffset.Y > 0)
				{ // Show
					if(!_childController.IsFooterShowing && _childController.IsFooterEnabled)
					{
						_childController.ShowFooterView();	
					}
				}
			}
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			return _childController.ShowBottomSeparator ? 1 : 0;
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			UIView view = null;

			if(_childController.ShowBottomSeparator)
			{
				view = new UIView(new CoreGraphics.CGRect(0,0,tableView.Frame.Size.Width, 1));
				view.BackgroundColor = SharedColors.Tan2.ToUIColor ();	
			}
			return view;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = _viewModel.ViewModels[indexPath.Row];
			var identifier = BaseRecommendationDetailCell.GetIdentifier(viewModel);
			var cell = tableView.DequeueReusableCell (identifier) as BaseRecommendationDetailCell;
			var height = cell.CalculateHeight(viewModel);

			return height;
		}

		public override nfloat EstimatedHeight (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = _viewModel.ViewModels[indexPath.Row];
			var identifier = BaseRecommendationDetailCell.GetIdentifier(viewModel);
			var cell = tableView.DequeueReusableCell (identifier) as BaseRecommendationDetailCell;
			var height = cell.CalculateHeight(viewModel);

			return height;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = _viewModel.ViewModels[indexPath.Row];
			var identifier = BaseRecommendationDetailCell.GetIdentifier(viewModel);
			var cell = tableView.DequeueReusableCell (identifier) as BaseRecommendationDetailCell;

			cell.Configure (viewModel);

			return cell;
		}
	}
}

