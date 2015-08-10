using System;
using Shared.Common;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;

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

		#endregion

		#region Properties

		public BaseContentCardViewModel PrimaryCardViewModel { get; set; }

		public string Comments { get; set; }

		public ObservableRangeCollection<IListItem> CardViewModels 
		{
			get { return _cardViewModels; }
			set { _cardViewModels = value; }
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

			CardViewModels.AddRange (contentViewModels);
		}

		protected override void InitCommands () 
		{ 
			ReplyCommand = new RelayCommand (ReplyCommandExecute);		
		}
			
		private void ReplyCommandExecute ()
		{
				// TODO: Don't forget to fire off CanExecute event
				// TODO: validate text and post to correct outlet
				// TODO: update the UI with the latest post
		}

		#endregion
	}
}

