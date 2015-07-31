using System;
using Shared.Service;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Shared.VM
{
	public class MainViewModel : SharedViewModelBase
	{
		#region Variables

		ILifecycleService _lifecycleService;
		private IFacebookHelper _facebookHelper;
		private ITwitterHelper _twitterHelper;

		#endregion

		#region Properties

		public Action<HomeViewModel> RequestHomePage { get; set; }
		public Action<OnboardingViewModel> RequestOnboardingPage { get; set; }

		#endregion

		#region Methods

		public MainViewModel () : base ()
		{
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
			_lifecycleService = IocContainer.GetContainer ().Resolve<ILifecycleService> ();

			_lifecycleService.ApplicationWillStart -= OnApplicationWillStart;
			_lifecycleService.ApplicationWillStart += OnApplicationWillStart;
		}
			
		protected override void InitCommands () { }

		private void OnApplicationWillStart (object sender, EventArgs args)
		{
			var isLoggedIn = _facebookHelper.GetAccount () != null;
			isLoggedIn |= _twitterHelper.GetAccount () != null;

			if(isLoggedIn)
			{
				if (RequestHomePage != null)
				{
					var viewModel = new HomeViewModel ();

					RequestHomePage (viewModel);
				}
			}
			else
			{
				if(RequestOnboardingPage != null)
				{
					var viewModel = new OnboardingViewModel ();

					RequestOnboardingPage (viewModel);
				}
			}
		}

		#endregion
	}
}

