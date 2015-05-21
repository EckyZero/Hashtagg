
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.Common;
using CoreGraphics;

namespace iOS.Phone
{
	public partial class SideMenuCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("SideMenuCell");

	    public string Title
	    {
	        get
	        {
	            return TitleLabel.Text;
	        }
	        set
	        {
	            TitleLabel.Text = value;
	        }
	    }

	    public string Image
	    {
	        set
	        {
	            if (value != null)
	            {
	                IconImageView.Image = UIImage.FromFile(value);
	            }
	            else
	            {
	                IconImageView.Image = new UIImage();
	            }
	        }
	    }

        public MenuItem Item { get; set; }

		public SideMenuCell (IntPtr handle) : base (handle)
		{
		}

		public override void SetSelected (bool selected, bool animated)
		{
			base.SetSelected (selected, animated);

			if (selected) {
				ContentView.BackgroundColor = SharedColors.CompassBlue.ToUIColor ();
				Image = Item.SelectedImage;
			} else {
				ContentView.BackgroundColor = SharedColors.Black.ToUIColor ();
				Image = Item.NormalImage;
			}
		}
	}
}

