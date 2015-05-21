using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Droid.Controls
{
    class StyledSearchView : SearchView
    {
        private const string Tag = "SearchView";
                protected StyledSearchView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public StyledSearchView(Context context)
            : this(context, null)
        {
        }
        public StyledSearchView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            TypedArray attributes = context.ObtainStyledAttributes(attrs,Resource.Styleable.CustomFonts);
            var customFont = attributes.GetString(Resource.Styleable.CustomFonts_customFont);

            // change hint color
            TextView searchTextView = FindViewById<TextView>(context.Resources.GetIdentifier("android:id/search_src_text", null, null));
            searchTextView.SetTextColor(Color.White);
            searchTextView.SetHintTextColor(Color.ParseColor("#B3FFFFFF"));
            attributes.Recycle();
        }

        public StyledSearchView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context,attrs,defStyleAttr)
        {

        }
    }
}