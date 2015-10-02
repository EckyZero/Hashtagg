using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using ActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Droid;
using Shared.VM;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Shared.Common;
using Android.App;

namespace Droid.Phone
{
    [Activity (Label = "HomeActivity",  MainLauncher = false)]          
    public class HamburgerMenuActivity : ActionBarBaseActivity
    {
        private ActionBarDrawerToggle _drawerToggle;

        private Toolbar _toolbar;

        private DrawerLayout _drawerLayout;

        private Button _drawerButton;

        private ImageView _drawerTitle;

        private TextView _drawerTopText;

        private ListView _drawerList;

        TextView _drawerSubtitle;

        private bool _init;

        HomeViewModel _homeViewModel;
        MenuViewModel _menuViewModel;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            // Must change theme to support the toolbar
            //SetTheme(Resource.Style.MyTheme);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HamburgerLayout);

            _toolbar = FindViewById<Toolbar>(Resource.Id.hamburgerMenu_toolbar);
            _toolbar.ViewTreeObserver.GlobalLayout += ViewTreeObserverOnGlobalLayout;
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(true);


            GetAndRemoveParameters();
            _menuViewModel = new MenuViewModel(_homeViewModel);
            await _menuViewModel.DidLoad();

            OnCreateFragmentSetup ();
            SetupMenu();
            _menuViewModel.PropertyChanged += OnMenuViewModelPropertyChanged;
        }

        void OnMenuViewModelPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "PrimaryButtonText":
                    _drawerButton.Text = _menuViewModel.PrimaryButtonText;
                    break;
                case "Title":
                    _drawerTopText.Text = _menuViewModel.Title;
                    _drawerTitle.Visibility = _menuViewModel.ShowTitleLogo ? ViewStates.Visible : ViewStates.Invisible;
                    break;
                case "ShowSubtitle":
                    _drawerSubtitle.Visibility = _menuViewModel.ShowSubtitle ? ViewStates.Visible : ViewStates.Gone;
                    break;
            } 
        }

        private void ViewTreeObserverOnGlobalLayout(object sender, EventArgs eventArgs)
        {
            if (_init)
                return;
            _init = true;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.HomeMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_post:
                    _homeViewModel.PostCommand.Execute(null);
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
            }
            return true;
        }

        protected virtual void OnCreateFragmentSetup()
        {
            GetAndNavigateFragment();
        }

        void GetAndRemoveParameters()
        {
            _homeViewModel = _navigationService.GetAndRemoveParameter(Intent) as HomeViewModel;
        }

        protected override void OnNewIntent (Intent intent)
        {
            base.OnNewIntent (intent);
        }

        private async void GetAndNavigateFragment()
        {
            SupportActionBar.Title = string.Empty;

            Fragment next = await GetMenuFragment();

            if (next != null)
            {
                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.hamburgerMenu_content, next)
                    .Commit();
            }
        }

        private void SetupMenu()
        {

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.hamburgerMenu_layout);
            _drawerList = FindViewById<ListView>(Resource.Id.hamburgerMenu_menu);
            _drawerTitle = FindViewById<ImageView>(Resource.Id.DrawerAppName);
            _drawerTopText = FindViewById<TextView>(Resource.Id.DrawerTopText);
            _drawerButton = FindViewById<Button>(Resource.Id.DrawerFooterButton);
            _drawerSubtitle = FindViewById<TextView>(Resource.Id.DrawerSubTitle);

            _drawerSubtitle.Visibility = _menuViewModel.ShowSubtitle ? ViewStates.Visible : ViewStates.Gone;

            _drawerButton.SetCommand("Click", _menuViewModel.PrimaryCommand);
            _drawerButton.Text = _menuViewModel.PrimaryButtonText;

            _drawerTitle.Visibility = _menuViewModel.ShowTitleLogo ? ViewStates.Visible : ViewStates.Invisible;
            _drawerTopText.Text = _menuViewModel.Title;

            _drawerList.Adapter = new ObservableAdapter<IListItem>
            {
                DataSource= _menuViewModel.ItemViewModels,
                GetTemplateDelegate = AdapterHelpers.SetupMenuCell
            };

            _drawerToggle = new ActionBarDrawerToggle(this,_drawerLayout,_toolbar,Resource.String.abc_action_bar_home_description,Resource.String.abc_toolbar_collapse_description);
            _drawerLayout.SetDrawerListener(_drawerToggle);
            _drawerLayout.DrawerClosed += RaiseOnDrawerClosed;
            _drawerToggle.SyncState();
        }

        private void RaiseOnDrawerClosed(object sender, DrawerLayout.DrawerClosedEventArgs drawerClosedEventArgs)
        {
            _homeViewModel.DidAppear();
        }

        private void DrawerListOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            ItemSelected(itemClickEventArgs.Position);
        }

        private async void ItemSelected(int position)
        {
            
        }


        private async Task<Android.Support.V4.App.Fragment> GetMenuFragment()
        {
            var homeFrag = new HomeFragment (_homeViewModel);

            return homeFrag;
        }

        //Dissable Hardware Back
        public override void OnBackPressed() { }
    }

    public class MenuAdapter : ObservableAdapter<IListItem>
    {
        private LayoutInflater _layoutInflater;

        public MenuAdapter(LayoutInflater inflater)
        {
            _layoutInflater = inflater;
        }

       
    }
}