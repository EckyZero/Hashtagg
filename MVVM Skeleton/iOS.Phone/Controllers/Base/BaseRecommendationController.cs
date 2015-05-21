
using System;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using MapKit;
using System.Collections.Generic;
using CoreLocation;
using System.Linq;

namespace iOS.Phone
{
	public abstract class BaseRecommendationController : UIViewController
	{
		#region Private Variables

		protected BaseRecommendationListViewModel _viewModel;

		#endregion

		#region Member Properties

		public GenericRecommendationController ChildController { get; set; }

		#endregion

		#region Constructors

		public BaseRecommendationController () : base () { }

		#endregion

		#region Methods

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_viewModel.RequestFullMap = OnRequestFullMap;

			ChildController = GenericRecommendationController.Create (_viewModel, this);
		}

		public abstract UITableViewSource InitDataSource ();
		public abstract void HydrateMap();
			
		public void RefreshData(IRecommendation docRec)
		{
			_viewModel.HydrateResults (docRec);
			ChildController.RefreshTableView ();
			HydrateMap ();
		}

		public abstract void OnRequestFullMap();

		#endregion
	}

	public abstract class BaseRecommendationsTableSource : UITableViewSource
	{
		private nint SectionMyDoctors = 0;
		private nint SectionRecommendations = 1;
		private nint SectionOther = 2;

		protected BaseRecommendationListViewModel _viewModel;
		protected GenericRecommendationController _childController;

		public BaseRecommendationsTableSource(BaseRecommendationListViewModel viewModel, GenericRecommendationController childController)
		{
			_viewModel = viewModel;
			_childController = childController;

			if(_viewModel.MyList.Count == 0)
			{
				_childController.TableViewHeaderText = String.Empty;
			}
			_viewModel.RequestDetailPage = OnRequestDetailPage;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			// 1: "My Doctors"
			// 2: "Recommended Doctors"
			// 3: "Other Doctors"
			// TODO: What if there are no "My doctors"
			var count = _viewModel.OtherList.Count == 0 ? 2 : 3;

			return count;
		}

		public override nfloat GetHeightForFooter (UITableView tableView, nint section)
		{
			nfloat height = 25;
			System.Diagnostics.Debug.WriteLine (section);

			if (section == SectionMyDoctors)
			{
				height = _viewModel.MyList.Count == 1.0f ? 1.0f : 25;
			}
			return height;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			nfloat height = 1.0f;

			if(section != SectionMyDoctors)
			{
				var view = tableView.DequeueReusableCell (DrRecHeaderCell.Key) as DrRecHeaderCell;
				height = view.CalculateHeight ();	
			}
			return height;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);
			var identifier = BaseRecommendationCell.GetIdentifier (viewModel);
			var cell = tableView.DequeueReusableCell (identifier) as BaseRecommendationCell;
			var height = cell.CalculateHeight (viewModel);

			return height;
		}

		public override nfloat EstimatedHeight (UITableView tableView, NSIndexPath indexPath)
		{
			var height = 179;
//			var viewModel = GetViewModel(indexPath);
//			var identifier = BaseRecommendationCell.GetIdentifier (viewModel);
//			var cell = tableView.DequeueReusableCell (identifier) as BaseRecommendationCell;
//			var height = cell.CalculateHeight (viewModel);

			return height;
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			var view = tableView.DequeueReusableCell (DrRecHeaderCell.Key) as DrRecHeaderCell;

			view.Configure (section, _viewModel);

			return view;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			var count = 0;

			if(section == SectionMyDoctors)
			{
				count = _viewModel.MyList.Count;	
			}
			else if (section == SectionRecommendations)
			{
				count = _viewModel.RecList.Count;
			} 
			else if (section == SectionOther)
			{
				count = _viewModel.OtherList.Count;
			}
			return count;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);
			var identifier = BaseRecommendationCell.GetIdentifier (viewModel);
			var cell = tableView.DequeueReusableCell (identifier) as BaseRecommendationCell;

			viewModel.RequestActionPage = OnRequestActionPage;

			cell.Configure (viewModel, (object sender, EventArgs args) => {
				viewModel.ActionCommand.Execute(null);
			});

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var viewModel = GetViewModel(indexPath);
			_viewModel.DetailCommand.Execute (viewModel);
		}

		public override void Scrolled (UIScrollView scrollView)
		{
			_childController.ParentViewController.NavigationController.HideBarsWhileScrolling (scrollView);
		}

		public abstract void OnRequestActionPage (BaseRecommendationListItemViewModel baseViewModel);

		public abstract void OnRequestDetailPage (BaseRecommendationDetailListViewModel viewModel, PatientPreferences patientPreferences);

		private BaseRecommendationListItemViewModel GetViewModel (NSIndexPath indexPath)
		{
			// Get the correct viewModel
			BaseRecommendationListItemViewModel viewModel = null;

			if(indexPath.Section == SectionMyDoctors)
			{
				viewModel = _viewModel.MyList[indexPath.Row] as BaseRecommendationListItemViewModel;	
			}
			else if (indexPath.Section == SectionRecommendations)
			{
				viewModel = _viewModel.RecList[indexPath.Row] as BaseRecommendationListItemViewModel;	
			} 
			else if (indexPath.Section == SectionOther)
			{
				viewModel = _viewModel.OtherList[indexPath.Row] as BaseRecommendationListItemViewModel;	
			}
			return viewModel;
		}
	}
}

