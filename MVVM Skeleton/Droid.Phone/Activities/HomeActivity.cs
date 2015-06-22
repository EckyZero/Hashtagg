﻿using System;
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

namespace Droid.Phone
{
	[Activity (Label = "HomeActivity",  MainLauncher = true)]			
	public class HamburgerMenuActivity : ActionBarBaseActivity
	{
		private ActionBarDrawerToggle _drawerToggle;

		private Toolbar _toolbar;

		private DrawerLayout _drawerLayout;

		private ListView _drawerList;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			// Must change theme to support the toolbar
			//SetTheme(Resource.Style.MyTheme);
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.HamburgerLayout);

			_toolbar = FindViewById<Toolbar>(Resource.Id.hamburgerMenu_toolbar);
			SetSupportActionBar(_toolbar);
			SupportActionBar.SetDisplayShowTitleEnabled(true);

			OnCreateFragmentSetup ();

			SetupMenu();
		}

		protected virtual void OnCreateFragmentSetup()
		{
			GetAndNavigateFragment();
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

			//_drawerList.AddHeaderView(LayoutInflater.Inflate(Resource.Layout.Header,_drawerList,false));

			//_drawerList.Adapter = adapter;
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
			var homeFrag = new HomeFragment (new HomeViewModel ());
			return homeFrag;
		}

		//Dissable Hardware Back
		public override void OnBackPressed() { }
	}
}