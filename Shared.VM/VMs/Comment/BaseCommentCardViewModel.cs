using System;
using Shared.Common;

namespace Shared.VM
{
	public abstract class BaseCommentCardViewModel : BaseContentCardViewModel
	{
		#region Properties

		// Actual values displayed in the cell
		public abstract override SocialType SocialType { get; }
		public abstract override string ImageUrl { get; }
		public abstract override string UserImageUrl { get; }
		public abstract override string UserName { get; }
		public abstract override string SocialMediaImage { get; }
		public abstract override string Text { get; }
		public abstract override int? LikeCount { get; }
		public abstract override DateTime OrderByDateTime { get; }
		public override bool IsLikedByUser { get; set; }

		// For a comment card, we don't care about these properties
		// override them to be null or 0
		public override int? CommentCount 
		{
			get { return 0; }
		}

		public override int? ShareCount 
		{
			get { return 0; }
		}

		public override bool IsCommentedByUser 
		{ 
			get { return false; }
			set { } 
		}

		public override bool IsSharedByUser 
		{ 
			get { return false; }
			set { } 
		}

		public override bool IsMovie 
		{
			get { return false; }
		}

		public override string MovieUrl 
		{
			get { return string.Empty; }
		}
			
		#endregion

		#region Methods

		public BaseCommentCardViewModel () : base ()
		{
		}

		#endregion
	}
}

