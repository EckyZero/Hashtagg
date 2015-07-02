using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;

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

		public string DisplayDateTime { 
			get { return OrderByDateTime.ToRelativeString (); }
		}

		public bool ShowImage {
			get { return !String.IsNullOrWhiteSpace (ImageUrl); }
		}

		#endregion

		#region Commands

		public RelayCommand SelectCommand { get; private set; }
		public RelayCommand LikeCommand { get; private set; }
		public RelayCommand CommentCommand { get; private set; }
		public RelayCommand ShareCommand { get; private set; }

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

		#endregion
	}
}

