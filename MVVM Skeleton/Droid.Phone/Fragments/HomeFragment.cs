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
using Fragment = Android.Support.V4.App.Fragment;
using Shared.VM;
using Shared.Common;
using Microsoft.Practices.Unity;

namespace Droid.Phone.Fragments
{
    public class HomeFragment : BaseFragment
    {
		private HomeViewModel _viewModel;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
			_viewModel = IocContainer.GetContainer().Resolve<IHomeViewModel> () as HomeViewModel;

            return inflater.Inflate(Resource.Layout.Home, container, false);
        }

		public override void OnResume ()
		{
			base.OnResume ();

			_viewModel.DidLoad ();
		}
    }
}