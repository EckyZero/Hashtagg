using System;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.Service;

namespace Shared.VM
{
	public class FacebookCommentCardViewModel : BaseContentCardViewModel
	{
		#region Variables

		private FacebookComment _facebookComment;
		private IFacebookService _facebookService;

		#endregion

		#region Properties

		public override SocialType SocialType 
		{
			get { return SocialType.Facebook; }
		}

		public override string ImageUrl 
		{
			get { return string.Empty; }
		}

		public override string UserImageUrl 
		{
			get { return _facebookComment.User.Picture; }
		}

		public override string UserName
		{
			get { return _facebookComment.User.Name; }
		}

		public override string SocialMediaImage 
		{
			get { return "Facebook.png"; }
		}

		public override string Text 
		{
			get { return _facebookComment.Message; }
		}

		public override int? LikeCount 
		{
			get { return _facebookComment.LikedCount; }
		}

		public override int? CommentCount 
		{
			// TODO: Unecessary for this context
			get { return null; }
		}

		public override int? ShareCount 
		{
			// TODO: Unecessary for this context
			get { return null; }
		}

		public override DateTime OrderByDateTime 
		{
			get { return _facebookComment.CreatedAt; }
		}

		public override bool IsLikedByUser 
		{
			get { return _facebookComment.IsLikedByUser; }
			set {
				_facebookComment.IsLikedByUser = value;

				RaisePropertyChanged (() => IsLikedByUser);
			}
		}

		public override bool IsCommentedByUser 
		{
			// TODO: Unecessary for this context
			get { return false; }
			set { }
		}

		public override bool IsSharedByUser 
		{
			// TODO: Unecessary for this context
			get { return false; }
			set { }
		}

		public override bool IsMovie 
		{
			// TODO: Unecessary for this context
			get { return false; }
		}

		public override string MovieUrl 
		{
			// TODO: Unecessary for this context
			get { return string.Empty; }
		}

		#endregion

		#region Methods

		public FacebookCommentCardViewModel (FacebookComment facebookComment) : base ()
		{
			_facebookComment = facebookComment;
			_facebookService = IocContainer.GetContainer ().Resolve<IFacebookService> ();

			ShowTimeline = true;
			ShowCommentButton = false;
			ShowShareButton = false;
			ShowDateTime = false;
			ShowSocialMediaImage = false;
		}

		protected override async void LikeCommandExecute ()
		{
			base.LikeCommandExecute ();

			// may need to unlike the post (i.e. toggle state)
			if(IsLikedByUser) {
				IsLikedByUser = false;
				await _facebookService.Unlike (_facebookComment.Id);
			} 
			else {
				IsLikedByUser = true;
				await _facebookService.Like (_facebookComment.Id);	
			}
		}

		#endregion
	}
}

