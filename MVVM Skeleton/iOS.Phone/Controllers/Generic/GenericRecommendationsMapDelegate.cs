using System;
using MapKit;
using Foundation;
using UIKit;
using CoreGraphics;

namespace iOS.Phone
{
	public class GenericRecommendationsMapDelegate : MKMapViewDelegate
	{
		private static readonly string DocRecViewId = "DocRecView";

		public GenericRecommendationsMapDelegate()
		{

		}
			
		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			MKAnnotationView anView;

			if (annotation is MKUserLocation)
				return null; 

			if(annotation is RecommendationAnnotation){

				var docRecAnnotation = (RecommendationAnnotation) annotation;

				anView = mapView.DequeueReusableAnnotation (DocRecViewId);

				if (anView == null)
					anView = new MKAnnotationView (annotation, DocRecViewId);

				anView.Image = docRecAnnotation.Image;

				return anView;
			}


			return null;
		}

	}
}

