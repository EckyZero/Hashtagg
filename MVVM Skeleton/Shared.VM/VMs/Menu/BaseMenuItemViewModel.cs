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
		private string _imageName = "Add button.png";

		#endregion

		#region Properties

		public ListItemType ListItemType { 
			get { return _listItemType; }
			set { _listItemType = value; } 
		}

		public MenuItemType MenuItemType {
			get { return _menuItemType; }
			set { _menuItemType = value; }
		}

		public Action<IListItem> OnSelected { get; set; }

		public abstract string Title { get; set; }
		public abstract string Subtitle { get; set; }

		public string ImageName { 
			get { return _imageName; }
			set { Set (() => ImageName, ref _imageName, value); }
		}

		#endregion

		#region Methods

		public BaseMenuItemViewModel (Action<IListItem> selectedCallback) : base ()
		{
			OnSelected = selectedCallback;
		}

		protected override void InitCommands ()
		{
			
		}

		public virtual void Selected ()
		{
			if(OnSelected != null) {
				OnSelected (this);
			}
		}

		protected void UpdateImageName ()
		{
			var imageName = "Add button.png";

			if(MenuItemType == MenuItemType.Added) {
				imageName = "Added button.png";
			} 
			else if (MenuItemType == MenuItemType.Remove) {
				imageName = "Remove button.png";
			}
			ImageName = imageName;
		}

		#endregion
	}
}

