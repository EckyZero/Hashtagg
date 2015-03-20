using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Microsoft.Practices.Unity;
using Shared.Common;

namespace Shared.VM
{
	public abstract class SharedViewModelBase : ViewModelBase
	{
		protected IDispatcherService _dispatchService;

		protected IExtendedNavigationService _navigationService;

		protected IExtendedDialogService _dialogService;

		protected IHudService _hudService;

		protected IBrowserService _browserService;

		public SharedViewModelBase ()
		{
			InitServices ();

			InitCommands();
		}

		protected abstract void InitCommands();

		public virtual async Task DidLoad () { }

		public virtual async Task WillAppear() { }

		public virtual async Task WillDisappear () { }

		private void InitServices()
		{
			_navigationService = IocContainer.GetContainer().Resolve<IExtendedNavigationService>();

			_dialogService = IocContainer.GetContainer().Resolve<IExtendedDialogService> ();

			_hudService = IocContainer.GetContainer().Resolve<IHudService> ();

			_browserService = IocContainer.GetContainer().Resolve<IBrowserService> ();

			_dispatchService = IocContainer.GetContainer ().Resolve<IDispatcherService> ();
		}
	}
}

