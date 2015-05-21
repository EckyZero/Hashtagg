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
	[Register ("HealthProPromptController")]
	partial class HealthProPromptController
	{
		[Outlet]
		UIKit.UIButton CallButton { get; set; }

		[Outlet]
		UIKit.UILabel HeaderContent { get; set; }

		[Outlet]
		UIKit.UIImageView HealthProImage { get; set; }

		[Outlet]
		UIKit.UILabel HealthProName { get; set; }

		[Outlet]
		UIKit.UIView HealthProNameBorderBox { get; set; }

		[Outlet]
		UIKit.UILabel QuestionsTableHeaderLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (HeaderContent != null) {
				HeaderContent.Dispose ();
				HeaderContent = null;
			}

			if (HealthProImage != null) {
				HealthProImage.Dispose ();
				HealthProImage = null;
			}

			if (HealthProName != null) {
				HealthProName.Dispose ();
				HealthProName = null;
			}

			if (HealthProNameBorderBox != null) {
				HealthProNameBorderBox.Dispose ();
				HealthProNameBorderBox = null;
			}

			if (QuestionsTableHeaderLabel != null) {
				QuestionsTableHeaderLabel.Dispose ();
				QuestionsTableHeaderLabel = null;
			}
		}
	}
}
