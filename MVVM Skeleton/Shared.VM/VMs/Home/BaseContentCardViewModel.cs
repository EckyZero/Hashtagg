﻿using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using System.Text;

namespace Shared.VM
{
	public enum SocialType
	{
		Facebook,
		Twitter,
		None
	}

	public abstract class BaseContentCardViewModel : BaseCardViewModel
	{
		#region Private Variables

		private ListItemType _listItemType = ListItemType.Default;
		private string _likeButtonText;
		private string _commentButtonText;
		private string _shareButtonText;

		#endregion

		#region Member Properties

		public override ListItemType ListItemType {
			get { return _listItemType; }
			set { _listItemType = value; } 
		}
			
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

		protected BaseContentCardViewModel () { }

		protected override void InitCommands ()
		{
			base.InitCommands ();

			SelectCommand = new RelayCommand (SelectCommandExecute);
			LikeCommand = new RelayCommand (LikeCommandExecute);
			CommentCommand = new RelayCommand (CommentCommandExecute);
			ShareCommand = new RelayCommand (ShareCommandExecute);
		}

		protected virtual void SelectCommandExecute ()
		{
			// TODO: Repond to card selection
		}

		protected virtual void LikeCommandExecute ()
		{
			// TODO: Repond to like selection
		}

		protected virtual void CommentCommandExecute ()
		{
			// TODO: Repond to comment selection
		}

		protected virtual void ShareCommandExecute ()
		{
			// TODO: Repond to share selection
		}

		protected virtual void MediaCommandExecute ()
		{
			// TODO: Respond to image/movie tap
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

		#endregion
	}
}

