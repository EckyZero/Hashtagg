using System;
using Shared.Service;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System.Collections.Generic;

namespace Shared.VM
{
	public enum OrderBy
	{
		Popularity,
		Time
	}

	public class HomeViewModel : SharedViewModelBase
	{
		#region Variables

		private bool _isLoaded = false;
		private bool _isNoAccountsExistPromptHidden = true;
		private string _title = String.Empty;
		private OrderBy _orderBy = OrderBy.Time;
		private ITwitterService _twitterService;
		private ITwitterHelper _twitterHelper;
		private IFacebookHelper _facebookHelper;
		private IFacebookService _facebookService;
		private ObservableRangeCollection<IListItem> _cardViewModels = new ObservableRangeCollection<IListItem> ();

		#endregion

		#region Actions

		public Action RequestCompleted { get; set; }
		public Action<List<string>> RequestHeaderImages { get; set; }
		public Action<BaseContentCardViewModel> RequestPhotoViewer { get; set; }
		public Action<BaseContentCardViewModel> RequestMovieViewer { get; set; }
		public Action<CommentViewModel> RequestCommentPage { get; set; }
        public Action<PostViewModel> RequestPostPage { get; set; }

		#endregion

		#region Properties

		public OrderBy OrderBy
		{
			get { return _orderBy; }
			set { _orderBy = value; }
		}

		public ObservableRangeCollection<IListItem> CardViewModels 
		{
			get { return _cardViewModels; }
			set { _cardViewModels = value; }
		}

		public string Title 
		{
			get { return _title; }
 			set { Set (() => Title, ref _title, value); }
		}

		public bool IsLoaded 
		{
			get { return _isLoaded; }
			set { _isLoaded = value; }
		}

		public bool IsNoAccountsExistPromptHidden 
		{
			get { return _isNoAccountsExistPromptHidden; }
			set { Set (() => IsNoAccountsExistPromptHidden, ref _isNoAccountsExistPromptHidden, value); }
		}

		public string DefaultAccountImageName 
		{
			get { return "Profile Image default.png"; }
		}

		public bool IsRefreshing { get; set; }

        public bool ShowPostOption
        {
            get 
            {
                var atLeastOneAccountExists = 
                    _facebookHelper.GetAccount() != null ||
                    _twitterHelper.GetAccount() != null;

                return atLeastOneAccountExists;
            }
        }

		#endregion

		#region Commands

		public RelayCommand RefreshCommand { get; private set; }
		public RelayCommand TwitterCommand { get; private set; }
		public RelayCommand FacebookCommand { get; private set; }
        public RelayCommand PostCommand { get; private set; }

		#endregion

		#region Methods

		public HomeViewModel () : base ()
		{
			_twitterService = IocContainer.GetContainer ().Resolve<ITwitterService> ();
			_twitterHelper = IocContainer.GetContainer ().Resolve<ITwitterHelper> ();
			_facebookService = IocContainer.GetContainer ().Resolve<IFacebookService> ();
			_facebookHelper = IocContainer.GetContainer ().Resolve<IFacebookHelper> ();
		}

		public override async Task DidLoad ()
		{
			await base.DidLoad ();

			if(!_isLoaded) 
			{
				_isLoaded = true;

				await base.DidLoad ();
				await GetPosts();	
			}
		}

		public override async Task DidAppear ()
		{
			await base.DidAppear();

			GetName ();
			await GetSocialAccountDetails ();

			GetHeaderImages ();
			RemoveCardsIfNeeded ();
		}

		protected override void InitCommands ()
		{
			RefreshCommand = new RelayCommand (RefreshCommandExecute);
			TwitterCommand = new RelayCommand (TwitterCommandExecute);
			FacebookCommand = new RelayCommand (FacebookCommandExecute);
            PostCommand = new RelayCommand (PostCommandExecute);
		}

		private async void RefreshCommandExecute ()
		{
			if(!IsRefreshing)
			{
				IsRefreshing = true;
				await GetPosts ();	
			}
		}

		private void TwitterCommandExecute ()
		{
			_twitterHelper.Authenticate (async () => await _twitterService.GetHomeFeed() );
		}

		private void FacebookCommandExecute ()
		{
			_facebookHelper.Authenticate (async () => await _facebookService.GetHomeFeed() );
		}

		private async Task<ObservableCollection<FacebookCardViewModel>> GetFacebookFeed ()
		{
			var viewModels = new ObservableCollection<FacebookCardViewModel> ();

			if(_facebookHelper.GetAccount() != null)
			{
				var response = await _facebookService.GetHomeFeed ();

				if(await ProcessResponse(response))
				{
					foreach(FacebookPost post in response.Result)
					{
						var viewModel = new FacebookCardViewModel (post);
						viewModels.Add (viewModel);
					}
				}
			}
			return viewModels;
		}

		private async Task<ObservableCollection<TwitterCardViewModel>> GetTwitterFeed ()
		{
			var viewModels = new ObservableCollection<TwitterCardViewModel> ();

			if(_twitterHelper.GetAccount() != null)
			{
				var response = await _twitterService.GetHomeFeed ();	

				if(await ProcessResponse(response))
				{
					foreach(Tweet tweet in response.Result)
					{
						var viewModel = new TwitterCardViewModel (tweet);
						viewModels.Add (viewModel);
					}
				}
			}
			return viewModels;
		}

		public async Task GetPosts ()
		{
			var facebookViewModels = await GetFacebookFeed();
			var twitterViewModels = await GetTwitterFeed ();

			var allViewModels = new ObservableRangeCollection<BaseContentCardViewModel> ();

			// TODO: Need to further develop this system of ordering
			// Need to be able to toggle between popularity and time
			//			allViewModels.AddRange (facebookViewModels);
			//			allViewModels.AddRange (twitterViewModels);

			Random rnd = new Random();

			int total = facebookViewModels.Count + twitterViewModels.Count;
			double fbLuck = 0;
			double tLuck = 0;

			fbLuck = facebookViewModels.Count/(double) total;
			tLuck = twitterViewModels.Count/(double) total;

			for (int i = 0; i < total; i++)
			{
				int newTotal = 0;
				var dicey = rnd.NextDouble();
				if (dicey < fbLuck)
				{
					allViewModels.Add(facebookViewModels.First());
					facebookViewModels.RemoveAt(0);
				}
				else
				{
					allViewModels.Add(twitterViewModels.First());
					twitterViewModels.RemoveAt(0);
				}
				newTotal = facebookViewModels.Count + twitterViewModels.Count;
				if (newTotal != 0)
				{
					fbLuck = facebookViewModels.Count/(double) newTotal;
					tLuck = twitterViewModels.Count/(double) newTotal;
				}
			}



			//
			//			if(OrderBy == OrderBy.Time)
			//			{
			//				allViewModels = new ObservableRangeCollection<BaseContentCardViewModel> (allViewModels.OrderByDescending (vm => vm.OrderByDateTime));
			//			}
			//
			CardViewModels.Clear ();
			CardViewModels.AddRange (allViewModels);

			foreach (BaseContentCardViewModel viewModel in CardViewModels)
			{
				viewModel.RequestMovieViewer = RequestMovieViewer;
				viewModel.RequestPhotoViewer = RequestPhotoViewer;
				viewModel.RequestCommentPage = RequestCommentPage;
			}

			IsRefreshing = false;

			if(RequestCompleted != null) {
				RequestCompleted ();
			}
		}

		public string GetName ()
		{
			// Get the display name
			SocialAccount account = null;

			if(_facebookHelper.GetAccount() != null)
			{
				account = _facebookHelper.GetAccount ();
			}
			else if (_twitterHelper.GetAccount() != null)
			{
				account = _twitterHelper.GetAccount ();
			}
			Title = (account == null) ? string.Empty : account.Username;

			return Title;
		}

		private async Task GetSocialAccountDetails ()
		{
			// Sync all accounts
			await GetTwitterUserAccountDetails ();
			await GetFacebookUserAccountDetails ();
		}
		
		private async Task GetTwitterUserAccountDetails ()
		{
			// Sync twitter account details
			if(await _twitterHelper.AccountExists())
			{
				var account = _twitterHelper.GetAccount ();

				if(!account.Properties.ContainsKey("imageUrl") )
				{
					var twitterResponse = await _twitterService.GetUser (account.Username);

					if(await ProcessResponse(twitterResponse, false))
					{
						var user = twitterResponse.Result;

						account.Properties ["name"] = user.Name;
						account.Properties ["id"] = user.Id;
						account.Properties ["imageUrl"] = user.Picture;

						_twitterHelper.Synchronize (account);
					}	
				}	
			}
		}

		private async Task GetFacebookUserAccountDetails ()
		{
			// Sync facebook account details
			if(await _facebookHelper.AccountExists())
			{
				var account = _facebookHelper.GetAccount ();

				if(!account.Properties.ContainsKey("imageUrl"))
				{
					var facebookResponse = await _facebookService.GetUser ();	

					if(await ProcessResponse(facebookResponse, false))
					{
						var user = facebookResponse.Result;

						account.Properties ["name"] = user.Name;
						account.Properties ["screen_name"] = user.Name;
						account.Properties ["id"] = user.Id;
						account.Properties ["imageUrl"] = user.Picture;

						_facebookHelper.Synchronize (account);
					}	
				}	
			}
		}

		private void GetHeaderImages ()
		{
			// This gets the imageUrls for each synced account
			// It also toggles the "No accounts exist" prompt if none are found
			var urls = new List<string> ();
			var facebook = _facebookHelper.GetAccount ();
			var twitter = _twitterHelper.GetAccount ();

			if(facebook != null && facebook.Properties.ContainsKey("imageUrl")) {
				urls.Add (facebook.Properties ["imageUrl"]);
			}
			if(twitter != null && twitter.Properties.ContainsKey("imageUrl")) {
				urls.Add (twitter.Properties ["imageUrl"]);
			}

			// Also toggle the state of the label prompt
			IsNoAccountsExistPromptHidden = urls.Count != 0;

			if(RequestHeaderImages != null) {
				RequestHeaderImages (urls);
			}
		}

		private void RemoveCardsIfNeeded ()
		{
			// Remove cards that no longer have an associated account
			// This is to protected against a user signing out of an account in the menu
			// And then trying to take action on a card
			var removableViewModels = CardViewModels.Where (vm => vm.ListItemType == ListItemType.Default);
			var cardsToRemove = new List<IListItem> ();

			if(_twitterHelper.GetAccount() == null) {
				var tViewModels = removableViewModels.Where (vm => ((BaseContentCardViewModel)vm).SocialType == SocialType.Twitter);
				cardsToRemove.AddRange (tViewModels);
			}
			if(_facebookHelper.GetAccount() == null) {
				var fViewModels = removableViewModels.Where (vm => ((BaseContentCardViewModel)vm).SocialType == SocialType.Facebook);
				cardsToRemove.AddRange (fViewModels);
			}

			CardViewModels.RemoveRange (cardsToRemove);
		}

        public string FacebookImageUrl {
            get { return _facebookHelper.GetAccount().Properties["imageUrl"]; }
        }

        public void PostCommandExecute ()
        {
            if(RequestPostPage != null)
            {
                RequestPostPage(new PostViewModel());
            }
        }

		#endregion
	}
}

