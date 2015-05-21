using System;
using UIKit;
using Foundation;
using Shared.Common;
using Shared.VM;

namespace iOS.Phone
{
	public abstract class BaseTableController : UIViewController
	{
		protected GenericTableController ChildController;

		protected virtual BaseListViewModel ViewModel { get; set; }

		public virtual void OnRequestNextPage (object sender, object model) {}
		public abstract BaseTableSource InitDataSource (BaseListViewModel viewModel, GenericTableController childController);

		protected BaseTableController (IntPtr handle) : base(handle) {}
		protected BaseTableController () : base() {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitViewModel ();

			ChildController = GenericTableController.Create (ViewModel, this);
		}

		protected abstract void InitViewModel ();
	}

	public abstract class BaseTableSource : UITableViewSource
	{
		protected BaseTableSource (BaseListViewModel viewModel, GenericTableController childController) { }
		protected BaseTableSource () { }

		protected virtual void OnRequestCellSelectPage (BaseListItemViewModel viewModel) { }
		protected virtual void OnRequestCellActionPage (BaseListItemViewModel viewModel) { }
		protected virtual void OnRequestCellEditPage (BaseListItemViewModel viewModel) { }
	}
}

