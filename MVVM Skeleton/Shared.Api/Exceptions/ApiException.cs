using System;
using System.Net;
using Shared.Common;

namespace Shared.Api
{
	public class ApiException : BaseException
	{
		public HttpStatusCode StatusCode { get; set; }

		public ApiException(HttpStatusCode statusCode = HttpStatusCode.OK)
		{
			StatusCode = statusCode;
		}

		public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.OK)
			: base(message)
		{
			StatusCode = statusCode;
		}

		public ApiException(string message, Exception inner, HttpStatusCode statusCode = HttpStatusCode.OK)
			: base(message, inner)
		{
			StatusCode = statusCode;
		}
	}
}

