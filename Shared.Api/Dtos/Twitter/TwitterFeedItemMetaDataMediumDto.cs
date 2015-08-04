using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterFeedItemMetaDataMediumDto
	{
		public long Id { get; set; }
		public string Id_Str { get; set; }
		public string Media_Url { get; set; }
		public string Media_Url_Https { get; set; }
		public string Url { get; set; }
		public string Display_Url { get; set; }
		public string Expanded_Url { get; set; }
		public string Type { get; set; }

		public TwitterFeedItemMetaDataMediumDto ()
		{

		}
	}
}

