using System;

namespace Shared.VM
{
	public enum MenuItemType
	{
		Default,
		LoggedIn,
		Loggedout
	}

	public abstract class BaseMenuItemViewModel : SharedViewModelBase, IListItem
	{
		#region Private Variables

		private ListItemType _listItemType = ListItemType.MenuItem;
		private MenuItemType _menuItemType = MenuItemType.Default;

		#endregion

		#region Properties

		public Action RequestDefaultFormat { get; set; }
		public Action RequestLoggedInFormat { get; set; }
		public Action RequestLoggedOutFormat { get; set; }

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

		#endregion

		#region Methods

		public BaseMenuItemViewModel () : base ()
		{
		}

		protected override void InitCommands ()
		{
			
		}

		public abstract void Selected ();

		protected void RequestLoggedInFormatExecute ()
		{
			MenuItemType = MenuItemType.LoggedIn;

			if(RequestLoggedInFormat != null) {
				RequestLoggedInFormat();
			}
		}

		protected void RequestLoggedOutFormatExecute ()
		{
			MenuItemType = MenuItemType.Loggedout;

			if(RequestLoggedOutFormat != null) {
				RequestLoggedOutFormat ();
			}
		}

		protected void RequestDefaultFormatExecute ()
		{
			MenuItemType = MenuItemType.Default;

			if(RequestDefaultFormat != null) {
				RequestDefaultFormat ();
			}
		}

		#endregion
	}
}

