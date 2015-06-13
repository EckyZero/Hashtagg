using System;
using GalaSoft.MvvmLight.Command;

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
		#region Member Properties

		public override CardType CardType 
		{
			get { return String.IsNullOrWhiteSpace (ImageUrl) ? CardType.NoImage : CardType.Image; }
		}
			
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

		#region Commands

		public RelayCommand SelectCommand { get; private set; }
		public RelayCommand LikeCommand { get; private set; }
		public RelayCommand CommentCommand { get; private set; }
		public RelayCommand ShareCommand { get; private set; }

		#endregion

		#region Methods

		public BaseContentCardViewModel ()
		{
		}

		protected override void InitCommands ()
		{
			base.InitCommands ();

			SelectCommand = new RelayCommand (SelectCommandExecute);
			LikeCommand = new RelayCommand (LikeCommandExecute);
			CommentCommand = new RelayCommand (CommentCommandExecute);
			ShareCommand = new RelayCommand (ShareCommandExecute);
		}

		private void SelectCommandExecute ()
		{
			// TODO: Repond to card selection
		}

		private void LikeCommandExecute ()
		{
			// TODO: Repond to like selection
		}

		private void CommentCommandExecute ()
		{
			// TODO: Repond to comment selection
		}

		private void ShareCommandExecute ()
		{
			// TODO: Repond to share selection
		}

		#endregion
	}
}

