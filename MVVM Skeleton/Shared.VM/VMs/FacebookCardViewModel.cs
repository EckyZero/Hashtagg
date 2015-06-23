using System;
using Shared.Common;

namespace Shared.VM
{
	public class FacebookCardViewModel : BaseContentCardViewModel
	{
		#region Private Variables

		private FacebookPost _facebookPost;

		#endregion

		#region Member Properties

		public override SocialType SocialType 
		{
			get { return SocialType.Facebook; }
		}

		public override string ImageUrl 
		{
			get { return _facebookPost.ImageUrl; }
		}

		public override string UserImageUrl 
		{
			// TODO: Facebook only returns userIds. We'll have to do a separate
			// query to get the image
			get { return String.Empty; }
		}

		public override string UserName
		{
			get { return _facebookPost.User.Name; }
		}

		public override string SocialMediaImage 
		{
			get { return "Facebook_Icon.png"; }
		}

		public override string Text 
		{
			// TODO: May need to look into how the text is formatted
			// Sometimes the main body is in a description field, sometimes a story
			get { return _facebookPost.Text; }
		}

		public override int? LikeCount 
		{
			get { return _facebookPost.LikedCount; }
		}

		public override int? CommentCount 
		{
			get { return _facebookPost.Comments.Count; }
		}

		public override int? ShareCount 
		{
			get { return _facebookPost.ShareCount; }
		}

		public override DateTime OrderByDateTime 
		{
			get { return _facebookPost.UpdatedAt; }
		}

		#endregion

		public FacebookCardViewModel (FacebookPost post)
		{
			_facebookPost = post;
		}
	}
}

