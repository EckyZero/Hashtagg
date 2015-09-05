using System;
using Shared.Common;

namespace UnitTests
{
    public class MockMapService : IMapService
    {
        #region IMapService implementation

        public bool GoToLocation(GeoLocation location)
        {
            return true;
        }

        public bool GoToAddress(string address, string city, string state, string country)
        {
            return true;
        }

        public bool DirectionsToAddress(string address, string city, string state, string country)
        {
            return true;
        }

        #endregion
    }
}

