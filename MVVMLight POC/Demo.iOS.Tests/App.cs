using Demo.Shared;
using Demo.Shared.Helpers;
using Microsoft.Practices.Unity;
using Xamarin;

namespace Demo.iOS.Tests
{
    public static class App
    {
        public static void Initialize()
        {
            RegisterIocTypes();
            Insights.Initialize(Strings.Settings.XamarinInsightsApiKey);
        }

        private static void RegisterIocTypes()
        {
            //TODO replace with password from secure location, ex keychain services
            Shared.Helpers.App.IocContainer.RegisterType<IServiceAccessLayer, SharedSal>();
            Shared.Helpers.App.IocContainer.RegisterType<ISecureDatabase, IosSecureDatabase>(
                new InjectionConstructor("password"));
        }
    }
}