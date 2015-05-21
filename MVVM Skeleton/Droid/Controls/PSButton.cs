using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace Droid.Controls
{
    public class PSButton : Button
    {
        private const string Tag = "Button";

        protected PSButton(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public PSButton(Context context)
            : this(context, null)
        {
        }

        public PSButton(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public PSButton(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            TypedArray attributes = context.ObtainStyledAttributes(attrs,
                    Resource.Styleable.CustomFonts);
            var customFont = attributes.GetString(Resource.Styleable.CustomFonts_customFont);

            SetCustomFont(customFont);
            
            attributes.Recycle();
        }

        public void SetCustomFont(string asset)
        {
            try
            {
                Typeface tf = Typeface.CreateFromAsset(Context.Assets, asset);
                var tfStyle = TypefaceStyle.Normal;
                if (null != Typeface)
                    tfStyle = Typeface.Style;
                SetTypeface(tf, tfStyle);
            }
            catch
            {
            }
        }
    }
}