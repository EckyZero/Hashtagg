using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterFeedItemDto 
	{
		public DateTime Created_at { get; set; }
		public double Id { get; set; }
		public string Id_Str { get; set; }
		public string Text { get; set; }
		public string Source { get; set; }
		public bool Truncated { get; set; }
		public TwitterUserDto user { get; set; }
		public object Contributors { get; set; }
		public bool Is_Quote_Status { get; set; }
		public int Retweet_Count { get; set; }
		public int Favorite_Count { get; set; }
		public TwitterFeedItemMetaDataDto Entities { get; set; }
		public bool Favorited { get; set; }
		public bool Retweeted { get; set; }
		public bool Possibly_sensitive { get; set; }
		public bool Possibly_Sensitive_Appealable { get; set; }
		public string Lang { get; set; }
		public List<TwitterFeedItemMetaDataMediumDto> Extended_Entities { get; set; }
		public TwitterFeedItemRetweetedStatusDto Retweeted_Status { get; set; }

		public TwitterFeedItemDto ()
		{
		}
	}
}

