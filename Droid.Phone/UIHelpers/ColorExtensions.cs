using Android.Graphics;
using Shared.Common;

namespace Droid.Phone
{
    public static class ColorExtensions
    {
        public static Color ToDroidColor(this PSColor color)
        {
            return new Color(color.R, color.G, color.B);
        }
    }
}