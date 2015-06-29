using System;

namespace Shared.VM
{
	public enum RowItemType
	{
		Header,
		Default,
		Timeline
	}
	public interface IRowItem
	{
		Action<IRowItem> OnSelected { get; set; }
		RowItemType ListItemType { get; set; }

		void Selected ();
	}
}

