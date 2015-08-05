using System;
using Shared.Common;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Shared.Service
{
	public interface IFacebookService
	{
		Task<ServiceResponse<ObservableCollection<FacebookPost>>> GetHomeFeed ();
		Task<ServiceResponse<FacebookUser>> GetUser ();
		Task<ServiceResponseType> Like (string postId);
		Task<ServiceResponseType> Unlike (string postId);
		Task<ServiceResponseType> Comment (string postId, string message);
		Task<ServiceResponseType> Post (string userId, string message);
	}
}

