using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Shared.VM
{
	public class MenuViewModel : SharedViewModelBase
	{
		#region Variables

		private string _primaryButtonText = ApplicationResources.Signout;
		private ObservableRangeCollection<IListItem> _itemViewModels = new ObservableRangeCollection<IListItem> ();

		#endregion

		#region Properties

		public Action<BaseMenuItemViewModel, int> RequestRowUpdate { get; set; }

		public RelayCommand PrimaryCommand { get; private set; }

		public string Title {
			// TODO: Abstract this out of the social services
			get { return "Michael Peters"; }
		}

		public string PrimaryButtonText {
			get { return _primaryButtonText; }
			set { Set (() => PrimaryButtonText, ref _primaryButtonText, value); }
		}

		public ObservableRangeCollection<IListItem> ItemViewModels 
		{
			get { return _itemViewModels; }
			set { _itemViewModels = value; }
		}

		#endregion

		#region Methods

		public MenuViewModel () : base ()
		{
			var twitterViewModel = new TwitterMenuItemViewModel (OnListItemSelected);

			ItemViewModels.Add (twitterViewModel);
		}

		protected override void InitCommands ()
		{
			PrimaryCommand = new RelayCommand (PrimaryCommandExecute);
		}

		private void OnListItemSelected (IListItem item)
		{
			var viewModel = item as BaseMenuItemViewModel;
			var index = ItemViewModels.IndexOf (viewModel);

			if(RequestRowUpdate != null) {
				RequestRowUpdate (viewModel, index);
			}
		}

		private void PrimaryCommandExecute ()
		{
			// TODO: Keep track of state and toggle here
			// May need to iterate through the viewModels and update their editable state
		}

		#endregion
	}
}

