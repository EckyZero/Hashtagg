using System;
using Shared.VM;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Foundation;
using Shared.Common;

namespace iOS.Phone
{
	public class HealthDetailsConditionTableController : BaseTableController
	{
		public HealthDetailsConditionTableController () : base () {}

		public override BaseTableSource InitDataSource (BaseListViewModel viewModel, GenericTableController childController)
		{
			return new HealthDetailsConditionListTableSource ((HealthDetailsConditionListViewModel)viewModel, childController);
		}

		protected override void InitViewModel ()
		{
			ViewModel = new HealthDetailsConditionListViewModel ();
		}
	}

	public class HealthDetailsConditionListTableSource : BaseTableSource
	{
		private HealthDetailsConditionListViewModel  _viewModel;
		private GenericTableController _childController;

		public HealthDetailsConditionListTableSource (HealthDetailsConditionListViewModel viewModel, GenericTableController childController) : base (viewModel, childController)
		{
			_viewModel = viewModel;
			_childController = childController;
			_viewModel.RequestAddPage += OnRequestAddPage;
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
			
		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			return 44;
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			GenericAddCell view = _childController.TableView.DequeueReusableCell (GenericAddCell.Key) as GenericAddCell;

			view.Title = _viewModel.AddButtonTitle;
			view.SetCommand ("Action", _viewModel.AddCommand);

			return view;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = _childController.TableView.DequeueReusableCell (GenericEditCell.Key) as GenericEditCell;
			var viewModel = _viewModel.Models [indexPath.Row];

			cell.Title = viewModel.Title;
			cell.Subtitle = viewModel.Subtitle;
			cell.TouchUpAction = viewModel.OnTouchAction;
			cell.BottomConstraint = 12;

			viewModel.RequestActionPageAction = OnRequestCellActionPage;
			viewModel.RequestEditPageAction = OnRequestCellEditPage;

			return cell;
		}

		private void OnRequestAddPage (object sender, EventArgs args)
		{
			var controller = new HealthDetailsConditionLookupController();
			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults (false);

			_childController.ParentViewController.PresentViewController (navController, true, null);
		}

		protected override void OnRequestCellActionPage (BaseListItemViewModel viewModel)
		{
			var controller = new ModifyAlertController<IIdentifiable>(viewModel);

			if(Device.OS >= 8)
			{
				_childController.ParentViewController.PresentViewController (controller.Alert, true, null);	
			}
			else
			{
				controller.Show ();
			}
		}

		protected override void OnRequestCellEditPage (BaseListItemViewModel viewModel) 
		{
			var controller = new HealthDetailsConditionLookupController();
			var navController = new UINavigationController (controller);

			// Pass a delete command to be performed after selection
			controller.SearchBar.Text = viewModel.Title;
			controller.OnSelect = ((condition) => {
				viewModel.DeleteCommand.Execute (null);	
			});

			navController.ConfigureToCompassDefaults (false);

			_childController.ParentViewController.PresentViewController (navController, true, null);
		}
	}
}

