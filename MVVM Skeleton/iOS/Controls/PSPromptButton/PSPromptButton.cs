using System;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using UIKit;
using CoreGraphics;
using Shared.Common;

namespace iOS
{
	[Register("PSPromptButton"), DesignTimeVisible(true)]
	public partial class PSPromptButton : UIView
	{
		public event EventHandler Clicked;

		private UIView _view;

		#region Member Properties

		public UIImage Image 
		{
			get { return ImageView.Image; }
			set { ImageView.Image = value; }
		}

		public string Text
		{
			get { return Label.Text; }
			set { Label.Text = value; }
		}

		public nfloat BorderWidth
		{
			get { return _view.Layer.BorderWidth; }
			set { _view.Layer.BorderWidth = value; }
		}

		public UIColor BorderColor
		{
			get { return UIColor.FromCGColor (_view.Layer.BorderColor); }
			set { _view.Layer.BorderColor = value.CGColor; }
		}

		public UIColor TextColor
		{
			get { return Label.TextColor; }
			set { Label.TextColor = value; }
		}

		public UIFont Font
		{
			get { return Label.Font; }
			set { Label.Font = value; }
		}

		#endregion

		private PSPromptButton() {}

		public PSPromptButton (IntPtr handle) : base (handle) 
		{
			var nib = NSBundle.MainBundle.LoadNib("PSPromptButton", this, null);
			_view = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;

			_view.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);

			AddSubview (_view);
		
			Button.TouchUpInside += OnTouchUpInside;
			BorderColor = SharedColors.Tan2.ToUIColor ();
			Font = UIFont.FromName ("CenturyGothic", 14);
		}

		private void OnTouchUpInside (object sender, EventArgs args)
		{
			if (Clicked != null)
			{
				Clicked (this, args);
			}
		}
	}
}

