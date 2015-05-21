
using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace iOS.Phone
{
	public partial class PSExpandedListCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PSExpandedListCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PSExpandedListCell");

		public PSExpandedListCell (IntPtr handle) : base (handle)
		{
		}

		public static PSExpandedListCell Create ()
		{
			return (PSExpandedListCell)Nib.Instantiate (null, null) [0];
		}
	}
}

