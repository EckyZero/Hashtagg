using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Demo.Android.Utils;
using Demo.Shared.Helpers;
using Xamarin;
using Demo.Shared;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Demo.Android.Helpers;

namespace Demo.Android.Controllers
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LoginActivity : ActivityBaseEx
	{
		private LoginViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			_viewModel = App.Locator.Login;

			Insights.Initialize(Strings.Settings.XamarinInsightsApiKey, ApplicationContext);
            
            SetContentView(Resource.Layout.Login);

			Title = _viewModel.Title;

			InitBindings ();
		}

        private void InitBindings()
        {
			EditText emailText = FindViewById<EditText> (Resource.Id.EmailField);

			Binding<string,string> emailBinding = emailText.SetBinding (() => emailText.Text);

			FindViewById<Button>(Resource.Id.LoginButton).SetCommand("Click", _viewModel.LoginCommand, emailBinding);
        }
    }
}