
using System;

using Foundation;
using UIKit;

namespace iOS.Phone
{
	public class HomeTableControllerCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("HomeTableControllerCell");

		public HomeTableControllerCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

