using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class FacebookLikeResponseDto
	{
		public List<FacebookLikeDto> Data { get; set; }
		public FacebookPagingDto Paging { get; set; }

		public FacebookLikeResponseDto ()
		{
		}
	}
}

