using System;
using Shared.Service;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Linq;

namespace Shared.VM
{
	public class HomeViewModel : SharedViewModelBase
	{
		#region Private Variables

		private ITwitterService _twitterService;
		private ITwitterHelper _twitterHelper;
		private IFacebookHelper _facebookHelper;
		private IFacebookService _facebookService;
		private ObservableCollection<BaseCardViewModel> _cardViewModels = new ObservableCollection<BaseCardViewModel> ();

		#endregion

		#region Member Properties

		public ObservableCollection<BaseCardViewModel> CardViewModels 
		{
			get { return _cardViewModels; }
			set { _cardViewModels = value; }
		}

		#endregion

		#region Commands

		public RelayCommand RefreshCommand { get; private set; }

		#endregion

		#region Methods

		public HomeViewModel () : base ()
		{
			_twitterService = IocContainer.GetContainer ().Resolve<ITwitterService> ();
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
			_facebookService = IocContainer.GetContainer ().Resolve<IFacebookService> ();
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		protected override void InitCommands ()
		{
			RefreshCommand = new RelayCommand (RefreshCommandExecute);
		}

		private async void RefreshCommandExecute ()
		{
			_facebookHelper.Authenticate( async () => {
				await GetFacebookFeed();
				});
//			await GetTwitterFeed ();
//			await GetFacebookFeed ();
		}

		private async Task GetFacebookFeed ()
		{
			if( await _facebookHelper.AccountExists())
			{
				var response = new ServiceResponse<ObservableCollection<FacebookPost>> ();

				response = await _facebookService.GetHomeFeed ();

				if(await ProcessResponse(response))
				{
					var viewModelsToRemove = CardViewModels.Where (vm => vm.SocialType == SocialType.Facebook);

					foreach(BaseCardViewModel viewModel in viewModelsToRemove)
					{
						CardViewModels.Remove (viewModel);
					}

					foreach(FacebookPost post in response.Result)
					{
//						var viewModel = new FacebookCardViewModel (post);
//						CardViewModels.Add (viewModel);
					}
				}
			}
		}

		private async Task GetTwitterFeed ()
		{
			if(await _twitterHelper.AccountExists())
			{
				var response = new ServiceResponse<ObservableCollection<Tweet>> ();

				response = await _twitterService.GetHomeFeed ();	

				if(await ProcessResponse(response))
				{
					var viewModelsToRemove = CardViewModels.Where ( vm => vm.SocialType == SocialType.Twitter);

					foreach(BaseCardViewModel viewModel in viewModelsToRemove)
					{
						CardViewModels.Remove (viewModel);
					}

					foreach(Tweet tweet in response.Result)
					{
						var viewModel = new TwitterCardViewModel (tweet);
						CardViewModels.Add (viewModel);
					}
				}
			}
		}

		#endregion
	}
}

