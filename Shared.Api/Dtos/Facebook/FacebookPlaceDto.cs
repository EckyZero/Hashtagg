using System;

namespace Shared.Api
{
	public class FacebookPlaceDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public FacebookLocationDto Location { get; set; }

		public FacebookPlaceDto ()
		{
		}
	}
}

