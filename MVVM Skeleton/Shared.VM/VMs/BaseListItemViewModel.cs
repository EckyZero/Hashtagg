using System;

namespace Shared.VM
{
	public enum ListItemType
	{
		Header,
		Image,
		NoImage
	}
	public interface IListItem
	{
		Action<IListItem> OnSelected { get; set; }
		ListItemType ListItemType { get; }

		void Selected ();
	}
}

