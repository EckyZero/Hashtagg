using System;
using Shared.Service;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace Shared.VM
{
	public class HomeViewModel : SharedViewModelBase
	{
		#region Private Variables

		private ITwitterService _twitterService;
		private ISocialService _socialService;
		private ObservableCollection<TwitterFeedItem> _twitterFeed = new ObservableCollection<TwitterFeedItem>();

		#endregion

		#region Commands

		public RelayCommand RefreshCommand { get; private set; }

		#endregion

		#region Methods

		public HomeViewModel () : base ()
		{
			_twitterService = IocContainer.GetContainer ().Resolve<ITwitterService> ();
			_socialService = IocContainer.GetContainer ().Resolve<ISocialService> ();
		}

		protected override void InitCommands ()
		{
			RefreshCommand = new RelayCommand (RefreshCommandExecute);
		}

		private async void RefreshCommandExecute ()
		{
			if(await _socialService.TwitterAccountExists())
			{
				await GetTwitterFeed ();
			}
			else
			{
				_socialService.TwitterAuthenticationExecute (async () => await GetTwitterFeed ());
			}
		}

		private async Task GetTwitterFeed ()
		{
			var twitterFeed = await _twitterService.GetHomeFeed ();
			_twitterFeed.Clear ();

			foreach (TwitterFeedItem item in twitterFeed)
			{
				_twitterFeed.Add (item);
			}
		}

		#endregion
	}
}

