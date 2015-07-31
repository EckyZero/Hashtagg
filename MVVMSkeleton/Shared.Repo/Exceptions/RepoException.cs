using System;
using Shared.Common;

namespace Shared.Repo
{
	public class RepoException : BaseException
	{
		public RepoException()
		{
		}

		public RepoException(string message) : base(message)
		{
		}

		public RepoException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}

