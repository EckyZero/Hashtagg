using System;

namespace Shared.Common
{
	public sealed class ThemeManager
	{
		#region Variables

		private static volatile ThemeManager _instance;
		private static object _syncRoot = new Object();

		private ITheme _currentTheme = new CarnationTheme();

		#endregion

		#region Properties

		public static ThemeManager Instance {
			get {
				if (_instance == null) {
					lock (_syncRoot) {
						if (_instance == null) {
							_instance = new ThemeManager();
						}	
					}
				}
				return _instance;
			}
		}

		public ITheme CurrentTheme {
			get { return _currentTheme; }
			set { _currentTheme = value; }
		}

		#endregion

		#region Methods

		private ThemeManager () {}

		#endregion
	}
}

