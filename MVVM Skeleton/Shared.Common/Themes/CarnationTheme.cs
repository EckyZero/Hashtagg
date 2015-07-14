using System;

namespace Shared.Common
{
	public class CarnationTheme : ITheme
	{
		#region Variables

		private PSColor _primaryColor = new PSColor(242, 92, 101);		 // Carnation
		private PSColor _secondaryColor = new PSColor(255, 255, 255);	 // White
		private PSColor _tertiaryColor = new PSColor(3, 122, 255);		 // Blue

		private PSColor _textPrimaryColor = new PSColor(58, 58, 58); 	 // Dark Grey
		private PSColor _textSecondaryColor = new PSColor(190, 190, 190);// Light Grey
		private PSColor _textTertiaryColor = new PSColor(122, 122, 122); // Medium Grey

		private string _backgroundImageName = "App-bg.png";

		#endregion

		#region Properties

		public PSColor PrimaryColor {
			get { return _primaryColor; }
			set { _primaryColor = value; }
		}

		public PSColor SecondaryColor {
			get { return _secondaryColor; }
			set { _secondaryColor = value; }
		}

		public PSColor TertiaryColor {
			get { return _tertiaryColor; }
			set { _tertiaryColor = value; }
		}

		public string BackgroundImageName {
			get { return _backgroundImageName; }
			set { _backgroundImageName = value; }
		}

		#endregion

		#region Methods

		public CarnationTheme () { }

		#endregion
	}
}

