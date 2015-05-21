using System;

namespace Shared.Common
{
	public class OSServiceException : Exception
	{
		public OSServiceException()
		{
		}

		public OSServiceException(string message)
			: base(message)
		{
		}

		public OSServiceException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}

