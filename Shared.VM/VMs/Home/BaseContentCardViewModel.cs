﻿using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Service;

namespace Shared.VM
{
	public enum SocialType
	{
		Facebook,
		Twitter,
		None
	}

    public abstract class BaseContentCardViewModel : BaseCardViewModel, IEquatable<BaseContentCardViewModel>
	{
		#region Private Variables

		private ListItemType _listItemType = ListItemType.Default;
		private string _likeButtonText;
		private string _commentButtonText;
		private string _shareButtonText;

		#endregion

		#region Actions

		public Action<BaseContentCardViewModel> RequestPhotoViewer { get; set; }
		public Action<BaseContentCardViewModel> RequestMovieViewer { get; set; }
		public Action<CommentViewModel> RequestCommentPage { get; set; }

		#endregion

		#region Member Properties

		public override ListItemType ListItemType {
			get { return _listItemType; }
			set { _listItemType = value; } 
		}
			
		public bool ShowTimeline { get; protected set; }
		public abstract string ImageUrl { get; }
		public abstract string UserImageUrl { get; }
		public abstract string UserName { get; }
		public abstract string SocialMediaImage { get; }
		public abstract string Text { get; }
		public abstract int? LikeCount { get; }
		public abstract int? CommentCount { get; }
		public abstract int? ShareCount { get; }
		public abstract DateTime OrderByDateTime { get; }
		public abstract bool IsLikedByUser { get; set; }
		public abstract bool IsCommentedByUser { get; set; }
		public abstract bool IsSharedByUser { get; set; }
		public abstract bool IsMovie { get; }
		public abstract string MovieUrl { get; }

        private bool _showLikeButton = true;
        public bool ShowLikeButton { get{ return _showLikeButton; } protected set{ _showLikeButton = value; } }
        private bool _showCommentButton = true;
        public bool ShowCommentButton { get{ return _showCommentButton; } set{ _showCommentButton = value; } }
        private bool _showShareButton = true;
        public bool ShowShareButton { get{ return false; } protected set{ } }
        private bool _showDateTime = true;
        public bool ShowDateTime { get{ return _showDateTime; } protected set{ _showDateTime = value; } }
        private bool _showSocialMediaImage = true;
        public bool ShowSocialMediaImage { get{ return _showSocialMediaImage; } protected set{ _showSocialMediaImage = value; } }

		public List<BaseContentCardViewModel> CommentViewModels = new List<BaseContentCardViewModel> ();

		public string DisplayDateTime { 
			get { return OrderByDateTime.ToRelativeString (); }
		}

		public bool ShowImage {
			get { return !String.IsNullOrWhiteSpace (ImageUrl); }
		}

		public string UserImagePlaceholder {
			get { return "Profile Image default.png"; }
		}

		public string LikeButtonText {
			get { 
				_likeButtonText = CalculateButtonText (IsLikedByUser, ApplicationResources.Liked, ApplicationResources.Like, LikeCount);
				return _likeButtonText;
			}
			set { Set (() => LikeButtonText, ref _likeButtonText, value); }
		}

		public string CommentButtonText {
			get { 
				_commentButtonText = CalculateButtonText (IsCommentedByUser, ApplicationResources.Commented, ApplicationResources.Comment, CommentCount);
				return _commentButtonText;
			}
			set { Set (() => CommentButtonText, ref _commentButtonText, value); }
		}

		public string ShareButtonText {
			get { 
				_shareButtonText = CalculateButtonText (IsSharedByUser, ApplicationResources.Shared, ApplicationResources.Share, ShareCount);
				return _shareButtonText;
			}
			set { Set (() => ShareButtonText, ref _shareButtonText, value); }
		}

		public PSColor LikeButtonTextColor {
			get { return CalculateTextColor (IsLikedByUser); }
		}

		public PSColor CommentButtonTextColor {
			get { return CalculateTextColor (IsCommentedByUser); }
		}

		public PSColor ShareButtonTextColor {
			get { return CalculateTextColor (IsSharedByUser); }
		}

		#endregion

		#region Commands

		public RelayCommand SelectCommand { get; private set; }
		public RelayCommand LikeCommand { get; private set; }
		public RelayCommand CommentCommand { get; private set; }
		public RelayCommand ShareCommand { get; private set; }
		public RelayCommand MediaCommand { get; private set; }

		#endregion

		#region Methods

		protected BaseContentCardViewModel () 
		{ 
			ShowTimeline = false;
			CommentViewModels = new List<BaseContentCardViewModel> ();
		}

		protected override void InitCommands ()
		{
			base.InitCommands ();

			SelectCommand = new RelayCommand (SelectCommandExecute);
			LikeCommand = new RelayCommand (LikeCommandExecute);
			CommentCommand = new RelayCommand (CommentCommandExecute);
			ShareCommand = new RelayCommand (ShareCommandExecute);
			MediaCommand = new RelayCommand (MediaCommandExecute);
		}

		protected virtual void SelectCommandExecute ()
		{
			CommentCommandExecute ();
		}

		protected virtual void LikeCommandExecute ()
		{
			// TODO: Repond to like selection
		}

		protected virtual async void CommentCommandExecute ()
		{
			if(RequestCommentPage != null)
			{
				var viewModel = new CommentViewModel (this);

				RequestCommentPage (viewModel);
			}
		}

		protected virtual void ShareCommandExecute ()
		{
			// TODO: Repond to share selection
		}

		protected virtual void MediaCommandExecute ()
		{
			if(IsMovie) {

				// Per Youtube's policies, all videos must be presenting on their site
				if(MovieUrl.Contains("youtube")) {
					
					_browserService.OpenUrl (MovieUrl);
				}
				else if (RequestMovieViewer != null) {
					RequestMovieViewer (this);
				}
			} else {
				if(RequestPhotoViewer != null) {
					RequestPhotoViewer (this);
				}
			}
		}

		private PSColor CalculateTextColor (bool isSelectedByUser)
		{
			var theme = ThemeManager.Instance.CurrentTheme;
			var color = isSelectedByUser ? theme.PrimaryColor : theme.TextTertiaryColor;

			return color;
		}

		private string CalculateButtonText (bool isSelectedByUser, string positiveResponse, string negativeResponse, int? count) 
		{
			var builder = new StringBuilder ();
			builder.Append (isSelectedByUser ? positiveResponse : negativeResponse);

			if(count.HasValue) {
				var userCount = isSelectedByUser ? 1 : 0;
				builder.Append (String.Format (" ({0})", count.Value + userCount));
			}
			return builder.ToString ();
		}

		// Make sure to set the CommentViewModels Property before completing this
		public virtual async Task GetComments () { }

		public virtual async Task Reply(string message) { }

		#endregion

        public bool Equals(BaseContentCardViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            return UserName == other.UserName && Text == other.Text && LikeCount == other.LikeCount && DisplayDateTime == other.DisplayDateTime;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as BaseContentCardViewModel);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (-1521134295) * (UserName.GetHashCode() + Text.GetHashCode() - LikeCount ?? 0 + DisplayDateTime.GetHashCode());
            }
        }

        public override string ToString()
        {
            return String.Format(
                "UserName = {0}, Text = {1}, LikeCount = {3}, DisplayDateTime = {4}",
                UserName,
                Text,
                LikeCount,
                DisplayDateTime
            );
        }

        public static bool operator ==(BaseContentCardViewModel leftOperand, BaseContentCardViewModel rightOperand)
        {
            if (ReferenceEquals(null, leftOperand)) return ReferenceEquals(null, rightOperand);
            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(BaseContentCardViewModel leftOperand, BaseContentCardViewModel rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
	}
}

