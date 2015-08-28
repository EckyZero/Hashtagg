using System;
using Shared.Common;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using Shared.Service;

namespace Shared.VM
{
	public class CommentGroup
	{
		public HeaderCardViewModel Header { get; set; }
		public IEnumerable<BaseContentCardViewModel> Content { get; set; }

		public CommentGroup (HeaderCardViewModel header, IEnumerable<BaseContentCardViewModel> content)
		{
			Header = header;
			Content = content;
		}
	}

	public class CommentViewModel : SharedViewModelBase
	{
		#region Variables

		private ObservableRangeCollection<IListItem> _cardViewModels = new ObservableRangeCollection<IListItem> ();
		private string _comments;

		#endregion

		#region Properties

		public Action<bool> RequestCanExecute { get; set; }
		public Action RequestDismissKeyboard { get; set; }

		public BaseContentCardViewModel PrimaryCardViewModel { get; set; }

		public string Comments { 
			get { return _comments; } 
			set {
				CanExecute ();
				Set (() => Comments, ref _comments, value);
			} 
		}

		public ObservableRangeCollection<IListItem> CardViewModels 
		{
			get { return _cardViewModels; }
			set { Set (()=> CardViewModels, ref _cardViewModels, value);  }
		}

		public string Title
		{
			get { return ApplicationResources.Comments; }
		}

		public string CommentPlaceholder 
		{
			get { return ApplicationResources.CommentPlaceholder; }
		}

		public RelayCommand ReplyCommand { get; private set; }

		#endregion

		#region Lifecycle

		public CommentViewModel (BaseContentCardViewModel viewModel) : base ()
		{
			PrimaryCardViewModel = viewModel;

            _logger.Track(Analytics.COMMENT_PAGE_VIEWED.ToString());
		}

		#endregion

		#region Methods

		public override async Task DidLoad ()
		{
			// Only fetch new comments as needed
			if(PrimaryCardViewModel.CommentCount != 0 || PrimaryCardViewModel.CommentViewModels.Count == 0)
			{
				await PrimaryCardViewModel.GetComments ();
			}

			// populate all cards
			var viewModels = PrimaryCardViewModel.CommentViewModels.OrderBy (vm => vm.OrderByDateTime).ToList();

			// Group by the relative string of the 15 minute increment
			var groups = viewModels.GroupBy(x =>
				{
					var stamp = x.OrderByDateTime;

					stamp = stamp.AddMinutes(-(stamp.Minute % 15));
					stamp = stamp.AddMilliseconds(-stamp.Millisecond - 1000 * stamp.Second);

					return stamp.ToRelativeString();
				})
				.Select(g =>  new CommentGroup(new HeaderCardViewModel(g.Key), g.Distinct()));

			var contentViewModels = new List<BaseCardViewModel> ();

			// Format the new grouping by including the headers in the main list
			PrimaryCardViewModel.ShowCommentButton = false;

			contentViewModels.Add (PrimaryCardViewModel);

			for (int i = 0; i < groups.Count(); i++)
			{
				var commentGroup = groups.ElementAtOrDefault(i);

				if(i == 0)
				{
					commentGroup.Header.Position = Position.Top;
				}

				contentViewModels.Add (commentGroup.Header);

				foreach(BaseContentCardViewModel viewModel in commentGroup.Content)
				{
					contentViewModels.Add (viewModel);
				}
			}

			// Add the footer header for "now"
			contentViewModels.Add (new HeaderCardViewModel (ApplicationResources.Now, Position.Bottom));

			CardViewModels.Clear ();
			CardViewModels.AddRange (contentViewModels);	
		}

		public override async Task WillDisappear ()
		{
			PrimaryCardViewModel.ShowCommentButton = true;
		}

		protected override void InitCommands () 
		{ 
			ReplyCommand = new RelayCommand (ReplyCommandExecute);		
		}

		private async void ReplyCommandExecute ()
		{
			// TODO: Determine if we need to track the response
			var response = ServiceResponseType.ERROR;

			try
			{
				_hudService.Show();

				await PrimaryCardViewModel.Reply(Comments);
				await DidLoad();

				Comments = string.Empty;

				if(RequestDismissKeyboard != null)
				{
					RequestDismissKeyboard();
				}
			}
			finally
			{
				_hudService.Dismiss ();	
			}
		}

		private void CanExecute ()
		{
			var canExecute = !String.IsNullOrWhiteSpace (Comments) && !Comments.Equals(CommentPlaceholder);

			if(RequestCanExecute != null)
			{
				RequestCanExecute (canExecute);
			}
		}

		#endregion
	}
}

