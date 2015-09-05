using System;
using Shared.Common;

namespace UnitTests
{
    public class MockConnectivityService : IConnectivityService
    {
        #region IConnectivityService implementation

        public bool IsConnected
        {
            get { return true; }
        }

        #endregion
    }
}

