using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompassMobile.Shared.Common
{
    public class GeoLocation
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public GeoLocation(double lat, double lon)
        {
            Latitude = (float)lat;
            Longitude = (float)lon;
        }

        public GeoLocation(float lat, float lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }
}
