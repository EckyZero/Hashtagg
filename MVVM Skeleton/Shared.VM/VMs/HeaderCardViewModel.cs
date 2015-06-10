using System;

namespace Shared.VM
{
	public class HeaderCardViewModel : BaseCardViewModel
	{
		#region Private Variables

		private DateTime _dateTime;

		#endregion

		#region Member Properties

		public override CardType CardType 
		{
			get { return CardType.Header; }
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

