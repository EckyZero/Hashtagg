using System;

namespace Shared.VM
{
	public enum MenuItemType
	{
		Add,
		Added,
		Remove
	}

	public abstract class BaseMenuItemViewModel : SharedViewModelBase, IListItem
	{
		#region Private Variables

		private ListItemType _listItemType = ListItemType.MenuItem;
		private MenuItemType _menuItemType = MenuItemType.Add;

		#endregion

		#region Properties

		public Action<BaseMenuItemViewModel> RequestAddFormat { get; set; }
		public Action<BaseMenuItemViewModel> RequestAddedFormat { get; set; }
		public Action<BaseMenuItemViewModel> RequestRemoveFormat { get; set; }

		public ListItemType ListItemType { 
			get { return _listItemType; }
			set { _listItemType = value; } 
		}

		public MenuItemType MenuItemType {
			get { return _menuItemType; }
			set { _menuItemType = value; }
		}

		public Action<IListItem> OnSelected { get; set; }

		public abstract string Title { get; }
		public abstract string Subtitle { get; }

		public string ImageName { 
			get { 
				var imageName = "Add button.png";

				if(MenuItemType == MenuItemType.Added) {
					imageName = "Added button.png";
				} 
				else if (MenuItemType == MenuItemType.Remove) {
					imageName = "Remove button.png";
				}
				return imageName;
			} 
		}

		#endregion

		#region Methods

		public BaseMenuItemViewModel () : base ()
		{
		}

		protected override void InitCommands ()
		{
			
		}

		public abstract void Selected ();

		protected void RequestAddedFormatExecute ()
		{
			MenuItemType = MenuItemType.Added;

			if(RequestAddedFormat != null) {
				RequestAddedFormat(this);
			}
		}

		protected void RequestRemoveFormatExecute ()
		{
			MenuItemType = MenuItemType.Remove;

			if(RequestRemoveFormat != null) {
				RequestRemoveFormat (this);
			}
		}

		protected void RequestAddFormatExecute ()
		{
			MenuItemType = MenuItemType.Add;

			if(RequestAddFormat != null) {
				RequestAddFormat (this);
			}
		}

		#endregion
	}
}

