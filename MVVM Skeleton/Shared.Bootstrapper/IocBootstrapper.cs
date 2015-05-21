using System;
using Microsoft.Practices.ServiceLocation;
using Shared.Common.Logging;
using Shared.VM;
using UnityServiceLocator = Microsoft.Practices.Unity.UnityServiceLocator;

namespace Shared.Bootstrapper
{
	public static class IocBootstrapper
	{
		public static void RegisterTypes(IUnityContainer container)
		{
			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

            // Logging
		    container.RegisterType<ILogger, Logger>();
		}
	}
}
