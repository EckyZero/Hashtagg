using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class FacebookCommentResponseDto
	{
		public List<FacebookCommentDto> Data { get; set; }
		public FacebookPagingDto Paging { get; set; }

		public FacebookCommentResponseDto ()
		{
		}
	}
}

