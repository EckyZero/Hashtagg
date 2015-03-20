using System.Threading.Tasks;
using CompassMobile.Shared.Common;

namespace Shared.Common
{
	public interface IGeolocator
	{
		Task<GeoLocation> GetCurrentLocation ();
	}
}

