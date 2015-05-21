// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOS.Phone
{
	partial class SurveyDoctorPromptController
	{
		[Outlet]
		UIKit.UIButton _iDontHaveOneButton { get; set; }

		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		iOS.PSProgressView ProgressView { get; set; }

		[Outlet]
		iOS.PSPromptButton PromptButton { get; set; }

		[Outlet]
		UIKit.UIButton SkipThisPartButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (_iDontHaveOneButton != null) {
				_iDontHaveOneButton.Dispose ();
				_iDontHaveOneButton = null;
			}

			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}

			if (PromptButton != null) {
				PromptButton.Dispose ();
				PromptButton = null;
			}

			if (SkipThisPartButton != null) {
				SkipThisPartButton.Dispose ();
				SkipThisPartButton = null;
			}

			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
		}
	}
}
