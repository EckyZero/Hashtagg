using System;

namespace Shared.Service
{
	public interface ILifecycleService
	{
		Action RequestHomePage { get; set; }
		Action RequestOnboardingPage { get; set; }

		void OnStart();
		void OnResume();
		void OnPause();
		void OnTerminated();
	}
}

