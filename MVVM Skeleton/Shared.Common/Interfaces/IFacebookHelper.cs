using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Common
{
	public interface IFacebookHelper
	{
		Task<string> ExecuteRequest (string method, Uri uri, IDictionary<string,string> parameters = null);
		void Authenticate (Action callback);
		Task<bool> AccountExists ();
		void DeleteAccount ();
	}
}

