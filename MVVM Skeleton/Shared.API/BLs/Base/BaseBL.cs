using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Common;
using Shared.Common.Logging;
using Microsoft.Practices.Unity;

namespace Shared.BL.BLs.Base
{
    public abstract class BaseBL
    {
        protected ILogger _logger;

        protected IConnectivityService _connectivityService;

        public BaseBL()
        {
            _logger = IocContainer.GetContainer().Resolve<ILogger>();

            _connectivityService = IocContainer.GetContainer().Resolve<IConnectivityService>();
        }
    }
}
