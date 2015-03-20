using Android.App;
using Droid.Activities;

namespace Droid
{
    public abstract class BaseService
    {
        protected Activity _activity;

        protected IBaseActivity Activity
        {
            get { return _activity as IBaseActivity; }
        }

        internal void RegisterActivity(Activity activity)
        {
            _activity = activity;
        }
    }
}