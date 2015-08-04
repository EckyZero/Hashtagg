using System;

namespace Shared.VM
{
	public abstract class BaseCardViewModel : SharedViewModelBase, IListItem
	{
		#region Member Properties

		public abstract ListItemType ListItemType { get; set; }
		public abstract SocialType SocialType { get; }

		public Action<IListItem> OnSelected { get; set; }

		#endregion

		#region Methods

		protected BaseCardViewModel () {}
			
		protected override void InitCommands ()
		{
			
		}

		#endregion

		public void Selected ()
		{
			if(OnSelected != null) {
				OnSelected (this);
			}
		}
	}
}

