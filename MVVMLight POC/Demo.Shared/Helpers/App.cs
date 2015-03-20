using Microsoft.Practices.Unity;

namespace Demo.Shared.Helpers
{
    public static class App
    {
        public static UnityContainer IocContainer;

        static App()
        {
            IocContainer = new UnityContainer();
            RegisterSharedIocTypes();
        }

        private static void RegisterSharedIocTypes()
        {
            IocContainer.RegisterType<IServiceAccessLayer, SharedSal>();
        }
    }
}