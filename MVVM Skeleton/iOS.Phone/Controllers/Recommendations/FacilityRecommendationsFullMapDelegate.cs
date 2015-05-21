using System;
using MapKit;
using UIKit;

namespace iOS.Phone
{
	public class FacilityRecommendationsFullMapDelegate : BaseRecommendationsFullMapDelegate
	{
		private FacilityRecommendationsFullMapDelegate ()
		{
		}

		public FacilityRecommendationsFullMapDelegate(MKMapView map, UIView redoSearchContainerView, UIView mainView) : base(map,redoSearchContainerView, mainView)
		{

		}

		public override RecommendationMapCell GetCell(){
			return new FacilityBioCell();
		}
	}
}

