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

        private TextView _drawerTitle;

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
                    _drawerTitle.Text = _menuViewModel.Title;
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
            _drawerTitle = FindViewById<TextView>(Resource.Id.DrawerAppName);
            _drawerButton = FindViewById<Button>(Resource.Id.DrawerFooterButton);
            _drawerSubtitle = FindViewById<TextView>(Resource.Id.DrawerSubTitle);

            _drawerSubtitle.Visibility = _menuViewModel.ShowSubtitle ? ViewStates.Visible : ViewStates.Gone;

            _drawerButton.SetCommand("Click", _menuViewModel.PrimaryCommand);
            _drawerButton.Text = _menuViewModel.PrimaryButtonText;

            _drawerTitle.Text = _menuViewModel.Title;

            _drawerList.Adapter = new MenuAdapter(LayoutInflater)
            {
                DataSource=_menuViewModel.ItemViewModels,
            };
                //_drawerList.ItemClick += DrawerListOnItemClick;

            _drawerToggle = new ActionBarDrawerToggle(this,_drawerLayout,_toolbar,Resource.String.abc_action_bar_home_description,Resource.String.abc_toolbar_collapse_description);
            _drawerLayout.SetDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();
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

        private ObservableRangeCollection<IListItem> _viewModels;

        public MenuAdapter(LayoutInflater inflater)
        {
            _layoutInflater = inflater;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var vm = DataSource[position] as BaseMenuItemViewModel;
            MenuViewHolder viewHolder = null;

            if (convertView != null)
            {
                var oldViewHolder = convertView.Tag as MenuViewHolder;
                if (oldViewHolder != null && oldViewHolder.HolderType != vm.GetType())
                {
                    vm.PropertyChanged -= oldViewHolder.PropertyChanger;
                    convertView.Click -= oldViewHolder.OnSelect;
                    convertView = null;
                }
            }

            if (convertView == null)
            {
                convertView = _layoutInflater.Inflate(Resource.Layout.DrawerCell, parent, false);
                convertView.Tag = new MenuViewHolder
                {
                    Image = convertView.FindViewById<ImageView>(Resource.Id.DrawerCellImage),
                    Title = convertView.FindViewById<TextView>(Resource.Id.DrawerCellText),
                    Subtitle = convertView.FindViewById<TextView>(Resource.Id.DrawerCellFooterText),
                    HolderType = vm.GetType()
                };
            }

            viewHolder = convertView.Tag as MenuViewHolder;

            var image = DrawableHelpers.GetDrawableResourceIdViaReflection(vm.ImageName);
            viewHolder.Image.SetImageResource(image);
            viewHolder.Title.Text = vm.Title;
            viewHolder.Subtitle.Text = vm.Subtitle;

            vm.PropertyChanged -= viewHolder.PropertyChanger;

            viewHolder.PropertyChanger = (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                var senderVm = sender as BaseMenuItemViewModel;
                switch (e.PropertyName)
                {
                    case "Title":
                        viewHolder.Title.Text = senderVm.Title;
                        break;
                    case "Subtitle":
                        viewHolder.Subtitle.Text = senderVm.Subtitle;
                        break;
                    case "ImageName":
                        viewHolder.Image.SetImageResource(
                            DrawableHelpers.GetDrawableResourceIdViaReflection(senderVm.ImageName));
                        break;
                }
            };

            vm.PropertyChanged += viewHolder.PropertyChanger;
             
            convertView.Click -= viewHolder.OnSelect;

            viewHolder.OnSelect = (sender, eventargs) => vm.Selected();

            convertView.Tag = viewHolder;

            if (vm.UserInteractionEnabled)
            {
                convertView.Click += viewHolder.OnSelect;
            }

            return convertView;
        }

        private class MenuViewHolder: Java.Lang.Object
        {
            public ImageView Image {get;set;}
            public TextView Title {get;set;}
            public TextView Subtitle { get; set; }
            public System.ComponentModel.PropertyChangedEventHandler PropertyChanger { get; set; }
            public EventHandler OnSelect {get;set;}
            public Type HolderType { get; set; }
        }

    }
}