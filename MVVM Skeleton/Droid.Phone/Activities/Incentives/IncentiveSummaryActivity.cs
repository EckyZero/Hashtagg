
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
using Droid.Activities;
using Android.Support.V7.Widget;
using Shared.Common;
using Shared.VM;
using System.Threading.Tasks;
using Droid.Phone.Fragments;

namespace Droid.Phone
{
	[Activity (Label = "IncentiveSummaryActivity")]			
	public class IncentiveSummaryActivity : ActionBarBaseActivity
	{
		private Toolbar _toolbar;

		protected override void OnCreate (Bundle bundle)
		{
			SetTheme(Resource.Style.ToolbarPageTheme);

			base.OnCreate (bundle);

			SetContentView(Resource.Layout.IncentiveSummaryPage);

			//could be incentives or recommendations
			var menuActionType = _navigationService.GetAndRemoveParameter<MenuActionType> (Intent);

			SetupToolbar (menuActionType);

			GetAndNavigateFragment(menuActionType);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Android.Resource.Id.Home)
			{
				GoBack ();
				return true;
			}
			return base.OnOptionsItemSelected(item);
		}
			
		private void SetupToolbar(MenuActionType menuActionType)
		{
			var toolbar = FindViewById<Toolbar>(Resource.Id.incentivesSummaryPage_Toolbar);
			SetSupportActionBar(toolbar);

			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(true);
			SupportActionBar.Title = MenuLookup.MenuActionTitles[menuActionType];
		}

		private async void GetAndNavigateFragment(MenuActionType menuActionType)
		{
			Android.Support.V4.App.Fragment next = await GetMenuFragment(menuActionType);
			SupportFragmentManager.BeginTransaction()
				.Replace(Resource.Id.incentivesSummaryPage_content, next)
				.Commit();
		}

		private async Task<Android.Support.V4.App.Fragment> GetMenuFragment(MenuActionType actionType)
		{
			if (actionType == MenuActionType.Incentives) {
				IncentiveSummaryViewModel incentiveVM = new IncentiveSummaryViewModel ();
				await incentiveVM.DidLoad ();
				return new IncentivesSummaryFragment (incentiveVM);
			}
			else{
				IncentiveSummaryViewModel recommendedVM = new IncentiveSummaryViewModel();
				await recommendedVM.DidLoad();
				return new IncentivesSummaryFragment(recommendedVM);
			}

			
		}
	}
}

