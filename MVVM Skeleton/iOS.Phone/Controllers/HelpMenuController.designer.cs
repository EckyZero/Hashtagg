// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOS.Phone
{
	[Register ("HelpMenuController")]
	partial class HelpMenuController
	{
		[Outlet]
		UIKit.NSLayoutConstraint bottomConstraint { get; set; }

		[Outlet]
		UIKit.UITableView tableView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint topConstraint { get; set; }

		[Action ("OnDismissButtonTapped:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void OnDismissButtonTapped (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
		}
	}
}
