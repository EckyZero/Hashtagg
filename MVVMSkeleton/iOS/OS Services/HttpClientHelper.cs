using System;
using System.Net.Http;
using ModernHttpClient;
using Shared.Common;

namespace iOS
{
    public class HttpClientHelper : BaseService, IHttpClientHelper
	{
		private HttpMessageHandler _handler;

		public HttpMessageHandler MessageHandler
		{
			get {return _handler ?? (_handler = new NativeMessageHandler ());}
		}
	}
}

