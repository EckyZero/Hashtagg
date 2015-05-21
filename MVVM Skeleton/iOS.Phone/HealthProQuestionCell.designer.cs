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
	[Register ("HealthProQuestionCell")]
	partial class HealthProQuestionCell
	{
		[Outlet]
		UIKit.UIView ClickableCell { get; set; }

		[Outlet]
		UIKit.UILabel QuestionLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ClickableCell != null) {
				ClickableCell.Dispose ();
				ClickableCell = null;
			}

			if (QuestionLabel != null) {
				QuestionLabel.Dispose ();
				QuestionLabel = null;
			}
		}
	}
}
