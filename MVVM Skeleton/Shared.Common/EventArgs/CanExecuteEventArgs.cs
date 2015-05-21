using System;

namespace Shared.Common
{
    public delegate void CanExecuteEventHandler(object sender, CanExecuteEventArgs e);

	public class CanExecuteEventArgs : EventArgs
	{
		public bool CanExecute { get; set; }

		public CanExecuteEventArgs (bool canExecute)
		{
			CanExecute = canExecute;
		}
	}
}

