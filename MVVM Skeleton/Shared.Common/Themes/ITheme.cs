using System;

namespace Shared.Common
{
	public interface ITheme
	{
		PSColor PrimaryColor { get; set; }
		PSColor SecondaryColor { get; set; }
		PSColor TertiaryColor { get; set; }

		string BackgroundImageName { get; set; }
	}
}

