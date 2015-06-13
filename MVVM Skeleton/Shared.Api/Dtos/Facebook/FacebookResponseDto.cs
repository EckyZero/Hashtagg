using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class FacebookResponseDto
	{
		public List<FacebookFeedItemDto> Data { get; set; }
		public FacebookPagingDto Paging { get; set; }
	}
}

