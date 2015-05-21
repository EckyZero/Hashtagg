using System;

namespace iOS
{
	public class PSFloatingPickerControlEventArgs : EventArgs
	{
		public int Index { get; private set;}

		public PSFloatingPickerControlEventArgs (int index)
		{
			Index = index;
		}
	}
}

