using System;

namespace Shared.Common
{
	public class FacebookUser
	{
		public string Id { get; set;}
		public string Name { get; set;}
		public string Picture { 
			get { return String.Format ("https://graph.facebook.com/v2.3/{0}/picture", Id); } 
		}

		public FacebookUser ()
		{
		}
	}
}

