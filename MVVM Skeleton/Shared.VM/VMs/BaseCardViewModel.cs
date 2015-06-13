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
		public abstract SocialType SocialType { get; }

		#endregion

		#region Methods

		protected BaseCardViewModel () {}
			
		protected override void InitCommands ()
		{
			
		}

		#endregion
	}
}

