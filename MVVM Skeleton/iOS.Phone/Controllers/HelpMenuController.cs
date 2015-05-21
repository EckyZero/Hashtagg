using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using System.Threading.Tasks;

namespace iOS.Phone
{
	partial class HelpMenuController : UIViewController
	{
		public HelpMenuController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// TODO: Use actual objects instead of strings
			var strings = new string[] {
				"an estimate for a procedure",
				"finding a doctor or facility",
				"my current doctors",
				"a medical bill received",
				"my insurance information",
				"saving and incentives",
				"cheaper medications",
				"contacting my health pro"
			};
				
			tableView.Source = new HelpMenuSource (strings);

			tableView.ReloadData ();
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			View.BackgroundColor = UIColor.White.ColorWithAlpha(0.0f);

			if(Device.OS < 8)
			{
				topConstraint.Constant = View.Frame.Size.Height;

				View.SetNeedsLayout ();
				View.LayoutIfNeeded ();

				topConstraint.Constant = 20;
			}
			UIView.Animate (
				0.25f, 
				0,
				UIViewAnimationOptions.CurveEaseOut,
				() => {
					View.SetNeedsLayout ();
					View.LayoutIfNeeded ();
					View.BackgroundColor = UIColor.White.ColorWithAlpha (0.7f);
				},
				null
			);

			await Task.Delay (200);
			ResizeTableView ();
		}

		partial void OnDismissButtonTapped (UIButton sender)
		{
			DismissViewController(true, null);
		}

		private void ResizeTableView ()
		{
			if (tableView.Frame.Height >= tableView.ContentSize.Height) 
			{
				var difference = tableView.Frame.Size.Height - tableView.ContentSize.Height;

				bottomConstraint.Constant = bottomConstraint.Constant + difference;
				tableView.ScrollEnabled = false;
			}
		}
	}

	public class HelpMenuSource : UITableViewSource
	{
		private string[] _strings;

		public HelpMenuSource(string[] strings)
		{
			_strings = strings;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return _strings.Length;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("BasicMenuCell") as BasicMenuCell;

			// TODO: Pass in an actual object, not just a string
			cell.Configure ("", _strings [indexPath.Row]);

			return cell;
		}
	}
}
