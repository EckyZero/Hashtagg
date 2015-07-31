using System;

namespace Shared.Api
{
	public class TwitterEntityDto
	{
		public class Entities
		{
			public TwitterUserUrlDto Url { get; set; }
			public TwitterUserDescriptionDto Description { get; set; }
		}

		public TwitterEntityDto ()
		{
		}
	}
}

