using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.BL;
using Shared.VM;
using UIKit;
using Foundation;

namespace iOS.Phone
{
	public class SpecialtyLookupController : BaseLookupController<Specialty>
	{
		GenericLookupController _childController;

		public SpecialtyLookupController () : base () {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_childController = GenericLookupController.Create <Specialty>(ViewModel, this);
		}

		public override void InitDelegates () 
		{
			_childController.TableController.DataSource = ((SpecialtyLookupViewModel)ViewModel).GenericResults;

			var delegates = new GenericLookupDelegates<IIdentifiable> ();

			delegates.BindCellDelegate = (cell, specialty, indexPath) => {
				BindCell (cell, (Specialty)specialty, indexPath);
			};

			delegates.CreateCellDelegate = (id, tableView, indexPath) => {
				return CreateCell (id, tableView, indexPath);
			};	

			_childController.TableController.Delegates = delegates;
		}

		public override void InitViewModel ()
		{
			var bl = IocContainer.GetContainer().Resolve<ISpecialtyBL>();
			var gl = IocContainer.GetContainer().Resolve<IGeolocator>();

			ViewModel = new SpecialtyLookupViewModel (bl,gl);
		}

		private void BindCell (UITableViewCell cell, Specialty specialty, NSIndexPath indexPath)
		{
			cell.TextLabel.Text = specialty.Description;
		}

		private UITableViewCell CreateCell (string id, UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("GenericBasicCell");

			return cell;
		}
	}
}

