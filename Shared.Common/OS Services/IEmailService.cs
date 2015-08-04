using System;

namespace Shared.Common
{
	public interface IEmailService
	{
		bool Email(string emailTo = null);
	}
}

