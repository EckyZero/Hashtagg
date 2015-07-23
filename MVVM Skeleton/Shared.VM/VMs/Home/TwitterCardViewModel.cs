using System;
using Shared.Common;
using Shared.Service;
using Microsoft.Practices.Unity;

namespace Shared.VM
{
	public class TwitterCardViewModel : BaseContentCardViewModel
	{
		#region Private Variables

		private Tweet _tweet;
		private ITwitterService _twitterService;

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
			set {
				_tweet.IsFavoritedByUser = value;
				RaisePropertyChanged (() => IsLikedByUser);
			}
		}

		public override bool IsCommentedByUser 
		{
			// TODO: Unsure as to how to gather this data
			get { return _tweet.IsRetweetedByUser; }
			set { 
				_tweet.IsRetweetedByUser = value; 
				RaisePropertyChanged (() => IsCommentedByUser);
			}
		}

		public override bool IsSharedByUser 
		{
			get { return _tweet.IsRetweetedByUser; }
			set {
				_tweet.IsRetweetedByUser = value;
				RaisePropertyChanged (() => IsSharedByUser);
			}
		}

		public string UserScreenName 
		{
			get { return _tweet.UserScreenName; }
		}

		public override bool IsMovie 
		{
			get { return false; }
		}

		public override string MovieUrl 
		{
			// TODO: Change model object to contain the MediaUrl
			get { return String.Empty; }
		}

		#endregion

		#region Methods

		public TwitterCardViewModel (Tweet tweet)
		{
			_tweet = tweet;
			_twitterService = IocContainer.GetContainer ().Resolve<ITwitterService> ();
		}

		#endregion

		protected override async void LikeCommandExecute ()
		{
			base.LikeCommandExecute ();

			// may need to unlike the post (i.e. toggle state)
			if(IsLikedByUser) {
				IsLikedByUser = false;
				await _twitterService.Unlike (_tweet.Id);
			} 
			else {
				IsLikedByUser = true;
				await _twitterService.Like (_tweet.Id);	
			}
		}

		protected override void CommentCommandExecute ()
		{
			base.CommentCommandExecute ();

			// TODO: present screen to enter comment
		}
	}
}

