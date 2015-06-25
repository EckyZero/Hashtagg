using System;
using UIKit;
using Shared.VM;

namespace iOS
{
	public abstract class BaseCell : UITableViewCell
	{
		#region Methods

		protected BaseCell () : base () { }

		public void Configure (IListItem viewModel)
		{
			ConfigureSubviews (viewModel);

			ContentView.LayoutIfNeeded ();
		}

		protected abstract void ConfigureSubviews (IListItem viewModel);

		public static string GetIdentifier(IListItem viewModel)
		{
			var identifer = "";

			switch (viewModel.ListItemType)
			{
			case ListItemType.Image:
				break;
			case ListItemType.NoImage:
				break;
			case ListItemType.Header:
				break;
			}

			return identifer;
		}

		#endregion
	}
}

