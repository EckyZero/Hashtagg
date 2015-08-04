using System;

namespace Shared.Service
{
	public interface ILifecycleService
	{
		event EventHandler ApplicationWillStart;
		event EventHandler ApplicationWillPause;
		event EventHandler ApplicationWillResume;
		event EventHandler ApplicationWillTerminate;

		void OnStart();
		void OnResume();
		void OnPause();
		void OnTerminated();
	}
}

