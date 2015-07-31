using System;

namespace Shared.VM
{
	public enum ListItemType
	{
		Header,
		Default,
		MenuItem
	}
	public interface IListItem
	{
		Action<IListItem> OnSelected { get; set; }
		ListItemType ListItemType { get; set; }

		void Selected ();
	}
}

