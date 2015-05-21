using System;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.App;
using Droid.Phone.UIHelpers;
using Shared.Common;
using Android.Graphics;
using Droid.UIHelpers;

namespace Droid.Phone
{
    public class HamburgerMenuAdapter : BaseAdapter<ExtendedMenuItem>
    {
        private readonly Activity _context;

        private readonly List<ExtendedMenuItem> _menuWithSections;

        public HamburgerMenuAdapter(Activity context, List<ExtendedMenuItem> menuItems)
            : base()
		{
		    _context = context;
            _menuWithSections = menuItems;
		}

        public override int ViewTypeCount
        {
            get { return 2; }
        }

        public override int GetItemViewType(int position)
        {
            return _menuWithSections[position].IsSectionHeader ? 0 : 1;
        }

        public override ExtendedMenuItem this[int index] {
			get {
                if (index < _menuWithSections.Count && index >= 0)
                {
                    return _menuWithSections[index];
				} else {
					throw new IndexOutOfRangeException ("The index provided is not a valid one.");
				}
			}
		}

		public override long GetItemId (int position)
		{
		    return (long) position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
		    ExtendedMenuItem item = _menuWithSections[position];

		    if (item.IsSectionHeader)
		    {
                convertView = convertView != null && convertView.Id == Resource.Layout.HamburgerMenuSection ? convertView :
                    _context.LayoutInflater.Inflate(Resource.Layout.HamburgerMenuSection, null);
 
                var title = convertView.FindViewById<TextView>(Resource.Id.hamburgerMenuSectionTitle);
                title.Text = item.Title;
		    }
		    else
            {
                convertView = convertView != null && convertView.Id == Resource.Layout.HamburgerMenuItem ? convertView :
                    _context.LayoutInflater.Inflate(Resource.Layout.HamburgerMenuItem, null);

                var title = convertView.FindViewById<TextView>(Resource.Id.hamburgerMenuTitle);
                title.Text = item.Title;

                var imageView = convertView.FindViewById<ImageView>(Resource.Id.hamburgerMenuIcon);

                if (item.IsSelected)
                {
                    if (item.SelectedImage != null)
                    {
                        imageView.SetImageDrawable(
                            _context.Resources.GetDrawable(
                                DrawableHelpers.GetDrawableResourceIdViaReflection(item.SelectedImage)));
                    }
                    else
                    {
                        imageView.SetImageDrawable(null);
                    }
                    convertView.SetBackgroundColor(Color.ParseColor("#6fa9dc"));
                }
                else
                {
                    if(item.NormalImage != null)
                    {
                        imageView.SetImageDrawable(_context.Resources.GetDrawable(DrawableHelpers.GetDrawableResourceIdViaReflection(item.NormalImage)));
                    }
                    else
                    {
                        imageView.SetImageDrawable(null);

                    }
                    convertView.SetBackgroundColor(Color.ParseColor("#555555"));
                }
            }

            return convertView;
		}
        
	    public override int Count {
			get {
                return _menuWithSections.Count;
			}
		}

	}
}

