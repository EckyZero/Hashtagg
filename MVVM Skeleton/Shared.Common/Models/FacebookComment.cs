using System;

namespace Shared.Common
{
	public class FacebookComment
	{
		public string Id { get; set;}
		public DateTime CreatedAt { get; set; }
		public string Text { get; set; }
		public int LikedCount { get; set; }
		public bool IsLikedByUser { get; set; }

		public FacebookUser User { get; set; }

		public FacebookComment ()
		{
		}
	}
}

