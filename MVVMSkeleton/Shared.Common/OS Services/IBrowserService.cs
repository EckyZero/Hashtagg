using System;
using System.Threading.Tasks;

namespace Shared.Common
{
	public interface IBrowserService
	{
	    Task OpenUrl(string url);
	}
}

