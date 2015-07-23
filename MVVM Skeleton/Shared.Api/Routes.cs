using System;

namespace Shared.Api
{
	public class Routes
	{
		public const string TWITTER_BASE = "https://api.twitter.com/1.1";
		public const string TWITTER_HOME_FEED = "/statuses/home_timeline.json";
		public const string TWITTER_USER = "/users/show.json";
		public const string TWITTER_LIKE = "/favorites/create.json";
		public const string TWITTER_UNLIKE = "/favorites/destroy.json";
		public const string TWITTER_COMMENT = "statuses/update.json";
		public const string TWITTER_DELETE_POST = "statuses/destroy.json";

		public const string FACEBOOK_BASE = "https://graph.facebook.com/v2.3";
		public const string FACEBOOK_HOME_FEED = "/me/home";
		public const string FACEBOOK_USER = "/me";
		public const string FACEBOOK_LIKE = "/likes";
		public const string FACEBOOK_COMMENT = "/comments";
		public const string FACEBOOK_DELETE_COMMENT = "/comment";
	}
}

