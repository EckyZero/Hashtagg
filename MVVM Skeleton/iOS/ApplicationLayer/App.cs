using Microsoft.Practices.Unity;
using Shared.Common;

namespace iOS
{
	public static class App
	{
		public static void Initialize()
		{
			RegisterIocTypes();
		}

		private static void RegisterIocTypes()
		{
			IocContainer.GetContainer().RegisterType<ISecureDatabase, iOSSecureDatabase>();
		}
	}
}

