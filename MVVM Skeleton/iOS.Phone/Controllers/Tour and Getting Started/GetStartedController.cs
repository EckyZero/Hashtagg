using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
{
	partial class GetStartedController : BaseGetStartedController
	{
		public GetStartedController (IntPtr handle) : base (handle)
		{
			Index = 0;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			_loginButton.SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (_loginButton.Bounds), UIControlState.Normal);
			_loginButton.SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (_loginButton.Bounds), UIControlState.Disabled);
			_registerButton.SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (_registerButton.Bounds), UIControlState.Normal);
			_registerButton.SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (_registerButton.Bounds), UIControlState.Disabled);
		}

		private void InitBindings ()
		{
			_registerButton.SetCommand ("TouchUpInside", Application.VMStore.TourVM.GetStartedCommand);

			_loginButton.SetCommand ("TouchUpInside", Application.VMStore.TourVM.LoginCommand);
		}
	}
}
