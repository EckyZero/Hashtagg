
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.VM;

namespace iOS.Phone
{
	public abstract class BaseAttestationController : UIViewController
	{
		protected BaseAttestationViewModel _viewModel { get; set;}

		protected BaseAttestationController (IntPtr handle) : base(handle) {}
		protected BaseAttestationController () : base () {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			GenericAttestationController.Create (_viewModel, this);
		}

		public void OnRequestSubmitPage (object sender, EventArgs args)
		{
			NavigationController.PopToRootViewController(true);
		}
	}
}

