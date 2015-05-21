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
	[Register ("GenericRecommendationsMapController")]
	partial class GenericRecommendationsMapController
	{
		[Outlet]
		MapKit.MKMapView MapView { get; set; }

		[Outlet]
		UIKit.UIButton RedoSearchButton { get; set; }

		[Outlet]
		UIKit.UIView RedoSearchContainerView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MapView != null) {
				MapView.Dispose ();
				MapView = null;
			}

			if (RedoSearchButton != null) {
				RedoSearchButton.Dispose ();
				RedoSearchButton = null;
			}

			if (RedoSearchContainerView != null) {
				RedoSearchContainerView.Dispose ();
				RedoSearchContainerView = null;
			}
		}
	}
}
