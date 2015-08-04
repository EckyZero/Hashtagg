using System;

namespace Shared.Common
{
	public interface IMapService
	{
		bool GoToLocation (GeoLocation location);

		bool GoToAddress (string address, string city, string state, string country);

		bool DirectionsToAddress (string address, string city, string state, string country);
	}
}

