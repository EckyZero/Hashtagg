using System;
using UIKit;
using Shared.Common;
using Shared.VM;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using System.Linq;

namespace iOS.Phone
{
	public class HealthDetailsProcedureTableController : BaseTableController
	{
		public HealthDetailsProcedureTableController () : base() {}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ChildController.TableView.RowHeight = 70;
		}

		public override BaseTableSource InitDataSource (BaseListViewModel viewModel, GenericTableController childController)
		{
			// The generic controller requires a reference to this table source
			return new HealthDetailsProcedureListTableSource ((HealthDetailsProcedureListViewModel)viewModel, childController);
		}

		protected override void InitViewModel ()
		{
			ViewModel = new HealthDetailsProcedureListViewModel ();
		}
	}

	public class HealthDetailsProcedureListTableSource : BaseTableSource
	{
		private HealthDetailsProcedureListViewModel  _viewModel;
		private GenericTableController _childController;

		public HealthDetailsProcedureListTableSource (HealthDetailsProcedureListViewModel viewModel, GenericTableController childController) : base (viewModel, childController)
		{
			_viewModel = viewModel;
			_childController = childController;
			_viewModel.RequestAddPage += OnRequestAddPage;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			var count = _viewModel.GetStructuredProcedures ().Count;

			return count;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			var keyvalue = _viewModel.GetStructuredProcedures()[(int)section];
			var count = keyvalue.Value.Count;

			return count;
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			var view = _childController.TableView.DequeueReusableCell (GenericSectionHeader.Key) as GenericSectionHeader;
			var title = _viewModel.GetStructuredProcedures()[(int)section].Key;

			view.Title = title;

			return view;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			var keyvalue = _viewModel.GetStructuredProcedures()[(int)section];
			var height = 25;

			if(!keyvalue.Value.SequenceEqual(_viewModel.GetFutureProcedures()))
			{
				height = 44;
			}
			return height;
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			var keyvalue = _viewModel.GetStructuredProcedures()[(int)section];
			var height = 1;

			if(keyvalue.Value.SequenceEqual(_viewModel.GetFutureProcedures()))
			{
				height = 44;
			}
			return height;
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			var keyvalue = _viewModel.GetStructuredProcedures()[(int)section];
			UIView view = null;

			if(keyvalue.Value.SequenceEqual(_viewModel.GetFutureProcedures()))
			{
				view = _childController.TableView.DequeueReusableCell (GenericAddCell.Key) as GenericAddCell;
				view.SetCommand ("Action", _viewModel.AddCommand);
			}
			else
			{
				view = new UIView (new CoreGraphics.CGRect (0, 0, _childController.View.Frame.Width, 1));
				view.BackgroundColor = SharedColors.Tan2.ToUIColor ();
			}

			return view;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = _childController.TableView.DequeueReusableCell (GenericEditCell.Key) as GenericEditCell;
			var viewModel = _viewModel.GetStructuredProcedures () [(int)indexPath.Section].Value [indexPath.Row];

			cell.Title = viewModel.Title;
			cell.Subtitle = viewModel.Subtitle;
			cell.BackgroundColor = viewModel.BackgroundColor.ToUIColor();
			cell.TouchUpAction = viewModel.OnTouchAction;

			viewModel.RequestActionPageAction = OnRequestCellActionPage;
			viewModel.RequestEditPageAction = OnRequestCellEditPage;

			return cell;
		}

		private void OnRequestAddPage (object sender, EventArgs args)
		{
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("ProcedureLookupListController") as ProcedureLookupListController;
			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults (false);

			_childController.ParentViewController.PresentViewController (navController, true, null);
		}

		protected override void OnRequestCellSelectPage (BaseListItemViewModel viewModel)
		{
			System.Diagnostics.Debug.WriteLine (viewModel);
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
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController("ProcedurePromptInformationController") as ProcedurePromptInformationController;
			var navController = new UINavigationController (controller);

			controller.SelectedPatientProcedure = (PatientProcedure)viewModel.Model;
			controller.EditMode = true;

			navController.ConfigureToCompassDefaults (false);

			_childController.ParentViewController.PresentViewController (navController, true, null);
		}
	}
}