using System;
using Android.App;
using Shared.Common;

namespace Droid
{
    public class HudService : BaseService, IHudService
    {
        private ProgressDialog _progress;

        public void Show(string message = "")
        {
            _progress = new ProgressDialog(_activity);
            _progress.Indeterminate = true;
            _progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            if (!String.IsNullOrWhiteSpace(message))
            {
                _progress.SetMessage(message);
            }
            else
            {
                _progress.SetMessage("Processing Request");
            }
            _progress.SetCancelable(false);
            _progress.Show();
        }

        public void Dismiss()
        {
            _progress.Hide();
        }
    }
}