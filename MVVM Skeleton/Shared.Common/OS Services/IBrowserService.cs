using System;
using System.Threading.Tasks;

namespace Shared.Common
{
	public interface IBrowserService
	{
		Task OpenUrlinApp(string url, string title);

	    Task OpenUrlinOS(string url);
	}
}

