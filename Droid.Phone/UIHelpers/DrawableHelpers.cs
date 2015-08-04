using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;

namespace Droid.Phone
{
    public static class DrawableHelpers
    {
        public static int GetDrawableResourceIdViaReflection(string fileName, string extension = ".png")
        {
            var result = 0;
            var drawablesType = typeof(Resource.Drawable); 
            var resourceIdField = drawablesType.GetField((fileName.Replace(extension,"")), BindingFlags.Public | BindingFlags.Static);
            if (resourceIdField != null)
            {
                result = (int)resourceIdField.GetValue(null);
            }
            return result;
        }
    }
}