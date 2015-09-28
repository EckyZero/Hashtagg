
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
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Shared.VM;

namespace Droid.Phone
{
    [Activity(Label = "PostActivity", WindowSoftInputMode = SoftInput.AdjustResize)]			
    public class PostActivity : ActionBarBaseActivity
    {
        Toolbar _toolbar;

        BaseFragment _fragment;

        FrameLayout _container;

        PostViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GenericFragmentContainer);

            _toolbar = FindViewById<Toolbar>(Resource.Id.GenericContainerToolbar);
            _container = FindViewById<FrameLayout>(Resource.Id.GenericContainer);
            SetSupportActionBar(_toolbar);

            _viewModel = _navigationService.GetAndRemoveParameter(Intent) as PostViewModel;
            Title = "New Post";

            _fragment = new PostFragment(_viewModel);
            SupportFragmentManager.BeginTransaction().Add(_container.Id, _fragment, "POST").Commit();
        }

        //TODO Menu and action should be param
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.PostMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_cancel:
                    if (_fragment != null && _fragment.RightActionBarButton != null)
                        _fragment.RightActionBarButton();
                    break;
                case Android.Resource.Id.Home:
                    if (_fragment != null && _fragment.LeftActionBarButton != null)
                        _fragment.LeftActionBarButton();
                    else
                        OnBackPressed();
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            return true;
        }
    }
}

