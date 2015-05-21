using System;
using UIKit;
using Shared.Common;
using Foundation;
using System.Collections.Generic;
using Shared.VM;
using System.Linq;
using Shared.Common.Models.TuringTree;
using Shared.Common.TuringTree;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Shared.BL;

namespace iOS.Phone
{
	public class BaseSurveyTableController : UIViewController
	{
		public GenericSurveyTableController ChildViewController;

		private SurveyViewModel _viewModel;

		public BaseSurveyTableController (IntPtr handle) : base(handle) {}

		public BaseSurveyTableController (SurveyViewModel viewModel) : base() 
		{
			_viewModel = viewModel;

			_viewModel.RequestDoctorPageAction = OnRequestDoctorPage; 
			_viewModel.RequestProcedurePageAction = OnRequestProcedurePage;
			_viewModel.RequestFacilityPageAction = OnRequestFacilityPage;
			_viewModel.RequestHealthProPageAction = OnRequestHealthProPage;
			_viewModel.RequestHomePageAction = OnRequestHomePage;
			_viewModel.RequestInterstitialPageAction = OnRequestInterstialPage;
			_viewModel.RequestSpecialtyPageAction = OnRequestSpecialtyPage;
			_viewModel.RequestSurveyPageAction = OnRequestSurveyPage;
			_viewModel.RequestResultsPageAction = OnRequestResultsPage;
			_viewModel.RequestPreferencesPageAction = OnRequestPreferencesPage;

			_viewModel.CanExecute = OnCanExecute;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ChildViewController = GenericSurveyTableController.Create (_viewModel, this);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NavigationController.SetNavigationBarHidden (false, true);
		}

		public BaseSurveyTableSource InitDataSource ()
		{
			return new BaseSurveyTableSource (_viewModel, ChildViewController);
		}

		private void OnCanExecute (bool canExecute, GenericAction action)
		{
			var nodeSection = _viewModel.Node.NodeSections.FirstOrDefault (s => s.SectionActions.Contains (action));
			int section = _viewModel.Node.NodeSections.IndexOf (nodeSection);
			int row = nodeSection.SectionActions.IndexOf (action);
			var indexPath = NSIndexPath.FromRowSection (row, section);
			var cell = ChildViewController.TableView.CellAt (indexPath) as BaseSurveyCell;

			if(cell != null)
			{
				cell.OnCanExecute (canExecute, action);	
			}
		}

		protected async void OnRequestHomePage (SurveyViewModel destViewModel)
		{
			// WARNING: This is an inefficient implementation and is hackish
			// This is necessary because we need to swap out the entire navigation stack with the home screen
			// Just swapping out doesn't give us the correct navigation bar though (menu button, settings button, title, etc.)
			// AS such, after the animation is completed, we need to swap in another instance of the same controller and place it as the Center Panel
			// a 400 delay was chosen because that is about when the transition animation is completed

			// Animate in dummy controller
			var container = UIApplication.SharedApplication.KeyWindow.RootViewController.FindViewControllerClass (typeof(HomeContainerController)) as HomeContainerController;
			var controller = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeController");

			NavigationController.SetViewControllers(new UIViewController[] {controller}, true);
			await Task.Delay (400);

			// Swap with another controller so we get the hamburger nenu icon
			var temp = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeController");
			var navController = new UINavigationController (temp);

			navController.ConfigureToCompassDefaults (false);
			container.CenterPanel = navController;
		}

		protected void OnRequestPreviousPage (SurveyViewModel destViewModel)
		{
			NavigationController.PopViewController (true);
		}

		protected void OnRequestSpecialtyPage (SurveyViewModel destViewModel)
		{
			var controller = new SpecialtyLookupController();
			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults ();

			controller.OnSelect = (specialty) => {
				NavigationController.DismissViewController(true, null);
				_viewModel.SpecialtySelectedCommand.Execute(specialty);
			};
			NavigationController.PresentViewController (navController, true, null);
		}

		protected void OnRequestProcedurePage (SurveyViewModel destViewModel)
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);
			var controller = storyboard.InstantiateViewController ("SurveyProcedurePromptController") as SurveyProcedurePromptController;

			controller.ViewModel.DestinationViewModel = destViewModel;

			NavigationController.PushViewController (controller, true);
		}

		protected void OnRequestPreferencesPage (SurveyViewModel destViewModel)
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);
			var controller = storyboard.InstantiateViewController ("DoctorPreferencesController") as DoctorPreferencesController;

			controller.FlowData = destViewModel.FlowData;
			controller.TreeMap = destViewModel.Map;

			NavigationController.PushViewController (controller, true);
		}

		protected void OnRequestFacilityPage (SurveyViewModel destViewModel)
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);
			var controller = storyboard.InstantiateViewController ("SurveyFacilityPromptController") as SurveyFacilityPromptController;

			controller.ViewModel.DestinationViewModel = destViewModel;
			NavigationController.PushViewController (controller, true);
		}

		protected void OnRequestDoctorPage (SurveyViewModel destViewModel)
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);
			var controller = storyboard.InstantiateViewController ("SurveyDoctorPromptController") as SurveyDoctorPromptController;

			controller.ViewModel.DestinationViewModel = destViewModel;
			NavigationController.PushViewController (controller, true);
		}

		protected void OnRequestInterstialPage (SurveyViewModel destViewModel)
		{

		}

		protected void OnRequestHealthProPage (SurveyViewModel destViewModel)
		{
			HealthProQuestionResponseController controller = UIStoryboard.FromName ("SettingsStoryboard", null).InstantiateViewController ("HealthProQuestionResponseController") as HealthProQuestionResponseController;
			controller.SelectedQuestion = new HealthProQuestionViewModel (HealthProQuestionKeys.Recommendation);
			controller.NonUserVisableComments = TuringTreeHelpers.FlowDataWalkWithMapToHumanReadableText (destViewModel.Map,destViewModel.FlowData);

			//Since we do not have health pro from previous page, ready it up.
			controller.ViewModel.ReadyHealthProFromBL();
			controller.ViewModel.RequestPostSendPage =  () => {
				var returnController = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");
				NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { returnController }, true);
			};
			controller.ViewModel.RequestCancelPage = () => 	{
				var returnController = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");
				NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { returnController }, true);
			};

			NavigationController.PushViewController(controller,true);
		}

		protected void OnRequestSurveyPage (SurveyViewModel destViewModel)
		{
			var controller = new BaseSurveyTableController (destViewModel);

			NavigationController.PushViewController (controller, true);
		}

		protected void OnRequestResultsPage (SurveyViewModel destViewModel)
		{

		}
	}

	public class BaseSurveyTableSource : UITableViewSource
	{
		private SurveyViewModel _viewModel;
		private GenericSurveyTableController _childController;

		public BaseSurveyTableSource (SurveyViewModel viewModel, GenericSurveyTableController childController) 
		{ 
			_viewModel = viewModel;
			_childController = childController;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			nint count = _viewModel.Node.NodeSections.Count();

			return count;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			var nodeSection = _viewModel.Node.NodeSections.ElementAt ((int)section);
			bool requiresHorizontalLayout = nodeSection.LayoutHorizontally ();
			nint count = requiresHorizontalLayout ? 1 : nodeSection.SectionActions.Count();

			return count;
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			var nodeSection = _viewModel.Node.NodeSections.ElementAt ((int)section);
			GenericSectionHeader view = null;

			if(!nodeSection.LayoutHorizontally())
			{
				view = tableView.DequeueReusableCell (GenericSectionHeader.Key) as GenericSectionHeader;	
				view.Title = nodeSection.SectionTitle;
			}
			// Call this here to ensure that any buttons (i.e. actions) with dependencies have the correct enabled state
			_viewModel.CanExecuteCommand.Execute (null);

			return view;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			var nodeSection = _viewModel.Node.NodeSections.ElementAt ((int)section);
			var height = String.IsNullOrWhiteSpace(nodeSection.SectionTitle) ? 0 : 25;

			if(section != 0 && height > 0)
			{
				var prevSection = _viewModel.Node.NodeSections.ElementAt ((int)section - 1);
				var temp = prevSection.LayoutHorizontally ();

				height = temp ? height : height + 25;
			}

			return height;
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			var nodeSection = _viewModel.Node.NodeSections.ElementAt ((int)section);
			var height = nodeSection.LayoutHorizontally () ? 0 : 1;

			return height;
		}

		public override UIView GetViewForFooter (UITableView tableView, nint section)
		{
			var nodeSection = _viewModel.Node.NodeSections.ElementAt ((int)section);
			UIView view = null;

			if(!nodeSection.LayoutHorizontally())
			{
				view = new UIView (new CoreGraphics.CGRect (0, 0, tableView.Frame.Width, 1));
				view.BackgroundColor = SharedColors.Tan2.ToUIColor ();
			}

			return view;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			nfloat height = UITableView.AutomaticDimension;

			if(Device.OS < 8)
			{
				//				 Note: If performance becomes an issue, see here (http://stackoverflow.com/questions/18746929/using-auto-layout-in-uitableview-for-dynamic-cell-layouts-variable-row-heights)
				//				 Option 1: Dynamically create different cell identifiers based off the configuration (2 buttons, no header, etc.)
				//				 Option 2: Store the heights for each cell in a dictionary and reference those in later dequeues

				var section = _viewModel.Node.NodeSections.ElementAt (indexPath.Section);
				var identifier = BaseSurveyCell.GetIdentifier (section);
				var cell = tableView.DequeueReusableCell (identifier) as BaseSurveyCell;
				height = cell.CalculateHeight (section, indexPath);
//				cell = null;
			}
			return height;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var section = _viewModel.Node.NodeSections.ElementAt (indexPath.Section);
			var action = section.SectionActions.ElementAt (indexPath.Row);
			var identifier = BaseSurveyCell.GetIdentifier (action, section);
			var cell = tableView.DequeueReusableCell (identifier) as BaseSurveyCell;

			cell.Configure (section, indexPath);
			cell.TouchUpAction = OnTouchUpInside;

			return cell;	
		}

		public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt(indexPath) as BaseSurveyCell;

			cell.OnDeselected (tableView, indexPath);	
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt(indexPath) as BaseSurveyCell;

			cell.OnSelected (tableView, indexPath);
		}

		public void OnTouchUpInside (object sender, GenericAction action)
		{
			_viewModel.ActionCommand.Execute (action);
			action.Selected = !action.Selected;
		}
	}
}