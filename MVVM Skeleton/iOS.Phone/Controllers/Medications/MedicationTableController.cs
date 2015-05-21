using System;
using Shared.VM;
using UIKit;
using GalaSoft.MvvmLight.Helpers;
using Foundation;
using Shared.Common;

namespace iOS.Phone
{
	 public class MedicationTableController : BaseTableController
	{
		public MedicationTableController () : base () { }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ChildController.TableView.RowHeight = 70;

			NavigationController.NavigationBarHidden = true;
		}
			
		public override BaseTableSource InitDataSource (BaseListViewModel viewModel, GenericTableController childController)
		{
			return new MedicationListTableSource ((MedicationListViewModel)viewModel, childController);
		}

		protected override void InitViewModel ()
		{
			ViewModel = new MedicationListViewModel ();
		}
	}

	public class MedicationListTableSource : BaseTableSource 
	{
		private MedicationListViewModel  _viewModel;
		private GenericTableController _childController;

		public MedicationListTableSource (MedicationListViewModel viewModel, GenericTableController childController) : base (viewModel, childController)
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
			var viewModel = _viewModel.Models [(int)indexPath.Row];

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
			var controller = storyboard.InstantiateViewController ("PrescriptionLookupListController") as PrescriptionLookupListController;
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
			var controller = storyboard.InstantiateViewController("PrescriptionInformationController") as PrescriptionInformationController;
			var vm = viewModel as MedicationListItemViewModel;

			controller.SelectedPrescription = vm.Prescription;
			controller.EditMode = true;

			var navController = new UINavigationController (controller);
			navController.ConfigureToCompassDefaults (false);

			_childController.ParentViewController.PresentViewController (navController, true, null);
		}
	}
}

