using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class FacebookFeedItemDto
	{
		public string Id { get; set; }
		public FacebookToFromDto From { get; set; }
		public string Story { get; set; }
		public string Full_Picture { get; set; }
		public string Link { get; set; }
		public string Source { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public DateTime Created_Time { get; set; }
		public DateTime Updated_Time { get; set; }
		public bool Is_Hidden { get; set; }
		public bool Is_Expired { get; set; }
		public FacebookLikeResponseDto Likes { get; set; }
		public string Message { get; set; }
		public List<FacebookPropertyDto> Properties { get; set; }
		public string Icon { get; set; }
		public List<FacebookActionDto> Actions { get; set; }
		public string Status_Type { get; set; }
		public string Object_Id { get; set; }
		public FacebookSharesDto Shares { get; set; }
		public FacebookCommentResponseDto Comments { get; set; }
		public string Caption { get; set; }
		public FacebookPlaceDto Place { get; set; }
		public FacebookToFromDto To { get; set; }
		public FacebookMessageTagDto Message_Tags { get; set; }

		public FacebookFeedItemDto ()
		{
		}
	}
}

