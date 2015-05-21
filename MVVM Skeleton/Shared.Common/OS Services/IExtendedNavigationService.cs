using GalaSoft.MvvmLight.Views;

namespace Shared.Common
{
	public interface IExtendedNavigationService : INavigationService
	{
		void Present(string pageKey, object parameter = null);
		void DismissPage();
	}
}

