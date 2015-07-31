using System;

namespace Shared.Common
{
	public interface IDispatcherService
	{
		void InvokeOnMainThread(Action action);
	}
}

