using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.BL;
using Shared.VM;
using UIKit;
using Foundation;

namespace iOS.Phone
{
	public class DrPreferencesLocationLookupController : BaseLookupController<Location>
	{

		private static UIFont CenturyGothic14 = UIFont.FromName ("CenturyGothic", 14);

		GenericLookupController _childController;

		public DrPreferencesLocationLookupController () : base () {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_childController = GenericLookupController.Create <Location>(ViewModel, this, OnSelectionAction);
		}

		public new Action<LocationLookupCellViewModel> OnSelect
		{ 
			get { return ((LocationLookupViewModel)ViewModel).OnSelect; } 
			set { ((LocationLookupViewModel)ViewModel).OnSelect = value; } 
		}


		public void OnSelectionAction (object sender, EventArgs e)
		{
			var args = (ObservableTableViewControllerEventArgs<IIdentifiable>)e;
			var item = args.SelectedItem;

			((LocationLookupViewModel)ViewModel).SelectedResult = (LocationLookupCellViewModel)item;
			((LocationLookupViewModel)ViewModel).SelectionCommand.Execute(item);
		}

		public override void InitDelegates () 
		{
			((LocationLookupViewModel)ViewModel).ShowDefaultResults();
			_childController.TableController.DataSource = ((LocationLookupViewModel)ViewModel).LocationResults;


			var delegates = new GenericLookupDelegates<IIdentifiable> ();

			delegates.BindCellDelegate = (cell, location, indexPath) => {
				BindCell (cell, (LocationLookupCellViewModel)location, indexPath);
			};

			delegates.CreateCellDelegate = (id, tableView, indexPath) => {
				return CreateCell (id, tableView, indexPath);
			};	


			delegates.GetHeightForRowDelegate = ((UITableView tableView, NSIndexPath indexPath) => {
				var cellVm = (LocationLookupCellViewModel)((LocationLookupViewModel)ViewModel).LocationResults[indexPath.Row];

				if (cellVm.Type == LocationLookupCellType.CurrentLocationCell || string.IsNullOrWhiteSpace (cellVm.Subtitle)) {
					return 44f;
				} else {
					return 58f;
				}
			}); 

			_childController.TableController.Delegates = delegates;

			this.SearchBar.TextChanged += (object sender, UISearchBarTextChangedEventArgs e) => 
			{
				if(string.IsNullOrWhiteSpace(e.SearchText)){
					((LocationLookupViewModel)ViewModel).ShowDefaultResults();
				}
				else{
					((LocationLookupViewModel)ViewModel).ShowSearchResults();
				}
			};
		}

		public override void InitViewModel ()
		{
			var bl = IocContainer.GetContainer().Resolve<ILocationBL>();
			var gl = IocContainer.GetContainer().Resolve<IGeolocator>();

			ViewModel = new LocationLookupViewModel (bl,gl);
		}

		private void BindCell (UITableViewCell cell, LocationLookupCellViewModel cellVm, NSIndexPath indexPath)
		{
			if (cellVm.Type == LocationLookupCellType.CurrentLocationCell || string.IsNullOrWhiteSpace (cellVm.Subtitle)) {

				cell.TextLabel.Text = cellVm.Title;
				cell.TextLabel.TextColor = cellVm.TitleColor.ToUIColor ();

				if(cell.TextLabel.Font != CenturyGothic14){
					cell.TextLabel.Font = CenturyGothic14;
				}

			} else {

				var specificCell = (LocationLookupCell)cell;

				specificCell.Title = cellVm.Title;
				specificCell.Subtitle = cellVm.Subtitle;
			}
		}

		private UITableViewCell CreateCell (string id, UITableView tableView, NSIndexPath indexPath)
		{
			var cellVm =(LocationLookupCellViewModel) ((LocationLookupViewModel)ViewModel).LocationResults [indexPath.Row];

			UITableViewCell cell = null;

			if (cellVm.Type == LocationLookupCellType.CurrentLocationCell || string.IsNullOrWhiteSpace (cellVm.Subtitle)) {
				cell = tableView.DequeueReusableCell ("GenericBasicCell");
			} else {
				cell = tableView.DequeueReusableCell ("LocationLookupCell");
			}

			return cell;
		}
	}
}

