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

namespace Droid.Phone.UIHelpers.ViewHolders
{
    public class CellDataHolder : Java.Lang.Object
    {
        public IViewHolder ViewHolder { get; set; }
        public string Tag { get; set; }
    }
}