using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using Shared.Common;

namespace iOS.Phone
{
	[Register("FullMapSearchBar"), DesignTimeVisible(true)]
	public partial class FullMapSearchBar : UIView
	{
	
		private Action _onPromptTap;

		public string Text {
			get{ return this.SearchTextLabel.Text; }
			set{ this.SearchTextLabel.Text = value; }
		}

		public FullMapSearchBar (Action onPromptTap)
		{
			_onPromptTap = onPromptTap;

			var view = InitView ();
			Frame = view.Frame;
			AddSubview (view);
			Layer.BorderColor = SharedColors.Tan2.ToUIColor ().CGColor;

			PromptButton.TouchUpInside += (object sender, EventArgs e) => {
				if(_onPromptTap != null){
					_onPromptTap();
				}
			};
		}

		private UIView InitView()
		{
			var nib = NSBundle.MainBundle.LoadNib("FullMapSearchBar", this, null);

			var view = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;

			return view;
		}
	}
}

