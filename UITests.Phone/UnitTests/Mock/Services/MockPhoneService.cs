using System;
using Shared.Common;

namespace UnitTests
{
    public class MockPhoneService : IPhoneService
    {
        #region IPhoneService implementation

        public bool CallNumber(string phoneNumber)
        {
            return true;
        }

        #endregion
    }
}

