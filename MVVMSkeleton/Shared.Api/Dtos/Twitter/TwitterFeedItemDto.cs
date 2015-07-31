using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterFeedItemDto 
	{
		public DateTime Created_At { get; set; }
		public long Id { get; set; }
		public string Text { get; set; }
		public TwitterUserDto User { get; set; }
		public int Retweet_Count { get; set; }
		public int Favorite_Count { get; set; }
		public TwitterFeedItemMetaDataDto Entities { get; set; }
		public string Lang { get; set; }
		public TwitterFeedItemExtendedEntityDto Extended_Entities { get; set; }
		public TwitterFeedItemRetweetedStatusDto Retweeted_Status { get; set; }
		public bool Favorited { get; set; }
		public bool Retweeted { get; set; }

		public TwitterFeedItemDto ()
		{
		}
	}
}

