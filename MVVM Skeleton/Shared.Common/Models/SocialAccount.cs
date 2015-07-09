using System;
using System.Collections.Generic;
using System.Net;

namespace Shared.Common
{
	public class SocialAccount
	{
		public string Username { get; set; }
		public IDictionary<string,string> Properties { get; set; }
		public CookieContainer Cookies { get; set; }

		public SocialAccount (string username, IDictionary<string,string> properties, CookieContainer cookies)
		{
			Username = username;
			Properties = properties;
			Cookies = cookies;
		}
	}
}

