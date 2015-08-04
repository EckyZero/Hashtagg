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
using System.Threading.Tasks;
using Shared.Common;

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
    }
}