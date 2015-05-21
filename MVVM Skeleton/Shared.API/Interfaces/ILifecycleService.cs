using System;
using Shared.Common;
using Shared.Common.Utils;

namespace Shared.BL
{
	public interface ILifecycleService
	{
		void OnStart();

		void OnSleep ();

		void OnResume();
	}
}

