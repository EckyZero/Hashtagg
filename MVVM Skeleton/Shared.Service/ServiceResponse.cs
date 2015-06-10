using System;

namespace Shared.Service
{
	public enum ServiceResponseType
	{
		SUCCESS,
		NO_CONNECTION,
		ERROR
	}

	public class ServiceResponse<T>
	{
		public ServiceResponseType ResponseType { get; private set; }
		public T Result { get; private set; }

		public ServiceResponse(T result, ServiceResponseType responseType)
		{
			Result = result;
			ResponseType = responseType;
		}

		public ServiceResponse()
		{
			Result = default(T);
			ResponseType = ServiceResponseType.ERROR;
		}
	}
}

