using System;
using Shared.Common;
using System.Threading.Tasks;
using System.Linq;

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
			// TODO: Insert section headers foreach 15 minute grouping
			var viewModels = PrimaryCardViewModel.CommentViewModels.OrderBy (vm => vm.OrderByDateTime);
//			var dateTimes = viewModels.Select (vm => vm.OrderByDateTime);

			CardViewModels.AddRange (viewModels);
		}

		protected override void InitCommands ()
		{
			
		}
			
		#endregion
	}
}

