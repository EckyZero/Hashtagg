using System;
using System.Collections.Generic;

namespace Shared.Api
{
	public class TwitterHashTagDto
	{
		public string Text { get; set; }
		public List<int> Indices { get; set; }

		public TwitterHashTagDto ()
		{
		}
	}
}

