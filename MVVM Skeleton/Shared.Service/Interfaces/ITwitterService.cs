using System;
using Shared.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Shared.Service
{
	public interface ITwitterService
	{
		Task<ServiceResponse<ObservableCollection<Tweet>>> GetHomeFeed ();
	}
}

