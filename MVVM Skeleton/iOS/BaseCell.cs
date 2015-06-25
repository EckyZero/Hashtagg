using System;
using UIKit;
using Shared.VM;

namespace iOS
{
	public abstract class BaseCell : UITableViewCell
	{
		#region Methods

		protected BaseCell () : base () { }

		public void Configure (IListItem item)
		{
			ConfigureSubviews (item);

			ContentView.LayoutIfNeeded ();
		}

		protected abstract void ConfigureSubviews (IListItem item);

		#endregion
	}
}

