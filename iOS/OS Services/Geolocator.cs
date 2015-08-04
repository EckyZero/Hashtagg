using System;
using Shared.Common;
using CoreLocation;
using System.Threading.Tasks;
using UIKit;

namespace iOS
{
    public class Geolocator : BaseService, IGeolocator
	{
		private bool _is8orGreater;

		private TaskCompletionSource<CLLocation> _locationRecieved;

		private TaskCompletionSource<object> _authorizationChanged;

		private CLLocationManager _locMgr;

		public Geolocator()
		{
			_is8orGreater = UIDevice.CurrentDevice.CheckSystemVersion (8, 0);

			_locMgr = new CLLocationManager ();

			_locMgr.DesiredAccuracy = 1;

			_locMgr.AuthorizationChanged += HandleAuthorizationChanged;

			_locMgr.LocationsUpdated += HandleLocationsUpdated;

		}

		public bool IsDeniedFromUsingGeoLocation()
		{
			return CLLocationManager.Status == CLAuthorizationStatus.Denied;
		}

		public async Task<GeoLocation> GetCurrentLocation(){
		
			// must manually request authorization post iOS 8
			if (_is8orGreater) {
				if (CLLocationManager.Status == CLAuthorizationStatus.NotDetermined) {
					_authorizationChanged = new TaskCompletionSource<object> ();

					_locMgr.RequestWhenInUseAuthorization ();

					await _authorizationChanged.Task;
				}
			}

			if (CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse ||
			    CLLocationManager.Status == CLAuthorizationStatus.AuthorizedAlways ||
			    CLLocationManager.Status == CLAuthorizationStatus.Authorized) {

				_locationRecieved = new TaskCompletionSource<CLLocation> ();

				_locMgr.StartUpdatingLocation ();

				CLLocation loc = await _locationRecieved.Task.TimeoutAfter(1000);

				return loc == null ? null : new GeoLocation (loc.Coordinate.Latitude, loc.Coordinate.Longitude);
			}

			// will get prompted at this point in pre iOS 8
			else if (!_is8orGreater && CLLocationManager.Status == CLAuthorizationStatus.NotDetermined) {
				_authorizationChanged = new TaskCompletionSource<object> ();

				_locMgr.StartUpdatingLocation ();

				await _authorizationChanged.Task;

				if (CLLocationManager.Status == CLAuthorizationStatus.AuthorizedWhenInUse ||
				    CLLocationManager.Status == CLAuthorizationStatus.AuthorizedAlways ||
				    CLLocationManager.Status == CLAuthorizationStatus.Authorized) {

					_locationRecieved = new TaskCompletionSource<CLLocation> ();

					CLLocation loc = await _locationRecieved.Task.TimeoutAfter(1000);

					return loc == null ? null : new GeoLocation (loc.Coordinate.Latitude, loc.Coordinate.Longitude);
				}
			}

			return null;
		}

		private void HandleLocationsUpdated (object sender, CLLocationsUpdatedEventArgs e)
		{
			_locMgr.StopUpdatingLocation();

			if (_locationRecieved != null) {
				CLLocation loc = e.Locations == null ? null : e.Locations [e.Locations.Length - 1];
				_locationRecieved.TrySetResult (loc);
			}
		}

		private void HandleAuthorizationChanged (object sender, CLAuthorizationChangedEventArgs e)
		{
			if (_authorizationChanged != null) {
				_authorizationChanged.TrySetResult (null);
			}
		}


	}
}

