using System;
using System.Threading.Tasks;

namespace Shared.Common
{
	public interface IGeolocator
	{
		Task<GeoLocation> GetCurrentLocation ();
		bool IsDeniedFromUsingGeoLocation ();
	}
}

