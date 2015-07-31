using CoreGraphics;
using UIKit;

namespace iOS
{
	public static class ImageExtensions
	{
		public static UIImage AddColor(this UIImage image, UIColor color)
		{
			UIImage coloredImage = null;

			UIGraphics.BeginImageContext(image.Size);
			using (CGContext context = UIGraphics.GetCurrentContext())
			{

				context.TranslateCTM(0, image.Size.Height);
				context.ScaleCTM(1.0f, -1.0f);

				var rect = new CGRect(0, 0, image.Size.Width, image.Size.Height);

				// draw image, (to get transparancy mask)
				context.SetBlendMode(CGBlendMode.Normal);
				context.DrawImage(rect, image.CGImage);

				// draw the color using the sourcein blend mode so its only draw on the non-transparent pixels
				context.SetBlendMode(CGBlendMode.SourceIn);
				context.SetFillColor(color.CGColor);
				context.FillRect(rect);

				coloredImage = UIGraphics.GetImageFromCurrentImageContext();
				UIGraphics.EndImageContext();
			}
			return coloredImage;
		}
	}
}

