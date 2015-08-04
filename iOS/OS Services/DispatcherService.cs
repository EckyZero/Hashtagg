using System;
using Foundation;
using Shared.Common;

namespace iOS
{
    public class DispatcherService : BaseService, IDispatcherService
	{
		private NSObject _invoker;

		public DispatcherService (NSObject invoker)
		{
			_invoker = invoker;
		}

		public void InvokeOnMainThread(Action action)
		{
			if (NSThread.Current.IsMainThread) {
				action ();
			} else {
				_invoker.InvokeOnMainThread (() => action ()); 
			}
		}
	}
}

