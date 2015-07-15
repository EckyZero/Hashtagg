using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shared.Api
{
	public interface ITwitterApi
	{
		Task<List<TwitterFeedItemDto>> GetHomeFeed();
		Task<TwitterUserDto> GetUser (string screenName);
		Task Like (string tweetId);
		Task Unlike (string tweetId);
	}
}

