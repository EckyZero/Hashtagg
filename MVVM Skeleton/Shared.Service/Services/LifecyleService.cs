using System;
using Shared.Common;
using System.Threading.Tasks;

namespace Shared.Service
{
	public class LifecyleService : ILifecycleService
	{
		#region Variables

		private IFacebookHelper _facebookHelper;
		private ITwitterHelper _twitterHelper;

		#endregion

		#region Properties

		public Action RequestHomePage { get; set; }
		public Action RequestOnboardingPage { get; set; }

		#endregion

		public LifecyleService (IFacebookHelper facebookHelper, ITwitterHelper twitterHelper) 
		{ 
			_facebookHelper = facebookHelper;
			_twitterHelper = twitterHelper;
		}

		#region Methods

		public void OnStart ()
		{
			var isLoggedIn = _facebookHelper.GetAccount () != null;
			isLoggedIn |= _twitterHelper.GetAccount () != null;

			if(isLoggedIn)
			{
				if (RequestHomePage != null)
				{
					RequestHomePage ();
				}
			}
			else
			{
				if(RequestOnboardingPage != null)
				{
					RequestOnboardingPage ();
				}
			}
		}

		public void OnResume ()
		{
			
		}

		public void OnPause ()
		{
			
		}

		public void OnTerminated ()
		{
			
		}

		#endregion
	}
}

