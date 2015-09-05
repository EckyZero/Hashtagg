using System;
using Shared.Common;

namespace UnitTests
{
    public class MockBrowserService : IBrowserService
    {
        #region IBrowserService implementation

        public System.Threading.Tasks.Task OpenUrl(string url)
        {
            return null;
        }

        #endregion
    }
}

