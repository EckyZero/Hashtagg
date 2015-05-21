using System;
using UIKit;
using Shared.Common;

namespace iOS
{
	public static class ToolbarExtensions
	{
		public static void ConfigureToCompassDefaults (this UIToolbar toolbar)
		{
			toolbar.BarTintColor = SharedColors.CompassBlue.ToUIColor ();
			toolbar.TintColor = SharedColors.White.ToUIColor ();
			toolbar.Translucent = false;
			toolbar.Opaque = true;
		}
	}
}

