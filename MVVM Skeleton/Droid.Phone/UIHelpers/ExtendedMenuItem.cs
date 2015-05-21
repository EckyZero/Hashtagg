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
using Shared.Common;

namespace Droid.Phone.UIHelpers
{
    public class ExtendedMenuItem : MenuItem
    {
        public bool IsSectionHeader { get; private set; }

        public bool IsSelected { get; set; }

        public override string Title
        {
            get
            {
                return IsSectionHeader ? MenuLookup.SectionNames[Section] : base.Title;
            }
        }

        public ExtendedMenuItem(MenuItem item, bool isSectionHeader):
            base(item.Action, item.Location, item.Section, item.IsNavigable)
        {
            IsSectionHeader = isSectionHeader;
            IsSelected = false;
        }
    }
}