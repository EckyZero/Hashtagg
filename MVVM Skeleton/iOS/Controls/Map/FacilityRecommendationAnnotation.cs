using System;
using Shared.VM;
using UIKit;

namespace iOS
{
	public class FacilityRecommendationAnnotation : RecommendationAnnotation
	{
		public FacilityRecommendationAnnotation (MapMarkerViewModel vm) : base(vm)
		{

		}

		private UIImage _largerImage;
		public override UIImage LargerImage{
			get {
				if(_viewModel.MarkerType == MapMarkerType.PreferredLocation){
					return Image;
				}

				if(_largerImage == null){
					_largerImage = UIImage.FromFile (string.Format ("map/Large_{0}", GetImagePathNoDir()));
				}

				return _largerImage;
			}
		}

		private UIImage _smallerImage;
		public override UIImage Image {
			get{

				if(_smallerImage == null){
					_smallerImage = UIImage.FromFile (string.Format("map/{0}", GetImagePathNoDir()));
				}

				return _smallerImage;
			}
		}


		private string GetImagePathNoDir()
		{
			string imageFilename = null;


			if(_viewModel.MarkerType == MapMarkerType.PreferredLocation){
				imageFilename = "PreferredLocation.png";
			}
			else if(_viewModel.MarkerType == MapMarkerType.Premier){
				imageFilename = "Premierflag.png";
			}
			else if(_viewModel.MarkerType == MapMarkerType.Recommended){
				imageFilename = "RecomFlag.png";
			}
			else if(_viewModel.MarkerType == MapMarkerType.Other){

				if(_viewModel.Rating == MapMarkerState.Negative){
					imageFilename = "Orangetriangle.png";
				}
				else if(_viewModel.Rating == MapMarkerState.Neutral){
					imageFilename = "greydot.png";
				}
				else if(_viewModel.Rating == MapMarkerState.Positive){
					imageFilename = "Greendot.png";
				}

			}
			else if(_viewModel.MarkerType == MapMarkerType.Favorite){

				if(_viewModel.Rating == MapMarkerState.Negative){
					imageFilename = string.Format ("yf_orange{0}.png", _viewModel.Rank);
				}
				else if(_viewModel.Rating == MapMarkerState.Neutral){
					imageFilename = string.Format ("yf_gray{0}.png", _viewModel.Rank);
				}
				else if(_viewModel.Rating == MapMarkerState.Positive){
					imageFilename = string.Format ("yf_green{0}.png", _viewModel.Rank);
				}

			}

			if(imageFilename == null){
				return null;
			}

			return imageFilename;
		}
	}
}

