using System;

namespace Shared.VM
{
	public enum SocialType
	{
		Facebook,
		Twitter,
		Flickr
	}

	public abstract class BaseContentCardViewModel : BaseCardViewModel
	{
		#region Member Properties

		public abstract SocialType Type { get; }
		public abstract string ImageUrl { get; }
		public abstract string UserImageUrl { get; }
		public abstract string UserName { get; }
		public abstract string UserScreenName { get; }
		public abstract string SocialMediaImage { get; }
		public abstract string Text { get; }
		public abstract int? LikeCount { get; }
		public abstract int? CommentCount { get; }
		public abstract int? ShareCount { get; }
		public abstract DateTime CreatedAt { get; }

		#endregion

		public BaseContentCardViewModel ()
		{
		}
	}
}

