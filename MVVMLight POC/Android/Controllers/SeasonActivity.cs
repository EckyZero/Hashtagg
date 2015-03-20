using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Demo.Android.Utils;
using Demo.Shared;
using Demo.Shared.Helpers;
using Demo.Shared.Models;
using Java.Lang;
using Xamarin;
using Demo.Android.Helpers;
using GalaSoft.MvvmLight.Helpers;

namespace Demo.Android.Controllers
{
    [Activity]
    public class SeasonActivity : ActivityBaseEx
	{
		private SeasonViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			_viewModel = new SeasonViewModel ();

            SetContentView(Resource.Layout.Season);

			Title = _viewModel.Title;

			ObservableAdapter<Season> adapter = new ObservableAdapter<Season>()
			{
				DataSource = _viewModel.Seasons,
				GetTemplateDelegate = GetSeasonTemplate
			};

			var listView = FindViewById<ListView>(Resource.Id.ListView);

			listView.ItemClick += (sender, e) => _viewModel.ShowTeamsCommand.Execute(_viewModel.Seasons[e.Position]);;
			listView.Adapter = adapter;
        }

		private View GetSeasonTemplate(int position, Season season, View convertView)
		{
			convertView = LayoutInflater.Inflate(global::Android.Resource.Layout.SimpleListItem2, null);

			var titleLabel = convertView.FindViewById<TextView>(global::Android.Resource.Id.Text1);
			var detailLabel = convertView.FindViewById<TextView>(global::Android.Resource.Id.Text2);

			titleLabel.Text = season.Caption;
			detailLabel.Text = season.League;

			return convertView;
		}
    }
}