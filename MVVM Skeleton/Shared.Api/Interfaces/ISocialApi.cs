using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Api
{
	public interface ISocialService
	{
		Task<string> TwitterRequestExecute (string method, Uri uri, IDictionary<string,string> parameters);
	}
}

