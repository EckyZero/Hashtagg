using BigTed;
using Shared.Common;

namespace iOS
{
	public class HudService : IHudService
	{
		public HudService ()
		{

		}

        public void Show(string message = "")
        {
            BTProgressHUD.Show(message, -1, ProgressHUD.MaskType.Black);
        }

		public void Dismiss() {
			BTProgressHUD.Dismiss ();
		}
	}
}

