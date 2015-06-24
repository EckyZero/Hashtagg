
using System;

using Foundation;
using UIKit;
using iOS;
using Shared.VM;

namespace iOS.Phone
{
	public class HomeTableController : ExtendedObservableTableViewController<BaseCardViewModel>
	{
		#region Methods

		public HomeTableController () : base ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register the TableView's data source
			TableView.Source = new HomeTableControllerSource ();
		}

		public override void ConfigureDelegates ()
		{
			
		}

		#endregion
	}
}

