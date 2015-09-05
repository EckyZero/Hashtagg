using System;
using Shared.Common;

namespace UnitTests
{
    public class MockEmailService : IEmailService
    {
        #region IEmailService implementation

        public bool Email(string emailTo = null)
        {
            return true;
        }

        #endregion
    }
}

