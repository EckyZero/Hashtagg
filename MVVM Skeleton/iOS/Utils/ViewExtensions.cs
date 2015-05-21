using System;
using UIKit;
using PDRatingSample;
using System.Linq;
using CoreAnimation;
using CoreGraphics;

namespace iOS
{
	public static class ViewExtensions
	{
		public static void AddStarRatingControl (this UIView view, decimal rating)
		{
			var subview = view.Subviews.FirstOrDefault (v => typeof(PDRatingView) == v.GetType ()) as PDRatingView;

			if (subview != null) {
				subview.AverageRating = rating;
			} else {

				// Add the star rating control
				RatingConfig ratingConfig = new RatingConfig(
					emptyImage: UIImage.FromBundle("GreyStar.png"),
					filledImage: UIImage.FromBundle("GoldStar.png"),
					chosenImage: UIImage.FromBundle("GoldStar.png")
				);

				var ratingView = new PDRatingView (view.Bounds, ratingConfig, rating);

				ratingView.UserInteractionEnabled = false;

				view.AddSubview (ratingView);
				view.BackgroundColor = UIColor.Clear;
			}
		}

		public static void ResizeToFitKeyboard (this UIView view)
		{
			NSLayoutConstraint heightConstraint = null;

			foreach(NSLayoutConstraint constraint in view.Constraints)
			{
				if(constraint.FirstItem.Equals(view) && constraint.FirstAttribute == NSLayoutAttribute.Height) 
				{
					heightConstraint = constraint;

					// Place the text/pin control centered in the available space
					// 20 is for the status bar
					var availableHeight = view.Superview.Frame.Height - Device.KeyboardHeight - 20;
					heightConstraint.Constant = availableHeight;
					view.SetNeedsLayout ();
					view.LayoutIfNeeded ();
					break;
				}
			}
		}


		public static void FadeIn(this UIView view)
		{
			view.Alpha = 0;
			view.Hidden = false;
			UIView.Animate (0.4, () => {
				view.Alpha = 1;
			});
		}

		public static void FadeOut(this UIView view)
		{
			UIView.Animate(0.4, ()=>{
				view.Alpha = 0;
			}, ()=> {
				view.Hidden = true;
			});
		}

		public static void AddGradient(this UIView view, UIColor color, bool transparentToOpaque = true)
		{
			CAGradientLayer gradient = new CAGradientLayer ();

			// The gradient layer must be positioned at the origin of the view
			CGRect gradientFrame = view.Frame;
			gradientFrame.X = 0;
			gradientFrame.Y = 0;
			gradient.Frame = gradientFrame;

			// Build the colors array for the gradient
			CGColor[] colors = new CGColor[] {
				color.ColorWithAlpha(0.9f).CGColor,
				color.ColorWithAlpha(0.6f).CGColor,
				color.ColorWithAlpha(0.3f).CGColor,
				color.ColorWithAlpha(0.1f).CGColor,
				UIColor.Clear.CGColor
			};

			// Reverse the color array if needed
			if(transparentToOpaque)
			{
				colors = colors.Reverse ().ToArray();
			}

			// Apply the colors and the gradient to the view
			gradient.Colors = colors;

			view.Layer.InsertSublayer (gradient, 0);
		}
	}
}

