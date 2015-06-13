
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
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace Droid.Phone
{
	[Activity (Label = "HomeActivity",  MainLauncher = true)]			
	public class HomeActivity : ActionBarBaseActivity
	{
		#region Private Variables

		HomeViewModel _viewModel = new HomeViewModel();
		Button _refreshButton;
		Button _twitterButton;
		Button _facebookButton;

		#endregion

		#region Methods

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.Home);

			_refreshButton = FindViewById<Button> (Resource.Id.RefreshButton);
			_twitterButton = FindViewById<Button> (Resource.Id.TwitterButton);
			_facebookButton = FindViewById<Button> (Resource.Id.FacebookButton);

			InitUI ();
			InitBindings ();
		}

		private void InitUI ()
		{
			
		}

		private void InitBindings ()
		{
			_refreshButton.SetCommand("Click", _viewModel.RefreshCommand);
			_twitterButton.SetCommand("Click", _viewModel.TwitterCommand);
			_facebookButton.SetCommand ("Click", _viewModel.FacebookCommand);
		}

		#endregion
	}
}

