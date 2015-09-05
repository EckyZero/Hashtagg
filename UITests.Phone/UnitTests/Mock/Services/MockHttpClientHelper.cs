using System;
using Shared.Common;
using System.Net.Http;

namespace UnitTests
{
    public class MockHttpClientHelper : IHttpClientHelper
    {
        #region IHttpClientHelper implementation

        public HttpMessageHandler MessageHandler
        {
            get { return null; }
        }
        #endregion
       
    }
}

