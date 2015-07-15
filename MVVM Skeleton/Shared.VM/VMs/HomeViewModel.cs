﻿using System;
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

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		public bool IsLoaded {
			get { return _isLoaded; }
			set { _isLoaded = value; }
		}

		public string DefaultAccountImageName {
			get { return "Profile Image default.png"; }
		}

		#endregion

		#region Commands

		public RelayCommand RefreshCommand { get; private set; }
		public RelayCommand TwitterCommand { get; private set; }
		public RelayCommand FacebookCommand { get; private set; }
		public RelayCommand HeaderImagesCommand { get; private set; }

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

			await GetPosts ();
			await GetName ();
			await GetSocialAccountDetails ();

			HeaderImagesCommandExecute ();

			_isLoaded = true;
		}

		protected override void InitCommands ()
		{
			RefreshCommand = new RelayCommand (RefreshCommandExecute);
			TwitterCommand = new RelayCommand (TwitterCommandExecute);
			FacebookCommand = new RelayCommand (FacebookCommandExecute);
			HeaderImagesCommand = new RelayCommand (HeaderImagesCommandExecute);
		}

		private async void RefreshCommandExecute ()
		{
			await GetPosts ();
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

            if(await _facebookHelper.AccountExists())
			{
				var response = new ServiceResponse<ObservableCollection<FacebookPost>> ();

				response = await _facebookService.GetHomeFeed ();

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

			if(await _twitterHelper.AccountExists())
			{
				var response = new ServiceResponse<ObservableCollection<Tweet>> ();

				response = await _twitterService.GetHomeFeed ();	

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

			if(RequestCompleted != null) {
				RequestCompleted ();
			}
		}

		private async Task GetName ()
		{
			SocialAccount account = null;

			if(await _facebookHelper.AccountExists())
			{
				account = _facebookHelper.GetAccount ();
			}
			else if (await _twitterHelper.AccountExists())
			{
				account = _twitterHelper.GetAccount ();
			}

			Title = account.Username;
		}

		private async Task GetSocialAccountDetails ()
		{
			GetTwitterUserAccountDetails ();
			GetFacebookUserAccountDetails ();
		}

		private async Task GetTwitterUserAccountDetails ()
		{
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

		private void HeaderImagesCommandExecute ()
		{
			var urls = new List<string> ();
			var facebook = _facebookHelper.GetAccount ();
			var twitter = _facebookHelper.GetAccount ();

			if(facebook != null) {
				urls.Add (facebook.Properties ["imageUrl"]);
			}
			if(twitter != null) {
				urls.Add (twitter.Properties ["imageUrl"]);
			}

			if(RequestHeaderImages != null) {
				RequestHeaderImages (urls);
			}
		}



		#endregion
	}
}

