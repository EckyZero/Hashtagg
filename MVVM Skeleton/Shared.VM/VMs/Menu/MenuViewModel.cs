using System;
using GalaSoft.MvvmLight.Command;
using Shared.Common;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace Shared.VM
{
	public enum MenuState {
		Add,
		Remove
	}

	public class MenuViewModel : SharedViewModelBase
	{
		#region Variables

		private MenuState _menuState = MenuState.Add;
		private string _title = String.Empty;
		private bool _showSubtitle = false;
		private string _primaryButtonText = ApplicationResources.Signout;
		private ObservableRangeCollection<IListItem> _itemViewModels = new ObservableRangeCollection<IListItem> ();

		#endregion

		#region Properties

		public Action<BaseMenuItemViewModel, int> RequestRowUpdate { get; set; }

		public RelayCommand PrimaryCommand { get; private set; }

		public string Title {
			get { return _title; }
			set { _title = value; }
		}

		public string PrimaryButtonText {
			get { return _primaryButtonText; }
			set { Set (() => PrimaryButtonText, ref _primaryButtonText, value); }
		}

		public MenuState MenuState {
			get { return _menuState; }
			set { 
				Set (() => MenuState, ref _menuState, value); 
				PrimaryButtonText = value == MenuState.Add ? ApplicationResources.Signout : ApplicationResources.Done;
			}
		}

		public bool ShowSubtitle {
			get { return _showSubtitle; }
			set { Set (() => ShowSubtitle, ref _showSubtitle, value); }
		}

		public ObservableRangeCollection<IListItem> ItemViewModels 
		{
			get { return _itemViewModels; }
			set { _itemViewModels = value; }
		}

		#endregion

		#region Methods

		public MenuViewModel (string title = "") : base ()
		{
			var twitterViewModel = new TwitterMenuItemViewModel (OnListItemSelected);
			var facebookViewModel = new FacebookMenuItemViewModel (OnListItemSelected);

			ItemViewModels.AddRange (new List<BaseMenuItemViewModel>() { facebookViewModel, twitterViewModel});
			Title = title;
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
			// Toggle the states
			if(MenuState == MenuState.Add) 
			{
				MenuState = MenuState.Remove;
				ConfigureForRemoval ();
			} 
			else if (MenuState == MenuState.Remove) 
			{
				MenuState = MenuState.Add;
				ConfigureForAdding ();
			}
		}

		private void ConfigureForRemoval ()
		{
			var itemsToRemove = new ObservableRangeCollection<BaseMenuItemViewModel> ();

			// Update each item in the list
			foreach (BaseMenuItemViewModel viewModel in ItemViewModels) {
				if(viewModel.MenuItemType == MenuItemType.Added) {
					viewModel.MenuItemType = MenuItemType.Remove;
				} else {
					itemsToRemove.Add (viewModel);
				}
			}

			// Remove the items that aren't editable
			if(itemsToRemove.Count > 0) {
				ItemViewModels.RemoveRange (itemsToRemove);
			}

			// Toggle the visibility of the "All accounts are removed" text
			ShowSubtitle = ItemViewModels.Count == 0;
		}

		private void ConfigureForAdding ()
		{
			
		}

		#endregion
	}
}

