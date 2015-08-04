using System;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Shared.Service
{
	public abstract class BaseService
	{
		#region Private Variables

		protected ILogger _logger;
		protected IConnectivityService _connectivityService;

		#endregion

		public BaseService ()
		{
			_logger = IocContainer.GetContainer().Resolve<ILogger>();
			_connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();
		}
	}
}

