using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iOS.Phone
{
	partial class ToolTipNavigationController : UINavigationController
	{
		public string TipKey { get; set; }

		public ToolTipNavigationController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var controller = ChildViewControllers [0] as ToolTipController;

			controller.TipKey = TipKey ?? null;
		}
	}
}
