using System;
using MapKit;
using CoreLocation;
using Shared.VM;
using UIKit;

namespace iOS
{
	public abstract class RecommendationAnnotation : MKAnnotation
	{
		protected CLLocationCoordinate2D _coordinate;
		protected MapMarkerViewModel _viewModel;

		public BaseRecommendationListItemViewModel RecommendationVm 
		{ 
			get
			{
				if (_viewModel != null)
					return _viewModel.RecommendationVm;
				else
					return null;
			}
		}

		public Action<BaseRecommendationListItemViewModel> OpenMenuAction{
			get {
				return _viewModel.OpenMenuAction;
			}
		}


		public RecommendationAnnotation (MapMarkerViewModel vm)
		{
			_viewModel = vm;
		}

		public override string Title {
			get {
				return _viewModel.Title;
			}
		}

		public override CLLocationCoordinate2D Coordinate {
			get {
				if(_viewModel.GeoLocation != null){
					_coordinate = new CLLocationCoordinate2D ((float)_viewModel.GeoLocation.Latitude, (float)_viewModel.GeoLocation.Longitude);
				}

				return _coordinate;
			}
		}


		public MapMarkerType MarkerType{
			get{
				return _viewModel.MarkerType;
			}
		}

		public abstract UIImage LargerImage { get; }
		public abstract UIImage Image { get; }

	}
}

