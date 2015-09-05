using System;
using Shared.Common;

namespace UnitTests
{
    public class MockGeolocatorService : IGeolocator
    {
        #region IGeolocator implementation

        public System.Threading.Tasks.Task<GeoLocation> GetCurrentLocation()
        {
            return null;
        }

        public bool IsDeniedFromUsingGeoLocation()
        {
            return true;
        }

        #endregion
    }
}

