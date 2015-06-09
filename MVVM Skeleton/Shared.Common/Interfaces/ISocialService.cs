using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Common
{
	public interface ISocialService
	{
		Task<string> TwitterRequestExecute (string method, Uri uri, IDictionary<string,string> parameters);
		void TwitterAuthenticationExecute (Action callback);
		Task<bool> TwitterAccountExists ();
	}
}

