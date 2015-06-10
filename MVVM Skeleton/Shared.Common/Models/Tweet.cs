using System;

namespace Shared.Common
{
	public class Tweet
	{
		public long Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Text { get; set; }
		public int RetweetCount { get; set; }
		public int FavoriteCount { get; set; }
		public bool IsFavoritedByUser { get; set; }
		public bool IsRetweetedByUser { get; set; }

		public long UserId { get; set; }
		public string UserName { get; set; }
		public string UserScreenName { get; set; }
		public string UserLocation { get; set; }
		public string UserUrl { get; set; }
		public string ProfileImageUrl { get; set; }

		public Tweet ()
		{
		}
	}
}

