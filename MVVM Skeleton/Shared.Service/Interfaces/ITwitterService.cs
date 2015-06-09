using System;
using Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Service
{
	public interface ITwitterService
	{
		Task<IList<TwitterFeedItem>> GetHomeFeed ();
	}
}

