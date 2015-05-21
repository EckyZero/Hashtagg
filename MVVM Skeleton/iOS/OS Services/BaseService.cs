using Shared.Common;
using Shared.Common.Logging;
using Microsoft.Practices.Unity;

namespace iOS
{
    public abstract class BaseService
    {
        protected ILogger _logger;

        public BaseService()
        {
            _logger = IocContainer.GetContainer().Resolve<ILogger>();
        }
    }
}