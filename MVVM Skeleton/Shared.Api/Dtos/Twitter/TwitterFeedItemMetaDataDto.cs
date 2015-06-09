using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterFeedItemMetaDataDto
	{
		public List<object> Hashtags { get; set; }
		public List<object> Symbols { get; set; }
		public List<object> User_mentions { get; set; }
		public List<object> Urls { get; set; }
		public List<TwitterFeedItemMetaDataMediumDto> Media { get; set; }

		public TwitterFeedItemMetaDataDto ()
		{
		}
	}
}

