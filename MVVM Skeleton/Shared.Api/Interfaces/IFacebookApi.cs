using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Api
{
	public interface IFacebookApi
	{
		Task<FacebookResponseDto> GetHomeFeed();
		Task<FacebookToFromDto> GetUser ();
	}
}

