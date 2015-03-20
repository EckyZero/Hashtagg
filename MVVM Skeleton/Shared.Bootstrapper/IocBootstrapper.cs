using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Shared.Bootstrapper
{
	public static class IocBootstrapper
	{
		public static void RegisterTypes(IUnityContainer container)
		{
			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

			// container.RegisterType<IHomeViewModel, HomeViewModel> ();
		}
	}
}

