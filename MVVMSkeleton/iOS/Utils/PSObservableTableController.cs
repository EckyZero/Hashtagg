
using System;

using Foundation;
using UIKit;
using iOS;
using Shared.VM;
using Shared.Common;
using System.Threading.Tasks;

namespace iOS
{
	public partial class PSObservableTableController : ExtendedObservableTableViewController<IListItem>, IUIScrollViewDelegate
	{
		#region Private Variables

		private PSObservableTableSource<IListItem> _source;

		#endregion

		#region Member Properties

		public ObservableRangeCollection<IListItem> Collection { get; set; }
		public Action<UIRefreshControl> OnPullToRefresh { get; set; }

		#endregion

		#region Methods

		public PSObservableTableController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RefreshControl = new UIRefreshControl();
			RefreshControl.ValueChanged += HandleValueChanged;
			RefreshControl.BeginRefreshing ();
			RefreshControl.EndRefreshing ();

			TableView.RowHeight = UITableView.AutomaticDimension;
		}

		public override void ConfigureDelegates ()
		{
			_source = new PSObservableTableSource<IListItem> (this, Collection);
		}

		public void SetEstimatedHeight (nfloat height) 
		{
			TableView.EstimatedRowHeight = 	height;
		}

		private void HandleValueChanged (object sender, EventArgs e)
		{
			if(OnPullToRefresh != null) {
				OnPullToRefresh(RefreshControl);
			}
		}

		#endregion
	}
}

