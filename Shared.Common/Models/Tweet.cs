using System;
using System.Collections.Generic;

namespace Shared.Common
{
	public class Tweet
	{
		public string Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string Text { get; set; }
		public string ImageUrl { get; set; }
		public bool IsFavoritedByUser { get; set; }
		public bool IsRetweetedByUser { get; set; }
		public int RetweetCount { get; set; }
		public int FavoriteCount { get; set; }

		public long UserId { get; set; }
		public string UserName { get; set; }
		public string UserScreenName { get; set; }
		public string UserLocation { get; set; }
		public string UserUrl { get; set; }
		public string UserImageUrl { get; set; }

		public List<TwitterUser> CommentedOnByUsers { get; set; }

		public Tweet ()
		{
		}
	}
}

