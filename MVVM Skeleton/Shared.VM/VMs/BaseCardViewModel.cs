using System;

namespace Shared.VM
{
	public enum CardType
	{
		Header,
		Image,
		NoImage
	}

	public abstract class BaseCardViewModel : SharedViewModelBase
	{
		#region Member Properties

		public abstract CardType CardType { get; }

		#endregion

		#region Methods

		public BaseCardViewModel () {}
			
		protected override void InitCommands ()
		{
			
		}

		#endregion
	}
}

