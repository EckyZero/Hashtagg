using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Common;

namespace Droid.UIHelpers
{
    public static class ColorExtensions
    {
        public static Color ToDroidColor(this PSColor color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }
    }
}