using System;
using Shared.Common;
using Shared.Service;
using Microsoft.Practices.Unity;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Shared.VM
{
	public class FacebookCardViewModel : BaseContentCardViewModel
	{
		#region Private Variables

		private FacebookPost _facebookPost;
		private IFacebookService _facebookService;
		private IFacebookHelper _facebookHelper;

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

				return builder.ToString ().Trim();
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
			set {
				_facebookPost.IsLikedByUser = value;

				RaisePropertyChanged (() => IsLikedByUser);
			}
		}

		public override bool IsCommentedByUser 
		{
            get 
            { 
                var account = _facebookHelper.GetAccount();
                var id = account.Properties["id"];
                var isCommentedByUser = _facebookPost.Comments.Any( c => c.User.Id.Equals(id));

                return isCommentedByUser;
            }
            set
            {
                RaisePropertyChanged(() => IsCommentedByUser);
            }
		}

		public override bool IsSharedByUser 
		{
			// TODO: Unsure as to how to gather this data
			get { return false; }
			set { }
		}

		public override bool IsMovie 
		{
			get { return _facebookPost.MediaType == MediaType.Video; }
		}

		public override string MovieUrl 
		{
			get { return _facebookPost.SourceUrl; }
		}

		#endregion

		#region Methods

		public FacebookCardViewModel (FacebookPost post)
		{
			_facebookPost = post;
			_facebookService = IocContainer.GetContainer ().Resolve<IFacebookService> ();
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		protected override async void LikeCommandExecute ()
		{
			base.LikeCommandExecute ();

			// may need to unlike the post (i.e. toggle state)
			if(IsLikedByUser) {
				IsLikedByUser = false;
				await _facebookService.Unlike (_facebookPost.Id);
			} 
			else {
				IsLikedByUser = true;
				await _facebookService.Like (_facebookPost.Id);	
			}
		}

		public override async Task GetComments ()
		{
			// Only add them if they don't already exist in the list
			foreach (FacebookComment comment in _facebookPost.Comments)
			{
				var any = CommentViewModels.Any (vm => vm.UserName.Equals (comment.User.Name) && vm.UserImageUrl.Equals (comment.User.Picture) && vm.Text.Equals (comment.Message));

				if(!any)
				{
					var viewModel = new FacebookCommentCardViewModel (comment);

					CommentViewModels.Add (viewModel);	
				}
			}
		}

		public override async Task Reply (string message)
		{
			// TODO: Grab the comment from the response and save the id
			var account = _facebookHelper.GetAccount ();
//			var response = await _facebookService.Comment (_facebookPost.Id, message);
			var comment = new FacebookComment ()
			{
				Id = Guid.NewGuid().ToString(),
				CreatedAt = DateTime.Now,
				Message = message,
				LikedCount = 0,
				IsLikedByUser = false,
				User = new FacebookUser ()
				{
					Id = account.Properties["id"],
					Name = account.Username,
				}
			};

            IsCommentedByUser = true;
			_facebookPost.Comments.Add (comment);
		}

		#endregion
	}
}

