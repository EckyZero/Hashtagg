using System;
using Shared.Common;

namespace Shared.VM
{
	public class TwitterCardViewModel : BaseContentCardViewModel
	{
		#region Private Variables

		private Tweet _tweet;

		#endregion

		#region Member Properties

		public override SocialType SocialType
		{
			get { return SocialType.Twitter; }
		}

		public override string ImageUrl 
		{
			get { return _tweet.ImageUrl; }
		}

		public override string UserImageUrl 
		{
			get { return _tweet.UserImageUrl; }
		}

		public override string UserName 
		{
			get { return _tweet.UserName; }
		}

		public override string SocialMediaImage 
		{
			get { return "Twitter_Icon.png"; }
		}

		public override string Text 
		{
			get { return _tweet.Text; }
		}

		public override int? LikeCount 
		{
			get { return _tweet.FavoriteCount; }
		}

		public override int? CommentCount 
		{
			get { return null; }
		}

		public override int? ShareCount 
		{
			get { return _tweet.RetweetCount; }
		}

		public override DateTime CreatedAt 
		{
			get { return _tweet.CreatedAt; }
		}

		public string UserScreenName 
		{
			get { return _tweet.UserScreenName; }
		}

		#endregion

		public TwitterCardViewModel (Tweet tweet)
		{
			_tweet = tweet;
		}
	}
}

