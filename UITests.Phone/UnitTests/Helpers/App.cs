using Shared.Bootstrapper;
using Shared.Common;
using Microsoft.Practices.Unity;
using UnitTests;

namespace Tests
{
    public static class App
    {
        public static void Initialize()
        {
            RegisterIocTypes();
            AutoMapperBootstrapper.MapTypes();
        }

        private static void RegisterIocTypes()
        {
            IocBootstrapper.RegisterTypes(IocContainer.GetContainer());

            IocContainer.GetContainer().RegisterInstance<ILogger> (new Logger ());
            IocContainer.GetContainer().RegisterInstance<ISecureDatabase>(new MockDatabase());
            IocContainer.GetContainer().RegisterType<IBrowserService, MockBrowserService> ();
            IocContainer.GetContainer().RegisterInstance<IExtendedNavigationService>(new MockNavigationService());
            IocContainer.GetContainer().RegisterType<IHttpClientHelper, MockHttpClientHelper>();
            IocContainer.GetContainer().RegisterType<IExtendedDialogService, MockExtendedDialogService>();
            IocContainer.GetContainer().RegisterInstance<IHudService> (new MockHudService());
            IocContainer.GetContainer().RegisterInstance<IConnectivityService>(new MockConnectivityService());
            IocContainer.GetContainer().RegisterInstance<IDispatcherService>( new MockDispatcherService());
            IocContainer.GetContainer().RegisterInstance<ITwitterHelper> (new MockTwitterHelper ());
            IocContainer.GetContainer().RegisterInstance<IFacebookHelper> (new MockFacebookHelper ());
            IocContainer.GetContainer().RegisterInstance<IPhoneService> (new MockPhoneService());
            IocContainer.GetContainer().RegisterInstance<IMapService> (new MockMapService());
            IocContainer.GetContainer().RegisterInstance<IGeolocator>(new MockGeolocatorService());
            IocContainer.GetContainer().RegisterInstance<IEmailService>(new MockEmailService());
        }
    }
}

