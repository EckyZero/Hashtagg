using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace iOS.Phone
{
	partial class BasicMenuCell : UITableViewCell
	{
		public BasicMenuCell (IntPtr handle) : base (handle){}

		public void Configure(string iconName, string text)
		{
			iconImageView.Image = UIImage.FromBundle ("ic_money_green");
			titleLabel.Text = text;
		}
	}
}
