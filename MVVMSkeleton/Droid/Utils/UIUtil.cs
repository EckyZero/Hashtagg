using System;
using Android.Views;
using Android.App;
using Android.Util;

namespace Droid
{
	public static class UIUtil
	{
		public static int CountChildOccurances<T>(ViewGroup viewGroup)
		{
			var numChildren = viewGroup.ChildCount;
			int count = 0;
			for(int i =0; i< numChildren; i++)
			{
				var childView = viewGroup.GetChildAt (i);
				if(childView.GetType().IsAssignableFrom(typeof(T))){
					count++;
				}

			}
			return count;
		}

		public static int DpiToPixel(double dpi)
		{
			var dp = Application.Context.Resources.DisplayMetrics;
			return (int) (dpi * (dp.Xdpi / (int)DisplayMetricsDensity.Default));
		}

	}
}

