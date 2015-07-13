using System;
using UIKit;
using CoreAnimation;
using Foundation;
using CoreGraphics;

namespace iOS
{
	public static class ViewExtensions
	{
		public static void Rotate (this UIView view, float duration, float rotations = 1.0f, bool repeat = false, bool bounce = false)
		{
			var animation = CABasicAnimation.FromKeyPath ("transform.rotation.z");

			animation.To = NSNumber.FromFloat ((float)Math.PI * 2.0f * rotations * duration);
			animation.Duration = duration;
			animation.Cumulative = true;
			animation.RepeatCount = rotations;

			if(bounce) {
				animation.TimingFunction = CAMediaTimingFunction.FromControlPoints (0.5f, 1.8f, 0.5f, 0.8f);	
			}

			view.Layer.AddAnimation (animation, "rotationAnimation");
		}

		public static void FadeOutIn (this UIView view, float duration, bool repeat = false)
		{
			var animation = CABasicAnimation.FromKeyPath("opacity");

			animation.Duration = duration;
			animation.RepeatCount = repeat ? int.MaxValue : duration;
			animation.AutoReverses = true;
			animation.From = NSNumber.FromFloat (1);
			animation.To = NSNumber.FromFloat (0);

			view.Layer.AddAnimation (animation, "animateOpacity");
		}

		public static void Pulse (this UIView view, double duration, float size/*, nfloat fromValue, nfloat toValue, int repeatCount*/)
		{
			var originalFrame = view.Frame;
			var largerFrame = view.Frame;

			largerFrame.Width += size;
			largerFrame.Height += size;
			largerFrame.X -= size/2;
			largerFrame.Y -= size/2;

			UIView.AnimateNotify (duration / 2, () => {
				view.Frame = largerFrame;
			}, (isComplete) => {
				UIView.AnimateNotify (1, 0, 0.4f, 1, 0, () => {
					view.Frame = originalFrame;
				}, null);
			});
//			UIView.AnimateNotify (1, 0, 0.4f, 1, 0, () => {
//				view.Frame = 	
//			}, null);

//			[UIView animateWithDuration:duration/2.f animations:^{
//				self.frame = largerFrame;
//			} completion:^(BOOL finished) {
//				[UIView animateWithDuration:duration/2.f animations:^{
//					self.frame = originalFrame;
//				} completion:^(BOOL finished) {
//					if (completion) {
//						completion();
//					}
//				}];
//			}];
//			var animation = CABasicAnimation.FromKeyPath("transform.scale");
//
//			animation.Duration = duration;
//			animation.RepeatCount = repeatCount;
//			animation.AutoReverses = true;
//			animation.TimingFunction = CAMediaTimingFunction.FromControlPoints (0.5f, 1.8f, 0.5f, 0.8f);	
////			animation.TimingFunction = CAMediaTimingFunction.FromName (CAMediaTimingFunction.EaseInEaseOut);
////			animation.From = NSValue.FromCATransform3D (CATransform3D.MakeScale (fromValue, fromValue, 0));
//			animation.To = NSValue.FromCATransform3D (CATransform3D.MakeScale (toValue, toValue, 0));
//
//			view.Layer.AddAnimation (animation, "animateOpacity");
		}
	}
}

