using System;
using System.Collections.Generic;
using System.Net;

namespace Shared.Common
{
	public class SocialAccount
	{
		public string Username { get; set; }
		public Dictionary<string,string> Properties { get; set; }
		public CookieContainer Cookies { get; set; }

		public SocialAccount (string username, Dictionary<string,string> properties, CookieContainer cookies)
		{
			Username = username;
			Properties = properties;
			Cookies = cookies;
		}
	}
}

