using System;
using Shared.VM;
using Shared.Common;
using UIKit;
using Foundation;
using Shared.BL;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace iOS.Phone
{
	public class HealthDetailsConditionLookupController : BaseLookupController<Condition>
	{
		GenericLookupController _childController; 

		public Action<Condition> OnSelect
		{ 
			get { return ViewModel.OnSelect; } 
			set { ViewModel.OnSelect = value; } 
		}
		
		public HealthDetailsConditionLookupController () : base() {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_childController = GenericLookupController.Create <Condition>(ViewModel, this);
		}

		public override void InitDelegates () 
		{
			_childController.TableController.DataSource = ((HealthDetailsConditionLookupViewModel)ViewModel).GenericResults; //Convert.ChangeType(ViewModel.Results, typeof(ObservableCollection<IIdentifiable>)) ;

			var delegates = new GenericLookupDelegates<IIdentifiable> ();

			delegates.BindCellDelegate = (cell, condition, indexPath) => {
				BindCell (cell, (Condition)condition, indexPath);
			};

			delegates.CreateCellDelegate = (id, tableView, indexPath) => {
				return CreateCell (id, tableView, indexPath);
			};	
				
			_childController.TableController.Delegates = delegates;
		}

		public override void InitViewModel ()
		{
			var bl = IocContainer.GetContainer().Resolve<IConditionBL>();
			var gl = IocContainer.GetContainer().Resolve<IGeolocator>();

			ViewModel = new HealthDetailsConditionLookupViewModel (bl,gl);	
		}

		private void BindCell (UITableViewCell cell, Condition condition, NSIndexPath indexPath)
		{
			cell.TextLabel.Text = condition.Description;
		}

		private UITableViewCell CreateCell (string id, UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("GenericBasicCell");

			return cell;
		}
	}

}

