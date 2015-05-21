using System;
using UIKit;
using Shared.Common;

namespace iOS
{
	public static class BarButtonItemExtensions
	{
		public static void ConfigureToCompassDefaults (this UIBarButtonItem barButton, bool bold = true)
		{
			var disableAttributes = new UITextAttributes () {
				Font = UIFont.FromName (bold ? "FuturaStd-Bold" : "CenturyGothic", 12),
				TextColor = SharedColors.DarkBlue.ToUIColor()
			};
			var enableAttributes = new UITextAttributes () {
				Font = UIFont.FromName (bold ? "FuturaStd-Bold" : "CenturyGothic", 12),
				TextColor = SharedColors.White.ToUIColor(),
			};
			barButton.SetTitleTextAttributes (disableAttributes, UIControlState.Disabled);
			barButton.SetTitleTextAttributes (enableAttributes, UIControlState.Normal);
		}
	}
}

