
using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;

namespace iOS.Phone
{
	[Register("PSListCell"), DesignTimeVisible(true)]
	public partial class PSListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PSListCell", null);
		public static readonly NSString Key = new NSString ("PSListCell");

		public event EventHandler TouchUpInside
		{
			add { ActionButton.TouchUpInside += value; }
			remove { ActionButton.TouchUpInside -= value; }
		}

		public string HeaderText { 
			get { return HeaderLabel.Text; }
			set { HeaderLabel.Text = value; }
		}

		public string ContentText { 
			get { return ContentLabel.Text; }
			set { ContentLabel.Text = value; }
		}

		public string FooterText { 
			get { return FooterLabel.Text; }
			set { FooterLabel.Text = value; }
		}
		public string SecondFooterText { 
			get { return SecondFooterLabel.Text; }
			set { SecondFooterLabel.Text = value; }
		}
		public PSListCell (IntPtr handle) : base(handle) {}

		public PSListCell () : base ()
		{
			var nibs = NSBundle.MainBundle.LoadNib("PSListCell", this, null);
			var view = Runtime.GetNSObject(nibs.ValueAt(0)) as UIView;
			Frame = view.Frame;

			AddSubview (view);
			ActionButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
		}

		public static PSListCell Create ()
		{
			return (PSListCell)Nib.Instantiate (null, null) [0];
		}
	}
}

