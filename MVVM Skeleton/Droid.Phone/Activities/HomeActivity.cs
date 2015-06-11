
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
	public class HomeActivity : Activity
	{
		#region Private Variables

		private Button _twitterButton;
		private HomeViewModel _viewModel = new HomeViewModel();

		#endregion

		#region Methods

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.Home);

			_twitterButton = FindViewById<Button> (Resource.Id.TwitterButton);

			InitUI ();
			InitBindings ();
		}

		private void InitUI ()
		{
			
		}

		private void InitBindings ()
		{
			_twitterButton.SetCommand("Click", _viewModel.RefreshCommand);
		}

		#endregion
	}
}

