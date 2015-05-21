using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Droid.Controls
{
    public class PopupSpinnerEventArgs : EventArgs
    {
        public object ItemSelected { get; private set; }
        public int Index { get; private set; }

        public PopupSpinnerEventArgs(object item, int index)
        {
            ItemSelected = item;
            Index = index;
        }
    }

    public delegate void PopupSpinnerItemSelectedHandler(object sender, PopupSpinnerEventArgs args);

    public class PopupSpinner : AlertDialog.Builder,IDialogInterfaceOnCancelListener
    {
        public event EventHandler Cancelled;

        public event PopupSpinnerItemSelectedHandler OnItemSelected;

        private object[] _items; 

        public PopupSpinner(Context context, object[] items) : base(context)
        {
            _items = items;
            
            this.SetOnCancelListener(this);
            this.SetItems(_items.Select(i => i.ToString()).ToArray(), Handler);
        }

        private void Handler(object sender, DialogClickEventArgs dialogClickEventArgs)
        {
            if (OnItemSelected != null)
            {
                int i = dialogClickEventArgs.Which;

                object item = i >= 0 ? _items[i] : new object();

                OnItemSelected(this, new PopupSpinnerEventArgs(item,i));
            }
        }

        public void OnCancel(IDialogInterface dialog)
        {
            if (Cancelled != null)
            {
                Cancelled(this, new EventArgs());
            }
        }
    }
}