﻿using System;

namespace Shared.Api
{
	public class FacebookLocationDto
	{
		public string City { get; set; }
		public string Country { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string State { get; set; }
		public string Street { get; set; }
		public string Zip { get; set; }

		public FacebookLocationDto ()
		{
		}
	}
}

