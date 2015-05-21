using Android.Content;
using Shared.Common;

namespace Droid
{
    public class BrowserService : BaseService, IBrowserService
    {
        public void OpenUrl(string url)
        {
            var uri = Android.Net.Uri.Parse(url);
            var intent = new Intent(Intent.ActionView, uri);
            _activity.StartActivity(intent); 
        }
    }
}