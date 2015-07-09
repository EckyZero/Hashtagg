using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Common
{
	public class FacebookPost
	{
		public string Id { get; set;}
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string Message { get; set; }
		public string Story { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public string LinkUrl { get; set; }
		public int ShareCount { get; set; }
		public string LikeUrl { get; set; }
		public string CommentUrl { get; set; }

		public bool IsLikedByUser 
		{
			get 
			{
				var liked = false;
				if(Likes != null)
				{
					liked = Likes.Any ( m => m.Id.Equals(Id));
				}
				return liked;
			}	
		}
		public int LikedCount 
		{ 
			get
			{
				var count = 0;
				if(Likes!= null)
				{
					count = Likes.Count;
				}
				return count;
			}
		}

		public FacebookUser User { get; set; }
		public List<FacebookUser> Likes { get; set; }
		public List<FacebookComment> Comments { get; set; }

		public FacebookPost ()
		{
		}
	}
}

