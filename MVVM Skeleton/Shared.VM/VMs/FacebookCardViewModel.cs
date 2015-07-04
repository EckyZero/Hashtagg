﻿using System;
using Shared.Common;
using Shared.Service;
using Microsoft.Practices.Unity;

namespace Shared.VM
{
	public class FacebookCardViewModel : BaseContentCardViewModel
	{
		#region Private Variables

		private FacebookPost _facebookPost;
		private IFacebookService _facebookService;

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
			get { return _facebookPost.User.Picture; }
		}

		public override string UserName
		{
			get { return _facebookPost.User.Name; }
		}

		public override string SocialMediaImage 
		{
			get { return "Facebook.png"; }
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

		public override bool IsLikedByUser 
		{
			get { return _facebookPost.IsLikedByUser; }
		}

		public override bool IsCommentedByUser 
		{
			// TODO: Unsure as to how to gather this data
			get { return false; }
		}

		public override bool IsSharedByUser 
		{
			// TODO: Unsure as to how to gather this data
			get { return false; }
		}
			
		#endregion

		public FacebookCardViewModel (FacebookPost post)
		{
			_facebookPost = post;
			_facebookService = IocContainer.GetContainer ().Resolve<IFacebookService> ();

				
		}
	}
}

