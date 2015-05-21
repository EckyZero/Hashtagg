using System;
using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace iOS.Phone
{
	[Register("AddressView")]
	public partial class AddressView : UIView
	{

		public string Title {
			get{ return TitleLabel.Text; }
			set { TitleLabel.Text = value; }
		}

		public string Subtitle {
			get{ return SubtitleLabel.Text; }
			set { SubtitleLabel.Text = value; }
		}
			
		public Action OnTapAction {get; set;}

		public AddressView (IntPtr handle) : base (handle)
		{
		}

		public AddressView (CGRect frame) : base(frame)
		{	
			try{
				var arr = NSBundle.MainBundle.LoadNib("AddressView", this, null);
				var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
				v.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
				AddSubview(v);

				NextButton.TouchUpInside += (object sender, EventArgs e) => {
					if(OnTapAction != null){
						OnTapAction();
					}
				};

			}catch(Exception e){
				var error = e.Message;
			}
		}
	}
}

