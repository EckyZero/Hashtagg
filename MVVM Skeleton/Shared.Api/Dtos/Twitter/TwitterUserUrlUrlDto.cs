using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterUserUrlUrlDto
	{
		public string Url { get; set; }
		public string Expanded_Url { get; set; }
		public string Display_Url { get; set; }
		public List<int> Indices { get; set; }

		public TwitterUserUrlUrlDto ()
		{
		}
	}
}

