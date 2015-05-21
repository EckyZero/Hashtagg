using System.Net.Http;

namespace Shared.Common
{
	public interface IHttpClientHelper
	{
		HttpMessageHandler MessageHandler {get;}
	}
}

