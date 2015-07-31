using Microsoft.Practices.Unity;

namespace Shared.Common
{
	public static class IocContainer
	{
		private static UnityContainer _container;

		public static UnityContainer GetContainer()
		{
			if (_container == null) {
				_container = new UnityContainer();
			}
			return _container;
		}
	}
}

