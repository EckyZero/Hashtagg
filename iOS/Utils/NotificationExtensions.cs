using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace iOS
{
	public static class NotificationExtensions
	{
		public static void KeyboardWillShow(this NSNotification notification, ref bool keyboardIsShown, UIScrollView containerView)
		{
			if(keyboardIsShown)
			{
				return;
			}

			var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);

			Action animation = () => {
				containerView.SetContentOffset(new CGPoint(containerView.ContentOffset.X, containerView.ContentOffset.Y + keyboardFrame.Height - 30), true);
			};

			UIScrollView.Animate (200d, 0d, UIViewAnimationOptions.CurveLinear, animation, null);

			keyboardIsShown = true;
		}

		public static void KeyboardWillHide (this NSNotification notification, ref bool keyboardIsShown, UIScrollView containerView)
		{
			if(!keyboardIsShown){
				return;
			}

			var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);

			Action animation = () => {
				containerView.SetContentOffset(new CGPoint(containerView.ContentOffset.X, containerView.ContentOffset.Y - keyboardFrame.Height + 30), true);
			};

			UIScrollView.Animate (50d, 0d, UIViewAnimationOptions.CurveLinear, animation, null);

			keyboardIsShown = false;
		}
	}
}

