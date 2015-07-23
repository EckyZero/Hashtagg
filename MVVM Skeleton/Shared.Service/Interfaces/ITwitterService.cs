using System;
using Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Shared.Service
{
	public interface ITwitterService
	{
		Task<ServiceResponse<ObservableCollection<Tweet>>> GetHomeFeed ();
		Task<ServiceResponse<TwitterUser>> GetUser (string screenName);
		Task<ServiceResponseType> Like (string tweetId);
		Task<ServiceResponseType> Unlike (string tweetId);
		Task<ServiceResponseType> Comment (string tweetId, string message);
	}
}

