
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Shared.Common;
using Android.Graphics.Drawables;
using Droid.UIHelpers;
using Android.Graphics;
using Droid.Controls;

namespace Droid
{
	[Activity (Label = "FeedbackPopup",Theme = "@android:style/Theme.NoTitleBar")]
	public class FeedbackPopup : DialogFragment
	{
		private Drawable _img;
		private string _message;
		private GradientDrawable _background;
		private Color _fontColor;

		public FeedbackPopup(Color backgroundColor, Color fontColor, Drawable image, string message)
		{
			_background = new GradientDrawable ();
			_background.SetColor (backgroundColor.ToArgb ());
			_background.SetCornerRadius (20.0f);

			_fontColor = fontColor;
			_img = image;
			_message = message;
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View feedbackPopup = inflater.Inflate(Resource.Layout.FeedbackPopup, container, false);

			RelativeLayout popupLayout = feedbackPopup.FindViewById<RelativeLayout>(Resource.Id.feedbackPopupLayout);

			ImageView popupIcon = feedbackPopup.FindViewById<ImageView> (Resource.Id.feedbackPopupImage);

			PSTextView popupText = feedbackPopup.FindViewById<PSTextView> (Resource.Id.feedbackPopupText);

			popupLayout.Background = _background;

			popupIcon.SetImageDrawable (_img);

			popupText.SetTextColor (_fontColor);

			popupText.Text = _message;

			return feedbackPopup;

		}

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			DialogFragmentStyle style = DialogFragmentStyle.NoTitle | DialogFragmentStyle.NoFrame;
			SetStyle (style, Android.Resource.Style.ThemeDialog);
		}

		public override void OnResume ()
		{
			base.OnResume ();

			float scale = Resources.DisplayMetrics.Density;
			int pixels = (int) (175 * scale + 0.5f);

			Dialog.Window.SetLayout (pixels, pixels);

			Dialog.Window.SetDimAmount (0);

			Dialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
		}
	}
}

