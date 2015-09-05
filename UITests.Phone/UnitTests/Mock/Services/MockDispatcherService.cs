using System;
using Shared.Common;

namespace UnitTests
{
    public class MockDispatcherService : IDispatcherService
    {
        #region IDispatcherService implementation

        public void InvokeOnMainThread(Action action)
        {
            action();
        }

        #endregion
       
    }
}

