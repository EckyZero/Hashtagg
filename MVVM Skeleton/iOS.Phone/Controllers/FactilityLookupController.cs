using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.BL;
using Shared.VM;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;

namespace iOS.Phone
{
	public class FacilityLookupListController : BaseLookupController<Facility>
	{
		GenericLookupController _childController;

		public FacilityLookupListController (FacilityLookupViewModel viewModel) : base () 
		{
			ViewModel = viewModel as ILookupViewModel<Facility>;	
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_childController = GenericLookupController.Create <Facility>(ViewModel, this);
		}

		public override void InitDelegates () 
		{
			_childController.TableController.DataSource = ((FacilityLookupViewModel)ViewModel).GenericResults;

			var delegates = new GenericLookupDelegates<IIdentifiable> ();

			delegates.BindCellDelegate = (cell, facility, indexPath) => {
				BindCell (cell, (Facility)facility, indexPath);
			};

			delegates.GetHeightForRowDelegate = ((UITableView tableView, NSIndexPath indexPath) => {
				return 76;
			});

			delegates.CreateCellDelegate = (id, tableView, indexPath) => {
				if(tableView.IsScrolledToBottom())
				{
					StartAnimating ();

					ViewModel.ShowMoreCommand.Execute (this.SearchBar.Text);
				}
				return CreateCell (id, tableView, indexPath);
			};	

			_childController.TableController.Delegates = delegates;
		}

		public void StartAnimating ()
		{
			(_childController.ChildViewControllers[0] as GenericLookupTableController).StartAnimating ();
		}

		public void StopAnimating ()
		{
			(_childController.ChildViewControllers[0] as GenericLookupTableController).StopAnimating ();
		}

		public override void InitViewModel (){}

		private void BindCell (UITableViewCell cell, Facility facility, NSIndexPath indexPath)
		{
			//TODO if possible we should make better decision on address for facility than the first available
			var facilityAddress = facility.RecommendedAddress;
			var facilityCell = cell as DoctorLookupCell;
			facilityCell.Name = facility.Name;
			facilityCell.Address = facilityAddress != null ? facilityAddress.ToString() : string.Empty;
			facilityCell.Specialty = facility.SpecialtiesDescription;

		}

		private UITableViewCell CreateCell (string id, UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (DoctorLookupCell.Key);

			return cell;
		}
	}
}

