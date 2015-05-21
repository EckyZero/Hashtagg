
using System;
using System.Drawing;

using Foundation;
using UIKit;
using ObjCRuntime;

namespace iOS.Phone
{
	public partial class PSLightListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PSLightListCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PSLightListCell");

		public string ContentText { get{ return ContentLabel.Text; } set{ ContentLabel.Text = value; }}
		public string FooterText { get{ return FooterLabel.Text; } set{ FooterLabel.Text = value; }}

		public event EventHandler TouchUpInside
		{
			add { ActionButton.TouchUpInside += value; }
			remove { ActionButton.TouchUpInside -= value; }
		}

		public PSLightListCell (IntPtr handle) : base (handle)
		{
		}

		public PSLightListCell () : base ()
		{
			var nibs = NSBundle.MainBundle.LoadNib("PSLightListCell", this, null);
			var view = Runtime.GetNSObject(nibs.ValueAt(0)) as UIView;
			Frame = view.Frame;

			AddSubview (view);
		}


		public static PSLightListCell Create ()
		{
			return (PSLightListCell)Nib.Instantiate (null, null) [0];
		}
	}
}

