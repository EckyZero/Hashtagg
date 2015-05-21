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
using Droid.Activities;
using Droid.Phone.Fragments;
using Shared.BL;
using Shared.Common;
using Microsoft.Practices.Unity;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Droid.Phone.UIHelpers;
using Shared.VM;
using ActionBarDrawerToggle = Android.Support.V7.App.ActionBarDrawerToggle;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using Droid.UIHelpers;

namespace Droid.Phone.Activities
{
    [Activity(Label = "HamburgerMenuActivity")]
    public class HamburgerMenuActivity : ActionBarBaseActivity
    {
        private ActionBarDrawerToggle _drawerToggle;

        private IMenuItemsBL _menuItemsBL;
        
        private List<ExtendedMenuItem> _menuWithSections = new List<ExtendedMenuItem>();

        private Toolbar _toolbar;
        
        private DrawerLayout _drawerLayout;
        
        private ListView _drawerList;
        
        private int _selectedIndex = 1;

        public MenuActionType _menuActionType;
        private int _startingTab;
        private HamburgerMenuParameters _parameters;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Must change theme to support the toolbar
            SetTheme(Resource.Style.ToolbarPageTheme);
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HamburgerMenuLayout);

            _toolbar = FindViewById<Toolbar>(Resource.Id.hamburgerMenu_toolbar);
            SetSupportActionBar(_toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(true);
        
            _menuItemsBL = IocContainer.GetContainer().Resolve <IMenuItemsBL> ();

            _parameters = _navigationService.GetAndRemoveParameter<HamburgerMenuParameters>(Intent) ??
                          new HamburgerMenuParameters();
            _menuActionType = _parameters.Action;
            _startingTab = _parameters.Tab;
            SetupMenu();

            GetAndNavigateFragment();

        }

        private async void GetAndNavigateFragment()
        {
            SupportActionBar.Title = MenuLookup.MenuActionTitles[_menuActionType];
            Fragment next = await GetMenuFragment(_menuActionType);
            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.hamburgerMenu_content, next)
                .Commit();
        }

        private void SetupMenu()
        {
            List<MenuItem> items = _menuItemsBL.GetAll(MenuLocation.SideMenu) ?? new List<MenuItem>();

            BuildMenuWithSections(items);

            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.hamburgerMenu_layout);
            _drawerList = FindViewById<ListView>(Resource.Id.hamburgerMenu_menu);

            _drawerList.AddHeaderView(LayoutInflater.Inflate(Resource.Layout.Header,_drawerList,false));

            var adapter = new HamburgerMenuAdapter(this, _menuWithSections);
            _drawerList.Adapter = adapter;
            _drawerList.ItemClick += DrawerListOnItemClick;

            _drawerToggle = new ActionBarDrawerToggle(this,_drawerLayout,_toolbar,Resource.String.abc_action_bar_home_description,Resource.String.abc_toolbar_collapse_description);
            _drawerLayout.SetDrawerListener(_drawerToggle);
            _drawerToggle.SyncState();
        }

        private void BuildMenuWithSections(List<MenuItem> items)
        {
            foreach (MenuSection section in Enum.GetValues(typeof (MenuSection)))
            {
                // home has no section header so dont add it
                if (section != MenuSection.Home)
                {
                    MenuItem sectionHeader = new MenuItem(MenuActionType.None, MenuLocation.SideMenu, section, false);
                    _menuWithSections.Add(new ExtendedMenuItem(sectionHeader, true));
                }
                foreach (MenuItem item in items)
                {
                    if (item.Section == section)
                    {
                        _menuWithSections.Add(new ExtendedMenuItem(item, false));
                    }
                }
            }
        }

        private void DrawerListOnItemClick(object sender, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            ItemSelected(itemClickEventArgs.Position);
        }

        private async void ItemSelected(int position)
        {
            var firstVisible = _drawerList.FirstVisiblePosition;
            var relativeSelectedPosition = position - firstVisible;
            var relativeOldSelectedPosition = _selectedIndex - firstVisible;
            var totalVisible = _drawerList.ChildCount;
            ExtendedMenuItem oldItem = _menuWithSections[_selectedIndex-1];
            ExtendedMenuItem newItem = _menuWithSections[position-1];

            if (newItem.IsSectionHeader)
            {
                return;
            }

            // restyle old item if it was not a section header
            if (!oldItem.IsSectionHeader)
            {
                oldItem.IsSelected = false;
                if (relativeOldSelectedPosition >= 0 && relativeOldSelectedPosition < totalVisible)
                {
                    View oldSelected = _drawerList.GetChildAt(relativeOldSelectedPosition);
                    oldSelected.SetBackgroundColor(Color.ParseColor("#555555"));
                    ImageView oldImage = oldSelected.FindViewById<ImageView>(Resource.Id.hamburgerMenuIcon);
                    if (oldItem.NormalImage != null)
                    {
                        oldImage.SetImageDrawable(
                            Resources.GetDrawable(DrawableHelpers.GetDrawableResourceIdViaReflection(oldItem.NormalImage)));
                    }
                    else
                    {
                        oldImage.SetImageDrawable(null);
                    }
                }
            }

            // restyle, navigate, and close if new item is not a section header
            if (!newItem.IsSectionHeader)
            {
                _selectedIndex = position;

                newItem.IsSelected = true;

                if (relativeSelectedPosition >= 0)
                {
                    View newSelected = _drawerList.GetChildAt(relativeSelectedPosition);
                    newSelected.SetBackgroundColor(Color.ParseColor("#6fa9dc"));
                    ImageView newImage = newSelected.FindViewById<ImageView>(Resource.Id.hamburgerMenuIcon);

                    if (newItem.SelectedImage != null)
                    {
                        newImage.SetImageDrawable(
                            Resources.GetDrawable(
                                DrawableHelpers.GetDrawableResourceIdViaReflection(newItem.SelectedImage)));
                    }
                    else
                    {
                        newImage.SetImageDrawable(null);
                    }
                }


                _drawerLayout.CloseDrawer(_drawerList);
				
                if (newItem.IsNavigable)
                {
                    SupportActionBar.Title = (newItem.Title);
                    SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.hamburgerMenu_content, await GetMenuFragment(newItem.Action))
                        .Commit();
                    _drawerList.SetItemChecked(_selectedIndex, true);
                }
                else
                {
                    ExecuteAction(newItem.Action);
                }
            }
        }

        private void ExecuteAction(MenuActionType actionType)
        {
            switch (actionType)
            {
                case MenuActionType.Logout:
                    var settingsViewModel = new SettingsViewModel();
                    settingsViewModel.RequestLogin += settingsViewModel_RequestLogin;
                    settingsViewModel.LogoutCommand.Execute(null);
                    break;
                default:
                    //If the action type does not exist in switch, throw exception.
                    var e = new Exception("Attempted to execute action for not allowed actionType");
                    _logger.Log(e);
                    throw e;
            }
        }

        void settingsViewModel_RequestLogin(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            intent.AddFlags(ActivityFlags.NewTask|ActivityFlags.ClearTask);
            StartActivity(intent);
        }

        private async Task<Android.Support.V4.App.Fragment> GetMenuFragment(MenuActionType actionType)
        {
            switch (actionType)
            {
                case MenuActionType.Home:
                    return new HomeFragment();

                case MenuActionType.Incentives:
                    IncentiveSummaryViewModel incentiveVM = new IncentiveSummaryViewModel();
                    await incentiveVM.DidLoad();
                    return new IncentivesSummaryFragment(incentiveVM);

                case MenuActionType.Recommendations:
                    IncentiveSummaryViewModel recommendedVM = new IncentiveSummaryViewModel();
                    await recommendedVM.DidLoad();
                    return new IncentivesSummaryFragment(recommendedVM);
                case MenuActionType.HealthDetails:
                    var nextViewModel = new HealthDetailsViewModel(){InitialTab = _startingTab};
                    return new MyHealthDetailsFragment(nextViewModel);
                case MenuActionType.MyPrescriptions:
                    var medicationPromptViewModel = new MedicationPromptViewModel();
                    return new MyMedicationsFragment(medicationPromptViewModel);
                case MenuActionType.Settings:
                    SettingsDependentListViewModel settingsVm = new SettingsDependentListViewModel();
                    await settingsVm.Subscribe();
                    return new SettingsFragment(settingsVm);
                default:
                    return new HomeFragment();
            }
            return new HomeFragment();
        }


        //Dissable Hardware Back
        public override void OnBackPressed() { }
    }
}