using System;
using MapKit;
using UIKit;
using System.Linq;
using Shared.VM;

namespace iOS.Phone
{
	public class DoctorRecommendationsFullMapDelegate : BaseRecommendationsFullMapDelegate
	{
		private DoctorRecommendationsFullMapDelegate(){}

		public DoctorRecommendationsFullMapDelegate(MKMapView map, UIView redoSearchContainerView, UIView mainView) : base(map,redoSearchContainerView, mainView)
		{

		}

		public override RecommendationMapCell GetCell(){
			return new DoctorBioCell();
		}
	}
}

