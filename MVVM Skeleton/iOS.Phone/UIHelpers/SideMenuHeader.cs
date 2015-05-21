
using System;
using System.Drawing;
using Foundation;
using UIKit;
using ObjCRuntime;
using CoreGraphics;

namespace iOS.Phone
{
	[Register ("SideMenuHeader")]
	public partial class SideMenuHeader : UIView
	{

		public string Label {get{ return HeaderLabel.Text;} set{ HeaderLabel.Text = value;}}
		public SideMenuHeader (IntPtr handle) : base (handle)
		{
		}
		public SideMenuHeader () : base()
		{	
			try{
				var arr = NSBundle.MainBundle.LoadNib("SideMenuHeader", this, null);
				var v = Runtime.GetNSObject(arr.ValueAt(0)) as UIView;
				v.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
				AddSubview(v);
				HeaderLabel.Text = "";
			}catch(Exception e){
				var error = e.Message;
			}
		}
			
	}
}

