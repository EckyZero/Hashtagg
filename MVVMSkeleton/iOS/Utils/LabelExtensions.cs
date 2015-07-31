using System;
using UIKit;
using Foundation;

namespace iOS
{
	public static class LabelExtensions
	{
		public static void SetHighlightText(this UILabel label, string fullText, int substringIndex, UIColor highlightColor, bool reverse = false, UIFont font = null)
		{
			if(substringIndex != -1)
			{
				font = font ?? UIFont.FromName (label.Font.FamilyName, label.Font.PointSize);

				var substring = fullText.Substring (substringIndex);
				var startIndex = fullText.IndexOf(substring, StringComparison.CurrentCulture);
				var range = (reverse == true) ? new NSRange(0,startIndex) : new NSRange (startIndex, substring.Length);
				var titleText = new NSMutableAttributedString (fullText);

				titleText.SetAttributes (new UIStringAttributes () { 
					Font = font,
					ForegroundColor = highlightColor
				}, range);

				label.AttributedText = titleText;	
			}
			else
			{
				label.Text = fullText;
			}
		}
	}
}

