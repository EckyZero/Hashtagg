using Android.Locations;

namespace Droid.Activities
{
    public interface IBaseActivity : ILocationListener
    {
        string ActivityKey { get; }

        string NextPageKey { get; set; }

        void GoBack();

        void Dismiss();
    }
}