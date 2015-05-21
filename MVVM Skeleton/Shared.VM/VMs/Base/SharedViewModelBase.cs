using System;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Globalization;
using Shared.Common;
using System.Threading.Tasks;
using Shared.BL;
using Shared.Common.Logging;

namespace Shared.VM
{
	public abstract class SharedViewModelBase : ViewModelBase
    {
        protected ILogger _logger;
        
        protected IConnectivityService _connectivityService;

		protected IDispatcherService _dispatchService;

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

		private void InitServices()
		{
			_hudService = IocContainer.GetContainer().Resolve<IHudService> ();

			_browserService = IocContainer.GetContainer().Resolve<IBrowserService> ();

			_dispatchService = IocContainer.GetContainer ().Resolve<IDispatcherService> ();

            _logger = IocContainer.GetContainer().Resolve<ILogger>();
			
			_phoneService = IocContainer.GetContainer().Resolve<IPhoneService> ();

			_mapService = IocContainer.GetContainer ().Resolve<IMapService> ();

		    _connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();

			_emailService = IocContainer.GetContainer().Resolve<IEmailService> ();
		}

    }
}

