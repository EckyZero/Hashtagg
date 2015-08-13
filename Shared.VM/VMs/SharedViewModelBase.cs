using System;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Globalization;
using System.Threading.Tasks;
using Shared.Common;
using Shared.Service;
using System.ComponentModel;

namespace Shared.VM
{
	public abstract class SharedViewModelBase : ViewModelBase
    {
        protected ILogger _logger;
        
        protected IConnectivityService _connectivityService;

		protected IDispatcherService _dispatchService;

		protected IExtendedNavigationService _navigationService;

		protected IExtendedDialogService _dialogService;

		protected IHudService _hudService;

		protected IBrowserService _browserService;

		protected IPhoneService _phoneService;

		protected IMapService _mapService;

		protected IEmailService _emailService;

		public SharedViewModelBase ()
        {
			InitServices ();

			InitCommands();
		}

		protected abstract void InitCommands();

		public virtual async Task DidLoad () {}

		public virtual async Task DidAppear () {}

		public virtual async Task WillDisappear () {}

		private void InitServices()
		{
			_navigationService = IocContainer.GetContainer().Resolve<IExtendedNavigationService>();

			_dialogService = IocContainer.GetContainer().Resolve<IExtendedDialogService> ();

			_hudService = IocContainer.GetContainer().Resolve<IHudService> ();

			_browserService = IocContainer.GetContainer().Resolve<IBrowserService> ();

			_dispatchService = IocContainer.GetContainer ().Resolve<IDispatcherService> ();

            _logger = IocContainer.GetContainer().Resolve<ILogger>();
			
			_phoneService = IocContainer.GetContainer().Resolve<IPhoneService> ();

			_mapService = IocContainer.GetContainer ().Resolve<IMapService> ();

		    _connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();

			_emailService = IocContainer.GetContainer().Resolve<IEmailService> ();
		}

		protected async Task OnError()
		{
			await _dialogService.ShowMessage(ApplicationResources.GenericError, ApplicationResources.Error);
		}

		protected async Task OnNoConnection()
		{
			await _dialogService.ShowMessage(ApplicationResources.GenericOffline, ApplicationResources.CurrentlyOffline);
		}

		protected async Task<bool> ProcessResponse<T>(ServiceResponse<T> response, bool showDialog = true)
		{
			if (response.ResponseType == ServiceResponseType.ERROR)
			{
				if (showDialog)
				{
					await OnError();
				}
				return false;
			}
			else if (response.ResponseType == ServiceResponseType.NO_CONNECTION)
			{
				if (showDialog)
				{
					await OnNoConnection();
				}
				return false;
			}
			else if (response.ResponseType == ServiceResponseType.SUCCESS)
			{
				return true;
			}
			return false;
		}

		protected async Task<bool> ProcessResponse(ServiceResponseType response, bool showDialog = true)
		{
			if (response == ServiceResponseType.ERROR)
			{
				if (showDialog)
				{
					await OnError();
				}
				return false;
			}
			else if (response == ServiceResponseType.NO_CONNECTION)
			{
				if (showDialog)
				{
					await OnNoConnection();
				}
				return false;
			}
			else if (response == ServiceResponseType.SUCCESS)
			{
				return true;
			}
			return false;
		}
    }
}

