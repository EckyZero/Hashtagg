using System;

namespace Shared.Api
{
	public class TwitterFeedItemRetweetedStatusDto
	{
		public DateTime Created_At { get; set; }
		public long Id { get; set; }
		public string Id_Str { get; set; }
		public string Text { get; set; }
		public string Source { get; set; }
		public bool Truncated { get; set; }
		public TwitterUserDto User { get; set; }
		public bool Is_Quote_Status { get; set; }
		public int Retweet_Count { get; set; }
		public int Favorite_Count { get; set; }
		public TwitterEntityDto Entities { get; set; }
		public bool Favorited { get; set; }
		public bool Retweeted { get; set; }
		public string Lang { get; set; }

		public TwitterFeedItemRetweetedStatusDto ()
		{
		}
	}
}

