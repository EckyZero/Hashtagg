using System.Net.Http;
using ModernHttpClient;
using Shared.Common;

namespace Droid
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private HttpMessageHandler _handler;

        public HttpMessageHandler MessageHandler
        {
            get { return _handler ?? (_handler = new NativeMessageHandler()); }
        }
    }
}