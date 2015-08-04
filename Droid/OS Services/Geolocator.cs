using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Common;

namespace Droid
{
    public class Geolocator : BaseService, IGeolocator
    {
        private TaskCompletionSource<Android.Locations.Location> _locationRecieved;

        private LocationManager Manager
        {
            get
            {
                return _activity.GetSystemService(Context.LocationService) as LocationManager;
            }
        }

        private GeoLocation _curLocation = null;

        public async Task<GeoLocation> GetCurrentLocation()
        {
            if (_curLocation == null)
            {
                _locationRecieved = new TaskCompletionSource<Android.Locations.Location>();

                Criteria locationCriteria = new Criteria();

                locationCriteria.Accuracy = Accuracy.Coarse;
                locationCriteria.PowerRequirement = Power.Low;

                string locationProvider = Manager.GetBestProvider(locationCriteria, true);

                if (locationProvider != null && Manager.IsProviderEnabled(locationProvider))
                {
                    Manager.RequestLocationUpdates(locationProvider, 2000, 1, Activity);

                    Android.Locations.Location loc = await _locationRecieved.Task.TimeoutAfter(1000);

                    if (loc != null)
                    {
                       _curLocation = new GeoLocation(loc.Latitude, loc.Longitude);
                    }
                }
            }

            return _curLocation;
        }

        public void LocationChanged(Android.Locations.Location location)
        {
            Manager.RemoveUpdates(Activity);

            if (_locationRecieved != null)
            {
                _locationRecieved.TrySetResult(location);
            }
        }

		public bool IsDeniedFromUsingGeoLocation ()
		{
            return !Manager.IsProviderEnabled(LocationManager.GpsProvider) && !Manager.IsProviderEnabled(LocationManager.NetworkProvider);
		}

    }
}