using System;

namespace Shared.Api
{
	public class FacebookMessageTagDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public int Offset { get; set; }
		public int Length { get; set; }

		public FacebookMessageTagDto ()
		{
		}
	}
}

