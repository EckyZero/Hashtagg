using System;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using Shared.Common.Models.TuringTree;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

namespace iOS.Phone
{
	public partial class MyProvidersTableController : UITableViewController
	{
		private MyDoctorsListViewModel _viewModel;

		public MyProvidersTableController (IntPtr handle) : base (handle)
		{
			_viewModel = new MyDoctorsListViewModel();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.Source = new MyProvidersTableSource (_viewModel, this);

			DocPrefButton.SetCommand("TouchUpInside", _viewModel.DoctorPreferencesCommand);

			DocRecButton.SetCommand("TouchUpInside", _viewModel.NeedADoctorCommand);

			AddAnotherDoctorButton.SetCommand("TouchUpInside", _viewModel.AddADoctorCommand);

			_viewModel.RequestPromptPage += _viewModel_RequestPromptPage;
			_viewModel.RequestLookupPage += _viewModel_RequestLookupPage;
			_viewModel.RequestDocRec += _viewModel_RequestDocRec;
			_viewModel.RequestPrefsPage += _viewModel_RequestPrefsPage;
			_viewModel.RequestRefreshPage += OnRequestRefreshPage;
		}

		private void _viewModel_RequestPrefsPage (object sender, EventArgs e)
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);

			var controller = storyboard.InstantiateViewController ("DoctorPreferencesController") as DoctorPreferencesController;
			controller.Mode = DoctorPreferencesMode.EditOnly;

			var navController = new UINavigationController (controller);
			navController.ConfigureToCompassDefaults ();
			ParentViewController.NavigationController.PushViewController (controller, false);
		}

		private void _viewModel_RequestPromptPage (object sender, EventArgs e)
		{
			NavigationController.PopViewController (false);
		}

		void _viewModel_RequestLookupPage (object sender, EventArgs e)
		{
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("DoctorLookupListController") as DoctorLookupListController;
			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults (false);

			this.PresentViewController (navController, true, () => TableView.ScrollToBottom (true));
		}

		void _viewModel_RequestDocRec (object sender, SurveyViewModel e)
		{
			if (e != null) {
				UIViewController controller = new BaseSurveyTableController(e);

				ParentViewController.NavigationController.PushViewController (controller, false);
			}
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			await _viewModel.WillAppear ();

			TableView.ReloadData ();
		}

		public override async void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			await _viewModel.WillDisappear ();
		}

		public void OnRequestRefreshPage (object sender, EventArgs args)
		{
			TableView.ReloadData ();
		}
	}

	public class MyProvidersTableSource: UITableViewSource
	{
		protected MyDoctorsListViewModel _viewModel;

		protected UITableViewController _controller;

		public MyProvidersTableSource(MyDoctorsListViewModel viewModel, UITableViewController controller)
		{
			_viewModel = viewModel;
			_controller = controller;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return 0;
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			return 0;
		}

		public void OnRequestActionPage (MyDoctorListItemViewModel viewModel)
		{
			var actionSheet = new UIActionSheet();


			Dictionary<int, RelayCommand> actionsMap = new Dictionary<int, RelayCommand> ();

			int actionIndex = 0;

			if (viewModel.ShowPhoneImage) {
				actionSheet.AddButton (ApplicationResources.CallDoctor);
				actionsMap.Add (actionIndex++, viewModel.CallDoctorCommand);
			}

			actionSheet.AddButton(ApplicationResources.ScheduleApptWithHealthPro);
			actionsMap.Add (actionIndex++, viewModel.ScheduleHealthProCommand);

			actionSheet.AddButton(ApplicationResources.RemoveFromMyDoctors);
			actionsMap.Add (actionIndex++, viewModel.DeleteProviderCommand);

			if (viewModel.ShowRateDoctor) {
				actionSheet.AddButton (ApplicationResources.RateDoctor);
				actionsMap.Add (actionIndex++, viewModel.RateDoctorCommand);
			}

			actionSheet.AddButton(ApplicationResources.Cancel);
			actionSheet.DestructiveButtonIndex = actionIndex;

			actionSheet.WillDismiss += (object sender, UIButtonEventArgs e) => OnActionComplete(viewModel, (int)e.ButtonIndex, actionsMap);

			actionSheet.ShowInView (_controller.ParentViewController.View);
		}

		private void OnActionComplete(MyDoctorListItemViewModel viewModel, int action, Dictionary<int, RelayCommand> map)
		{
			if(map.ContainsKey(action)){
				map [action].Execute (viewModel);
			}
		}

		public void OnRequestDetailPage (DoctorDetail detail)
		{
			DoctorRecommendationDetailListViewModel vm = new DoctorRecommendationDetailListViewModel (detail, null, RecommendationType.Mine, null, null, new MapMarkerViewModel (detail, MapMarkerType.Other));
			var controller = new DoctorRecommendationDetailController (vm, null);
			_controller.ParentViewController.NavigationController.PushViewController (controller, true);
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);
			var identifier = MyProviderCell.Key;
			var cell = tableView.DequeueReusableCell (identifier) as MyProviderCell;

			nfloat height = 180;

			if (viewModel != null) {
				height = cell.CalculateHeight (viewModel);
			}

			return height;
		}

		public override nfloat EstimatedHeight (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);
			var identifier = MyProviderCell.Key;
			var cell = tableView.DequeueReusableCell (identifier) as MyProviderCell;

			nfloat height = 180;

			if (viewModel != null) {
				height = cell.CalculateHeight (viewModel);
			}

			return height;
		}
			
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			var count = _viewModel.PatientProviderViewModels.Count;
			return count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);

			var identifier = MyProviderCell.Key;
			var cell = tableView.DequeueReusableCell (identifier) as MyProviderCell;

			if (viewModel != null) {

				viewModel.RequestActionPage = OnRequestActionPage;
				viewModel.RequestDoctorDetailPage = OnRequestDetailPage;

				cell.Configure (viewModel);
			}

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);
			if (viewModel != null) {
				viewModel.GoToBioCommand.Execute (null);
			}
		}
			
		private MyDoctorListItemViewModel GetViewModel (NSIndexPath indexPath)
		{
			MyDoctorListItemViewModel viewModel = null;

			if (_viewModel.PatientProviderViewModels.Count > indexPath.Row) {
				viewModel = _viewModel.PatientProviderViewModels[indexPath.Row] as MyDoctorListItemViewModel;
			}	

			return viewModel;
		}
	}
}
