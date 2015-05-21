
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.VM;
using MapKit;
using Shared.Common;
using CoreLocation;
using GalaSoft.MvvmLight.Helpers;
using System.ComponentModel;

namespace iOS.Phone
{
	public partial class GenericRecommendationsMapController : UIViewController
	{
		BaseRecommendationsMapViewModel _viewModel;
		BaseRecommendationsMapController _containerController;


		public MKMapView Map 
		{ 
			get
			{
				return MapView;
			} 
		}

		public UIView RedoSearchView{
			get{
				return RedoSearchContainerView;
			}
		}

		public FullMapSearchBar SearchBar { get; private set; }


		public GenericRecommendationsMapController (IntPtr handle) : base(handle)
		{
		}

		public static GenericRecommendationsMapController Create(BaseRecommendationsMapViewModel viewModel, BaseRecommendationsMapController containerController)
		{
			var storyboard = UIStoryboard.FromName ("RecommendationsStoryboard", null);
			var childController = storyboard.InstantiateViewController ("GenericRecommendationsMapController") as GenericRecommendationsMapController;

			childController._viewModel = viewModel;
			childController._containerController = containerController;
			containerController.ChildController = childController;

			containerController.AddChildViewController (childController);
			containerController.View.AddSubview (childController.View);

			childController.SearchBar = new FullMapSearchBar (viewModel.RequestLocationLookupPage);
			childController.ConfigureSearchBar ();


			return childController;
		}
			
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

        public override void ViewDidLoad ()
        {
			base.ViewDidLoad ();

			RedoSearchButton.Layer.BorderColor = SharedColors.Tan2.ToUIColor().CGColor;
			RedoSearchButton.Layer.BorderWidth = 1;

            RedoSearchButton.TouchUpInside += TouchUpInside;
        }
 
        private async void TouchUpInside(object sender, EventArgs e)
        {
            //TODO: go back and refresh results from SAL
            RedoSearchContainerView.FadeOut();
            CLLocation mapCenter = new CLLocation(Map.CenterCoordinate.Latitude, Map.CenterCoordinate.Longitude);

            //Note: this finds the distance from the center of the map to bottom edge of the screen ("radius"). 
            // Taking latitude instead of longitude because that would get results to better fill the screen vertically.
            var distanceInMeters = mapCenter.DistanceFrom(new CLLocation(mapCenter.Coordinate.Latitude + Map.Region.Span.LatitudeDelta / 2.0, Map.CenterCoordinate.Longitude));
            var distanceInMiles = MapsHelper.ConvertMetersToMiles(distanceInMeters);

            var mapCenterShared = new GeoLocation(mapCenter.Coordinate.Latitude, mapCenter.Coordinate.Longitude);

            _viewModel.PreferredLocation = new LocationLookupCellViewModel { Location = new Location { Address = new Address { Geolocation = mapCenterShared } } };

            //TODO: do we need the distance so we can search within the zoomed in radius?
            await _viewModel.GetRecommendation(mapCenterShared, distanceInMiles);
        }

		private void ConfigureSearchBar()
		{
			ParentViewController.NavigationItem.TitleView = SearchBar;

			if(_viewModel.PreferredLocation != null)
				SearchBar.Text = _viewModel.PreferredLocation.FullAddress;

			_viewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				if(e.PropertyName.Equals("PreferredLocation")) {
					if(_viewModel.PreferredLocation != null){
						SearchBar.Text = _viewModel.PreferredLocation.FullAddress;
					}
				}
			};

			var listButton = UIButton.FromType (UIButtonType.Custom);
			listButton.SetImage (UIImage.FromFile ("map/ListIcon.png"), UIControlState.Normal);
			listButton.SetTitle ("List",UIControlState.Normal);
			listButton.Font = UIFont.FromName ("CenturyGothic", 14);


			nfloat spacing = 7;
			listButton.TitleEdgeInsets = new UIEdgeInsets (0, spacing, 0, 0);
			listButton.SizeToFit ();

			listButton.Frame = new CoreGraphics.CGRect (listButton.Frame.X, listButton.Frame.Y, listButton.Frame.Width + spacing, listButton.Frame.Height);


			listButton.TouchUpInside += (object sender, EventArgs e) => {

				if(_viewModel.RequestPreviousPage != null && _viewModel.PreferredLocation != null){
					_viewModel.RequestPreviousPage(_viewModel.Recommendation, _viewModel.PreferredLocation.Location);
				}

				NavigationController.PopViewController(true);
			};

			ParentViewController.NavigationItem.RightBarButtonItem = 
				new UIBarButtonItem (listButton);

		}

		private void ShowRedoSearchView()
		{
			if (RedoSearchContainerView.Hidden)
				RedoSearchContainerView.FadeIn ();
		}

		public void MapLoadedWithMarkers()
		{
			RedoSearchContainerView.Hidden = true;
		}
	}
}

