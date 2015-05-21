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
	[Register ("ProcedurePromptInformationController")]
	partial class ProcedurePromptInformationController
	{
		[Outlet]
		UIKit.UIButton CancelButton { get; set; }

		[Outlet]
		UIKit.UIButton ChangeButton { get; set; }

		[Outlet]
		UIKit.UIScrollView ContainerView { get; set; }

		[Outlet]
		UIKit.UIButton DateButton { get; set; }

		[Outlet]
		iOS.PSFloatingTextControl DateControl { get; set; }

		[Outlet]
		UIKit.UILabel ProcedureLabel { get; set; }

		[Outlet]
		iOS.PSProgressView ProgressView { get; set; }

		[Outlet]
		UIKit.UIButton SaveButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}

			if (ProgressView != null) {
				ProgressView.Dispose ();
				ProgressView = null;
			}

			if (DateButton != null) {
				DateButton.Dispose ();
				DateButton = null;
			}

			if (ProcedureLabel != null) {
				ProcedureLabel.Dispose ();
				ProcedureLabel = null;
			}

			if (DateControl != null) {
				DateControl.Dispose ();
				DateControl = null;
			}

			if (ChangeButton != null) {
				ChangeButton.Dispose ();
				ChangeButton = null;
			}

			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (SaveButton != null) {
				SaveButton.Dispose ();
				SaveButton = null;
			}
		}
	}
}
