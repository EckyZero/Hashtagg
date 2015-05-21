using System;
using UIKit;
using Shared.Common;

namespace iOS
{
	public static class ButtonExtensions
	{
		public static void ConfigureToCompassDefaults (this UIButton button)
		{
			button.SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (button.Bounds), UIControlState.Disabled);
			button.SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (button.Bounds), UIControlState.Normal);
			button.ClipsToBounds = true;
			button.Layer.CornerRadius = 5;
		}
	}
}

