using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;
using CoreAnimation;
using System.Threading.Tasks;
using Shared.Common;

namespace iOS
{
	public class FeedbackPopup : UIView
	{
		private UIImageView _imageView;

		private UILabel _label;

		private bool _isCentered = false;

		public FeedbackPopup (PSColor backgroundColor, PSColor fontColor, string image, string message) : base(new CGRect(0,0,175.0f,175.0f))
		{
			BackgroundColor = backgroundColor.ToUIColor();
			Opaque = false;

			Layer.CornerRadius = 15.0f;

			_imageView = new UIImageView (UIImage.FromFile (image)) {
				Center = new CGPoint(this.Center.X, this.Center.Y - 30.0f)
			};

			_label = new UILabel (new CGRect(0,0,155,50)){
				Text = message,
				TextColor = fontColor.ToUIColor(),
				Font = UIFont.FromName ("CenturyGothic", 20.0f),
				TextAlignment = UITextAlignment.Center,
				Lines = 2,
				Center = new CGPoint(this.Center.X, this.Center.Y + 30.0f)
			};

			AddSubviews (_imageView, _label);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			if (!_isCentered) {
				Center = Superview.Center;
				_isCentered = true;
			}
		}
	}
}

