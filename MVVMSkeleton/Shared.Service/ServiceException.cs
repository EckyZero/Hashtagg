using System;
using Shared.Common;

namespace Shared.Service
{
	public class ServiceException : BaseException
	{
		public ServiceException() { }

		public ServiceException(string message) : base(message) { }

		public ServiceException(string message, Exception inner) : base(message, inner) { }
	}
}

