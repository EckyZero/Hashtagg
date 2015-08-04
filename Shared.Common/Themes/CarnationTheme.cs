using System;

namespace Shared.Common
{
	public class CarnationTheme : ITheme
	{
		#region Properties

		public PSColor PrimaryColor {
			get { return new PSColor(242, 92, 101); }
		}

		public PSColor SecondaryColor {
			get { return new PSColor(255, 255, 255); }
		}

		public PSColor TertiaryColor {
			get { return new PSColor(3, 122, 255); }
		}

		public PSColor TextPrimaryColor {
			get { return new PSColor(58, 58, 58); }
		}

		public PSColor TextSecondaryColor {
			get { return new PSColor(190, 190, 190); }
		}

		public PSColor TextTertiaryColor {
			get { return new PSColor(122, 122, 122); }
		}

		public PSColor Disabled {
			get { return new PSColor(190,190,190,190); }
		}

		public string BackgroundImageName {
			get { return "App-bg.png"; }
		}

		#endregion

		#region Methods

		public CarnationTheme () { }

		#endregion
	}
}

