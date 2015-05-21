using System;
using UIKit;
using Shared.VM;
using System.Collections.Generic;
using MapKit;
using Shared.Common;

namespace iOS.Phone
{
	public abstract class BaseRecommendationsMapController : UIViewController
	{
		public GenericRecommendationsMapController ChildController { get; set; }

		protected MKMapView Map {get {return ChildController.Map;}}

		protected BaseRecommendationsMapViewModel _viewModel;

		public abstract void OnRequestActionPage (BaseRecommendationListItemViewModel baseViewModel);
		public abstract void OnRequestLocationLookupPage ();
		public abstract void OnRequestMapRefresh();

		public BaseRecommendationsMapController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ChildController = GenericRecommendationsMapController.Create (_viewModel, this);

			var backButton = new UIBarButtonItem (UIImage.FromBundle ("BackButton.png"),UIBarButtonItemStyle.Plain, OnBackButtonTapped);
			NavigationItem.SetLeftBarButtonItem (backButton,false);
		}

		protected void OnBackButtonTapped(object sender, EventArgs eventArgs)
		{
			NavigationController.PopViewController (true);
		}

		public abstract void HydrateMap();
	}
}

