using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using CompassMobile.Shared.Common;
using Shared.Common;

namespace Droid
{
    public class Geolocator : BaseService, IGeolocator
    {
        private TaskCompletionSource<Location> _locationRecieved;

        private LocationManager _locMgr;

        private GeoLocation _curLocation = null;

        public async Task<GeoLocation> GetCurrentLocation()
        {
            if (_curLocation == null)
            {
                _locMgr = _activity.GetSystemService(Context.LocationService) as LocationManager;

                _locationRecieved = new TaskCompletionSource<Location>();

                Criteria locationCriteria = new Criteria();

                locationCriteria.Accuracy = Accuracy.Coarse;
                locationCriteria.PowerRequirement = Power.Low;

                string locationProvider = _locMgr.GetBestProvider(locationCriteria, true);

                if (locationProvider != null && _locMgr.IsProviderEnabled(locationProvider))
                {
                    _locMgr.RequestLocationUpdates(locationProvider, 2000, 1, Activity);

                    Location loc = await _locationRecieved.Task.TimeoutAfter(1000);

                    if (loc != null)
                    {
                       _curLocation = new GeoLocation(loc.Latitude, loc.Longitude);
                    }
                }
            }

            return _curLocation;
        }

        public void LocationChanged(Location location)
        {
             _locMgr.RemoveUpdates(Activity);

            if (_locationRecieved != null)
            {
                _locationRecieved.TrySetResult(location);
            }
        }
    }
}