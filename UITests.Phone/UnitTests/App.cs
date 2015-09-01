using System;
using Shared.Bootstrapper;
using Shared.Common;
using Microsoft.Practices.Unity;

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
        }
    }
}

