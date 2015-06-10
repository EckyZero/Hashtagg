using System;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Shared.Service
{
	public abstract class BaseService
	{
		#region Private Variables

		protected ILogger Logger;
		protected IConnectivityService ConnectivityService;

		#endregion

		public BaseService ()
		{
			Logger = IocContainer.GetContainer().Resolve<ILogger>();
			ConnectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();
		}
	}
}

