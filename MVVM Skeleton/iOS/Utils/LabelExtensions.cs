using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace iOS
{
	public static class LabelExtensions 
	{
		public static nfloat HeightToFitContent(this UILabel label)
		{
			nfloat height = 0;
			var frameHeight = label.Frame.Height;
			var text = new NSString (label.Text ?? string.Empty);
			var firstAttributes = new UIStringAttributes {
				Font = label.Font,
			};
			var temp = text.GetBoundingRect (new CGSize (label.Frame.Width, 0), NSStringDrawingOptions.UsesLineFragmentOrigin, firstAttributes, null);

			height += temp.Height - frameHeight;

			return height;
		}
	}
}

