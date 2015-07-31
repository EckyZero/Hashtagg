using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Shared.Api;
using Shared.Service;

namespace Shared.Bootstrapper
{
	public static class IocBootstrapper
	{
		public static void RegisterTypes(IUnityContainer container)
		{
			ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(container));

			// APIs
			container.RegisterType<ITwitterApi, TwitterApi> ();
			container.RegisterType<IFacebookApi, FacebookApi> ();

			// Services
			container.RegisterType<ITwitterService, TwitterService> ();
			container.RegisterType<IFacebookService, FacebookService> ();
			container.RegisterInstance<ILifecycleService> (new LifecyleService ());
		}
	}
}

