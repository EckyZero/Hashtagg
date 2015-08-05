using System;
using Shared.Common;

namespace Shared.VM
{
	public class CommentViewModel : SharedViewModelBase
	{
		#region Variables

		private ObservableRangeCollection<IListItem> _cardViewModels = new ObservableRangeCollection<IListItem> ();

		#endregion

		#region Properties

		public ObservableRangeCollection<IListItem> CardViewModels 
		{
			get { return _cardViewModels; }
			set { _cardViewModels = value; }
		}

		public BaseContentCardViewModel PrimaryCardViewModel { get; set; }

		public string Title
		{
			get { return ApplicationResources.Comment; }
		}

		#endregion

		#region Lifecycle

		public CommentViewModel (BaseContentCardViewModel viewModel) : base ()
		{
			PrimaryCardViewModel = viewModel;
			CardViewModels.Add (PrimaryCardViewModel);

			for (int i = 0; i < 10; i++)
			{
//				var vm = new TwitterCommentCardViewModel() {  = "HELLO WORLD!" };
//				CardViewModels.Add(vm);
			}
		}

		#endregion

		#region Methods

		protected override void InitCommands ()
		{
			
		}
			
		#endregion
	}
}

