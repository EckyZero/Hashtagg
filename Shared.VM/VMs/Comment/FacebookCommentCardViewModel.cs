using System;
using Shared.Common;

namespace Shared.VM
{
	public class FacebookCommentCardViewModel : BaseCommentCardViewModel
	{
		#region Variables

		private FacebookPost _post;

		#endregion

		#region Properties

		public override SocialType SocialType {
			get {
				throw new NotImplementedException ();
			}
		}

		public override string ImageUrl {
			get {
				throw new NotImplementedException ();
			}
		}

		public override string UserImageUrl {
			get {
				throw new NotImplementedException ();
			}
		}

		public override string UserName {
			get {
				throw new NotImplementedException ();
			}
		}

		public override string SocialMediaImage {
			get {
				throw new NotImplementedException ();
			}
		}

		public override string Text {
			get {
				throw new NotImplementedException ();
			}
		}

		public override int? LikeCount {
			get {
				throw new NotImplementedException ();
			}
		}

		public override DateTime OrderByDateTime 
		{
			get { return _post.UpdatedAt; }
		}

		#endregion

		#region Methods

		public FacebookCommentCardViewModel (FacebookPost post) : base ()
		{
			_post = post;
		}

		#endregion
	}
}

