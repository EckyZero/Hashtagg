using Android.App;

namespace Demo.Android.Utils
{
    public static class Alert
    {
        public static void Show(Activity context, string title, string message)
        {
            var builder = new AlertDialog.Builder(context);

            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.SetCancelable(false);
            builder.SetPositiveButton("OK", delegate { builder.Dispose(); });

            builder.Show();
        }
    }
}