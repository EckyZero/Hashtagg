
using System;

using Foundation;
using UIKit;

namespace iOS.Phone
{
	public partial class NoImageHomeCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("NoImageHomeCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("NoImageHomeCell");

		public NoImageHomeCell (IntPtr handle) : base (handle)
		{
		}

		public static NoImageHomeCell Create ()
		{
			return (NoImageHomeCell)Nib.Instantiate (null, null) [0];
		}
	}
}

