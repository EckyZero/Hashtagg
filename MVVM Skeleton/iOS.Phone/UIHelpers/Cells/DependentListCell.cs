
using System;

using Foundation;
using UIKit;
using ObjCRuntime;

namespace iOS.Phone
{
	[Register ("DependentListCell")]
	public partial class DependentListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("DependentListCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("DependentListCell");

		public event EventHandler TouchUpInside
		{
			add { ActionButton.TouchUpInside += value; }
			remove { ActionButton.TouchUpInside -= value; }
		}

		public string HeaderText { 
			get { return Header.Text; }
			set { Header.Text = value; }
		}

		public string BodyText { 
			get { return Body.Text; }
			set { Body.Text = value; }
		}
		public string ContentFieldOneText{
			get { return ContentFieldOne.Text; }
			set { ContentFieldOne.Text = value; }
		}

		public string ContentFieldTwoText{
			get { return ContentFieldTwo.Text; }
			set { ContentFieldTwo.Text = value; }
		}
		public string ContentFieldThreeText{
			get { return ContentFieldThree.Text; }
			set { ContentFieldThree.Text = value; }
		}

		public DependentListCell (IntPtr handle) : base (handle)
		{
		}

		public DependentListCell () : base ()
		{
			var nibs = NSBundle.MainBundle.LoadNib("DependentListCell", this, null);
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

