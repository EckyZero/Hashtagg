using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterFeedItemMetaDataDto
	{
		public List<TwitterHashTagDto> Hashtags { get; set; }
		public List<TwitterUserDto> User_Mentions { get; set; }
		public List<TwitterUserUrlUrlDto> Urls { get; set; }
		public List<TwitterFeedItemMetaDataMediumDto> Media { get; set; }

		public TwitterFeedItemMetaDataDto ()
		{
		}
	}
}

