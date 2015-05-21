using System;

using Foundation;
using UIKit;
using Shared.Common;

namespace iOS.Phone
{
	public partial class ToolTipController : UIViewController
	{
		public string TipKey { get; set; }
		public string Text { get; set; }

		public ToolTipController (IntPtr handle) : base (handle)  {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if(!String.IsNullOrWhiteSpace(TipKey))
			{
				Title = ToolTipHelper.GetToolTipPageTitleUsingKey (TipKey);
				_textView.Text = ToolTipHelper.GetMessageUsingKey (TipKey);	
			}
			else
			{
				_textView.Text = Text;
			}
			_textView.LayoutManager.Delegate = new TextViewDelegate ();

			_doneBarButton.Clicked += OnDoneButtonTapped;

			// TODO: Move colors and fonts to a centralized location
			var enableAttributes = new UITextAttributes () {
				Font = UIFont.FromName ("FuturaStd-Bold", 12),
				TextColor = SharedColors.White.ToUIColor()
			};
			_doneBarButton.SetTitleTextAttributes (enableAttributes, UIControlState.Normal);
			NavigationItem.SetHidesBackButton (true, false);
		}

		private void OnDoneButtonTapped (object sender, EventArgs args)
		{
			NavigationController.PopViewController (true);
		}
	}

	public class TextViewDelegate : NSLayoutManagerDelegate
	{
		public override nfloat LineSpacingAfterGlyphAtIndex (NSLayoutManager layoutManager, nuint glyphIndex, CoreGraphics.CGRect rect)
		{
			return 5.0f;
		}
	}
}
