using Demo.Shared.Tests.Mocks;
using Microsoft.Practices.Unity;

namespace Demo.Shared.Tests
{
    public static class App
    {
        public static void Initialize()
        {
            RegisterIocTypes();
        }

        private static void RegisterIocTypes()
        {
            Demo.Shared.Helpers.App.IocContainer.RegisterInstance(SharedSalMock.GetMock());
            Demo.Shared.Helpers.App.IocContainer.RegisterType<ISecureDatabase, TestDatabase>();
        }
    }
}