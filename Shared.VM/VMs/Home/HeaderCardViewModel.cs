using System;

namespace Shared.VM
{
	public class HeaderCardViewModel : BaseCardViewModel
	{
		#region Private Variables

		private DateTime _dateTime;
		private ListItemType _listItemType = ListItemType.Header;

		#endregion

		#region Member Properties

		public override ListItemType ListItemType 
		{
			get { return _listItemType; }
			set { _listItemType = value; }
		}

		public override SocialType SocialType 
		{
			get { return SocialType.None; }
		}
			
		public string Time 
		{ 
			get { return _dateTime.ToString ("t"); }
		}

		#endregion

		public HeaderCardViewModel (DateTime dateTime)
		{
			_dateTime = dateTime;
		}
	}
}

