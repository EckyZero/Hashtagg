using System;
using Shared.VM;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Foundation;
using Shared.Common;

namespace iOS.Phone
{
	public class HealthDetailsResultTableController : BaseTableController
	{
		public HealthDetailsResultTableController () : base()
		{
		}

		public override BaseTableSource InitDataSource (BaseListViewModel viewModel, GenericTableController childController)
		{
			return new HealthDetailsResultListTableSource ((HealthDetailsResultListViewModel)viewModel, childController);
		}

		protected override void InitViewModel ()
		{
			ViewModel = new HealthDetailsResultListViewModel ();
			ViewModel.NextButtonTitle = ApplicationResources.ViewMyIncentives;
			ViewModel.RequestNextPage += OnRequestNextPage;
		}

		private void OnRequestNextPage (object sender, EventArgs args)
		{
			var controller = UIStoryboard.FromName ("IncentiveStoryboard", null).InstantiateViewController("IncentivesSummaryTableController") as IncentivesSummaryTableController;
			ParentViewController.ParentViewController.NavigationController.PushViewController (controller, true);
		}
	}

	public class HealthDetailsResultListTableSource : BaseTableSource
	{
		private HealthDetailsResultListViewModel  _viewModel;
		private GenericTableController _childController;

		public HealthDetailsResultListTableSource (HealthDetailsResultListViewModel viewModel, GenericTableController childController) : base (viewModel, childController)
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
			var count = _viewModel.Models.Count;

			return count;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 60;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = _childController.TableView.DequeueReusableCell (BiometricResultCell.Key) as BiometricResultCell;
			BiometricResultCellViewModel viewModel = (BiometricResultCellViewModel) _viewModel.Models [indexPath.Row];

			cell.Title = viewModel.Title;
			cell.Subtitle = viewModel.Subtitle;
			cell.OnTouchUpInside = viewModel.OnTouchAction;
			cell.MeasurementDate = viewModel.MeasurementDate;
			cell.Status = viewModel.Status;
			cell.MeasurementValue = viewModel.Measurement;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			BiometricResultCellViewModel viewModel = (BiometricResultCellViewModel) _viewModel.Models [indexPath.Row];
			OnRequestCellActionPage (viewModel);
		}

		protected override void OnRequestCellActionPage (BaseListItemViewModel viewModel)
		{
			var controller = UIStoryboard.FromName ("IncentiveStoryboard", null).InstantiateViewController("BiometricResultDetailController") as BiometricResultDetailController;

			controller.BiometricData = _viewModel.GetBiometricHistoryFor((BiometricResult)viewModel.Model);

			_childController.ParentViewController.ParentViewController.NavigationController.PushViewController (controller, true);
		}
	}
}

