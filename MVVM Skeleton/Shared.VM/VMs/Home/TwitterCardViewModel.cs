﻿using System;
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
			get { return String.Format("{0} @{1}",_tweet.UserName, _tweet.UserScreenName); }
		}

		public override string SocialMediaImage 
		{
			get { return "Twitter.png"; }
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
			// TODO: Unsure as to how to gather this data
			get { return null; }
		}

		public override int? ShareCount 
		{
			get { return _tweet.RetweetCount; }
		}

		public override DateTime OrderByDateTime 
		{
			get { return _tweet.CreatedAt; }
		}

		public override bool IsLikedByUser 
		{
			get { return _tweet.IsFavoritedByUser; }
		}

		public override bool IsCommentedByUser 
		{
			// TODO: Unsure as to how to gather this data
			get { return _tweet.IsRetweetedByUser; }
		}

		public override bool IsSharedByUser 
		{
			get { return _tweet.IsRetweetedByUser; }
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

