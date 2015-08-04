using System;

namespace Shared.Api
{
	public class FacebookPagingDto
	{
		public FacebookCursorsDto Cursors { get; set; }
		public string Next { get; set; }

		public FacebookPagingDto ()
		{
		}
	}
}

