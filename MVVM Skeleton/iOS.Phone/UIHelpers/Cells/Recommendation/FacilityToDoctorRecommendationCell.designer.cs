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
	[Register ("FacilityToDoctorRecommendationCell")]
	partial class FacilityToDoctorRecommendationCell
	{
		[Outlet]
		UIKit.UIButton RecommendButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (RecommendButton != null) {
				RecommendButton.Dispose ();
				RecommendButton = null;
			}
		}
	}
}
