
using System;

using Foundation;
using UIKit;
using iOS;
using Shared.VM;
using Shared.Common;
using System.Threading.Tasks;

namespace iOS.Phone
{
	[Register ("HomeTableController")]
	public class HomeTableController : ExtendedObservableTableViewController<IListItem>
	{
		#region Private Variables

		private PSObservableTableSource<IListItem> _source;

		#endregion

		#region Member Properties

//		public TableView
		public ObservableRangeCollection<IListItem> Collection { get; set; }
		public Action<UIRefreshControl> OnPullToRefresh { get; set; }

		#endregion

		#region Methods

		public HomeTableController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			RefreshControl = new UIRefreshControl();
			RefreshControl.ValueChanged += HandleValueChanged;
		}

		public override void ConfigureDelegates ()
		{
			_source = new PSObservableTableSource<IListItem> (this, Collection);
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

