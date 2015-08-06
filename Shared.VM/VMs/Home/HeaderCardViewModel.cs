using System;
using Shared.Common;

namespace Shared.VM
{
	public enum Position
	{
		Top,
		Middle,
		Bottom
	}

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
			
		public string Title { get; private set; }
		public Position Position { get; set; }

		#endregion

		public HeaderCardViewModel (string title, Position position = Position.Middle)
		{
			Title = title;
			Position = position;
		}
	}
}

