using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class FacebookCommentDto
	{
		public string Id { get; set; }
		public FacebookToFromDto From { get; set; }
		public string Message { get; set; }
		public bool Can_Remove { get; set; }
		public DateTime Created_Time { get; set; }
		public int Like_Count { get; set; }
		public bool User_Likes { get; set; }
		public List<FacebookMessageTagDto> Message_Tags { get; set; }

		public FacebookCommentDto ()
		{
		}
	}
}

