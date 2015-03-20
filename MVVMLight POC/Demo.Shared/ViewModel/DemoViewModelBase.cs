using System;
using GalaSoft.MvvmLight;

namespace Demo.Shared
{
	public abstract class DemoViewModelBase : ViewModelBase
	{
		public abstract string Title {get;}

		public DemoViewModelBase ()
		{
			InitCommands ();
		}

		protected virtual void InitCommands(){}
	}
}

