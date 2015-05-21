using System;

namespace iOS
{
	public class PSFloatingDateTimeControlEventArgs : EventArgs
	{
		public DateTime DateTime { get; private set;}

		public PSFloatingDateTimeControlEventArgs (DateTime dateTime)
		{
			DateTime = dateTime;
		}
	}
}

