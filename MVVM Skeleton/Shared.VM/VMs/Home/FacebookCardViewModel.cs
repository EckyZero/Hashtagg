using System;
using Shared.Common;
using Shared.Service;
using Microsoft.Practices.Unity;
using System.Text;

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
			get { 
				var builder = new StringBuilder ();

				if(!String.IsNullOrWhiteSpace(_facebookPost.Message)) {
					builder.Append (_facebookPost.Message);
				}
				if(!String.IsNullOrWhiteSpace(_facebookPost.Story)) {
					builder.AppendFormat ("{0}{1}", builder.Length == 0 ? "" : "\n\n", _facebookPost.Story);
				}
				if(!String.IsNullOrWhiteSpace(_facebookPost.Description)) {
					builder.AppendFormat ("{0}{1}", builder.Length == 0 ? "" : "\n\n", _facebookPost.Description);
				}
				return builder.ToString ();
			}
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

		#region Methods

		public FacebookCardViewModel (FacebookPost post)
		{
			_facebookPost = post;
			_facebookService = IocContainer.GetContainer ().Resolve<IFacebookService> ();
		}

		protected override async void LikeCommandExecute ()
		{
			base.LikeCommandExecute ();

			// may need to unlike the post (i.e. toggle state)
			if(IsLikedByUser) {
				await _facebookService.Unlike (_facebookPost.Id);
			} 
			else {
				await _facebookService.Like (_facebookPost.Id);	
			}

			// Increment count
			// Toggle colors
			// Toggle font
		}

		#endregion
	}
}

