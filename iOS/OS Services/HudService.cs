using System;
using Shared.Common;
using BigTed;
using UIKit;
using CoreGraphics;
using System.Threading.Tasks;

namespace iOS
{
    public class HudService : BaseService, IHudService
	{
		public HudService ()
		{
			
		}

        public void Show(string message = "")
		{
			BTProgressHUD.ForceiOS6LookAndFeel = true;
			BTProgressHUD.Show(message, -1, ProgressHUD.MaskType.Black);
        }

		public void Dismiss() {
			BTProgressHUD.Dismiss ();
		}
	}
}

