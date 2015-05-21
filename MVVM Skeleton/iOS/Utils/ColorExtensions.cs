using System;
using UIKit;
using CoreGraphics;
using Shared.Common;

namespace iOS
{
	public static class ColorExtensions
	{
		public static UIImage ToImage(this UIColor color, CGRect rect)
		{
			UIGraphics.BeginImageContext (rect.Size);
			var context = UIGraphics.GetCurrentContext ();

			context.SetFillColor (color.CGColor);
			UIGraphics.RectFill (rect);

			var image = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();


			return image;
		}

		public static UIColor ToUIColor(this PSColor color)
		{
            var uiColor = UIColor.FromRGBA(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);

			return uiColor;
		}
	}
}

