using System;
using MapKit;
using UIKit;
using Shared.VM;
using System.Linq;

namespace iOS.Phone
{
	public abstract class BaseRecommendationsFullMapDelegate : GenericRecommendationsMapDelegate
	{
		protected BaseRecommendationsFullMapDelegate ()
		{
		}

		private UIView _mainView;
		private UIView _redoSearchContainerView;
		private MKMapView _map;
		private bool _isCenteringMapAroundAnAnnotation;
		private NSLayoutConstraint _redoSearchContainerViewBottomConstraint;


		public BaseRecommendationsFullMapDelegate(MKMapView map, UIView redoSearchContainerView, UIView mainView)
		{
			_redoSearchContainerView = redoSearchContainerView;
			_map = map;
			_mainView = mainView;
		}

		public override void RegionChanged (MKMapView mapView, bool animated)
		{
			if(_redoSearchContainerView != null && _redoSearchContainerView.Hidden && !_isCenteringMapAroundAnAnnotation){
				_redoSearchContainerView.FadeIn ();
			}

			if(_isCenteringMapAroundAnAnnotation){
				_isCenteringMapAroundAnAnnotation = false;

				if (_redoSearchContainerView != null && !_redoSearchContainerView.Hidden) {
					_redoSearchContainerView.FadeOut ();
				}
			}
		}

		public RecommendationMapCell _cell { get; set;}

		public override void DidDeselectAnnotationView (MKMapView mapView, MKAnnotationView view)
		{
			IMKAnnotation annotation = view.Annotation;
			if (annotation is RecommendationAnnotation) {
				var docRecAnnotation = (RecommendationAnnotation) annotation;

				view.Image = docRecAnnotation.Image;

				if (_cell != null) {

					if(_redoSearchContainerViewBottomConstraint != null)
						_redoSearchContainerViewBottomConstraint.Constant = 0;

					_cell.RemoveFromSuperview ();	
				} 
			}
		}

		public abstract RecommendationMapCell GetCell();

		public override void DidSelectAnnotationView (MKMapView mapView, MKAnnotationView view)
		{
			IMKAnnotation annotation = view.Annotation;
			if (annotation is RecommendationAnnotation) {
				var docRecAnnotation = (RecommendationAnnotation) annotation;

				view.Image = docRecAnnotation.LargerImage;

				if(docRecAnnotation.MarkerType != Shared.VM.MapMarkerType.PreferredLocation){
					if (_cell == null) {
						_cell = GetCell();
					}

					_cell.ConfigureCell (docRecAnnotation.RecommendationVm, docRecAnnotation.OpenMenuAction);

					_cell.Frame = new CoreGraphics.CGRect (_mainView.Frame.X, _mainView.Frame.Bottom - _cell.Frame.Height, _cell.Frame.Width, _cell.Frame.Height);

					mapView.AddSubview (_cell);

					if(_redoSearchContainerViewBottomConstraint == null){
						var constraint = _mainView.Constraints.First(c => c.FirstAttribute == NSLayoutAttribute.Top && c.SecondItem == _redoSearchContainerView);
						_redoSearchContainerViewBottomConstraint = constraint;
					}

					_redoSearchContainerViewBottomConstraint.Constant = _cell.Frame.Height;

					//center map around selected pin
					_isCenteringMapAroundAnAnnotation = true;

					//calculate center coordinate based on annotation's position and how much 
					//real estate the doctor card is occupying
					nfloat percentageOccupiedByCard = _redoSearchContainerView.Frame.Height / mapView.Frame.Height;

					var latDelta = mapView.Region.Span.LatitudeDelta * percentageOccupiedByCard;

					UIView.Animate(0.5, ()=> {
						mapView.CenterCoordinate = new CoreLocation.CLLocationCoordinate2D(docRecAnnotation.Coordinate.Latitude - latDelta, docRecAnnotation.Coordinate.Longitude);
					});
				}
			}
		}

	}
}

