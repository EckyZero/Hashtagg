using System;

namespace Shared.Common
{
	public interface ITheme
	{
		PSColor PrimaryColor { get; }
		PSColor SecondaryColor { get; }
		PSColor TertiaryColor { get; }

		PSColor TextPrimaryColor { get; }
		PSColor TextSecondaryColor { get;  }
		PSColor TextTertiaryColor { get; }

		PSColor Disabled { get; }

		string BackgroundImageName { get; }
	}
}

