using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidHUD;
using Droid.UIHelpers;
using Shared.Common;
using System.Threading.Tasks;

namespace Droid
{
    public class HudService : BaseService, IHudService
    {
        public void Show(string message = "")
        {
            AndHUD.Shared.Show(_activity, message);
        }

        public void Dismiss()
        {
            AndHUD.Shared.Dismiss(_activity);
        }

		public async Task ShowFeedbackPopup(PSColor background, PSColor fontColor, string imagePath, string message, int timeout)
		{
			Drawable img = _activity.Resources.GetDrawable (SharedDrawableHelpers.GetSharedDrawableResourceIdViaReflection (imagePath));
			FeedbackPopup popup = new FeedbackPopup (background.ToDroidColor(), fontColor.ToDroidColor(), img, message);

			popup.Show (_activity.FragmentManager,null);

			await Task.Delay (timeout);

			popup.Dismiss ();
		}
    }
}