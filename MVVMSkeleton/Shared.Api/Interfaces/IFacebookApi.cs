using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Api
{
	public interface IFacebookApi
	{
		Task<FacebookResponseDto> GetHomeFeed();
		Task<FacebookToFromDto> GetUser ();
		Task Like (string postId);
		Task Unlike (string postId);
		Task Comment (string postId, string message);
		Task DeleteComment (string commentId);
		Task Post (string userId, string message);
	}
}

