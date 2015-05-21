using System;

namespace iOS
{
	public class PSPinControlEventArgs : EventArgs
	{
		public string Value { get; private set; }

		public PSPinControlEventArgs (string value)
		{
			Value = value;
		}
	}
}

