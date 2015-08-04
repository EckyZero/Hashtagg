using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

namespace iOS
{
	public static class TableViewExtensions
	{
		public static void AddParrallax (this UITableView tableView, UIView parrallaxView)
		{
			nfloat scrollOffset = tableView.ContentOffset.Y;

			CGRect headerImageFrame = parrallaxView.Frame;

			if (scrollOffset < 0) {
				headerImageFrame.Height = parrallaxView.Frame.Height - scrollOffset;
				headerImageFrame.Y = 0;
			} else {
				headerImageFrame.Height = parrallaxView.Frame.Height;
				headerImageFrame.Y = 0 - ((scrollOffset / 2));
			}
			parrallaxView.Frame = headerImageFrame;
		}

		public static void ScrollViewWithTableView(this UITableView tableView, UIView view, NSLayoutConstraint viewTopConstraint, NSLayoutConstraint tableViewTopConstraint, bool stickToTop = true)
		{
			var offset = tableView.ContentOffset.Y;
//			var headerFrame = view.Frame;
//			var tableViewFrame = tableView.Frame;
//
//			var point = tableView.PanGestureRecognizer.TranslationInView (tableView) ;
//
//			if(point.Y <= 0 && (point.Y * -1) <= view.Frame.Height) {
//				viewTopConstraint.Constant = point.Y;	
//				System.Diagnostics.Debug.WriteLine ("POINT: " + point.Y);
//			}
//			else if (viewTopConstraint.Constant < 0 && viewTopConstraint.Constant >= -view.Frame.Height)
//			{
//				viewTopConstraint.Constant = point.Y;	
//				System.Diagnostics.Debug.WriteLine ("NEW POINT: " + point.Y);
//			}
//			else if (point.Y > 0) {
//				viewTopConstraint.Constant = point.Y;	
//				System.Diagnostics.Debug.WriteLine ("NEW POINT: " + point.Y);
//			}



//			headerFrame.Y = scrollOffset;
//			tableViewFrame.Y = scrollOffset;
//			heaerFrame.
//			tableViewFrame.Height -= scrollOffset:
				
			//			var headerFrameTopConstraint = view.Superview.Constraints.FirstOrDefault (c => c.SecondItem != null && c.SecondItem.Equals (view) && c.FirstAttribute == NSLayoutAttribute.Top);



//			if(stickToTop == true) {
//				if(-headerFrameTopConstraint.Constant < scrollOffset) {
//					headerFrame.Y = -scrollOffset;	
//				}
//			} else {
//			view.SetNeedsLayout();
//			viewTopConstraint.Constant = -scrollOffset/2;
//			tableViewTopConstraint.Constant = -scrollOffset / 2;

//			}
//			view.LayoutIfNeeded();
				
//			view.Frame = headerFrame;
		}

		public static void ScrollToBottom (this UITableView tableView, bool animated)
		{
			tableView.ScrollRectToVisible (new CoreGraphics.CGRect (0, tableView.ContentSize.Height - tableView.Bounds.Size.Height, tableView.Bounds.Size.Width, tableView.Bounds.Size.Height), animated);
		}

		public static void ScrollToTop (this UITableView tableView, bool animated)
		{
			tableView.ScrollRectToVisible (new CGRect(0,0,1,1), animated);
		}

		public static bool IsScrolledToBottom (this UITableView tableView)
		{
			nfloat height = tableView.Frame.Size.Height;
			nfloat contentYoffset = tableView.ContentOffset.Y;
			nfloat distanceFromBottom = tableView.ContentSize.Height - contentYoffset;
			bool isScrolledToBottom = (distanceFromBottom <= height);

			return isScrolledToBottom;
		}

//		public static void SizeHeaderToFitTextView(this UITableView tableView, UITextView textView, bool ignoreNewLineCharacters = true, float customOffset = 0.0f)
//		{
//			textView.Text = FormatString (ignoreNewLineCharacters, textView.Text);
//
//			var nstext = new NSString (textView.Text);
//			var firstAttributes = new UIStringAttributes {
//				Font = textView.Font,
//			};
//			var currentFrame = tableView.TableHeaderView.Frame;
//			var temp = nstext.GetBoundingRect (new CGSize (currentFrame.Width, 0), NSStringDrawingOptions.UsesLineFragmentOrigin, firstAttributes, null);
//			currentFrame.Height += temp.Height + customOffset;
//			tableView.TableHeaderView.Frame = currentFrame;
//			textView.Frame = new CGRect (textView.Frame.X, textView.Frame.Y, textView.ContentSize.Width, textView.ContentSize.Height);
//		}

//		public static void SizeHeaderToFitLabels(this UITableView tableView, List<UILabel> labels, bool ignoreNewLineCharacters = true)
//		{
//			var currentFrame = tableView.TableHeaderView.Frame;
//
//			foreach (UILabel label in labels)
//			{
//				label.Text = FormatString (ignoreNewLineCharacters, label.Text);
//				var nstext = new NSString (label.Text);
//				var firstAttributes = new UIStringAttributes {
//					Font = label.Font,
//				};
//				var temp = nstext.GetBoundingRect (new CGSize (currentFrame.Width, 0), NSStringDrawingOptions.UsesLineFragmentOrigin, firstAttributes, null);
//				currentFrame.Height += temp.Height;
//			}
//				
//			tableView.TableHeaderView.Frame = currentFrame;
//		}


		public static void SizeHeaderToFitLabel(this UITableView tableView, UILabel label, bool ignoreNewLineCharacters = true, float offset = 0f)
		{
			tableView.SizeHeaderToFitLabels (new List<UILabel> () { label }, ignoreNewLineCharacters, offset);
		}

		public static void SizeHeaderToFitLabels(this UITableView tableView, List<UILabel> labels, bool ignoreNewLineCharacters = true, float offset = 0f)
		{
			var currentFrame = tableView.TableHeaderView.Frame;

			nfloat sumHeight = 0f;
			foreach (UILabel label in labels)
			{
				label.Text = FormatString (ignoreNewLineCharacters, label.Text);
				var nstext = new NSString (label.Text);
				var firstAttributes = new UIStringAttributes {
					Font = label.Font,
				};
				var temp = nstext.GetBoundingRect (new CGSize (label.Frame.Width, 0), NSStringDrawingOptions.UsesLineFragmentOrigin, firstAttributes, null);
				sumHeight += temp.Height;
			}

			var viewsThatAreAlighedToTheLeftOfTheScreen = tableView.TableHeaderView.Subviews.Where (v=> v.Frame.Left <=  (tableView.TableHeaderView.Frame.GetMidX()/2f)).OrderBy(v=> v.Frame.Top).ToList();

			var otherViews = viewsThatAreAlighedToTheLeftOfTheScreen.Where(v=> !labels.Contains(v)).ToList();
			var otherViewsHeight = otherViews.Sum (v => v.Frame.Height);

			var spacing = currentFrame.Height - (viewsThatAreAlighedToTheLeftOfTheScreen.Sum(v=> v.Frame.Height));

			var finalHeight = (float) ((double)spacing + otherViewsHeight + (double)sumHeight) + offset;
			currentFrame.Height = finalHeight;

			tableView.TableHeaderView.Frame = currentFrame;
		}
			
		private static string FormatString (bool ignoreNewLineCharacters, string text)
		{
			if(ignoreNewLineCharacters)
			{
				text = text.Replace ("\\n", "");
				text = text.Replace ("\\r", "");
			}
			else 
			{
				text = text.Replace ("\\n", "\n");
				text = text.Replace ("\\r", "\r");
			}
			return text;
		}
	}
}

