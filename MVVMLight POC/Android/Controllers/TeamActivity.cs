using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Demo.Android.Utils;
using Demo.Shared;
using Demo.Shared.Helpers;
using Demo.Shared.Models;
using Java.Lang;
using Xamarin;
using Exception = System.Exception;
using Demo.Android.Helpers;
using GalaSoft.MvvmLight.Helpers;

namespace Demo.Android.Controllers
{
    [Activity]
	public class TeamActivity : ActivityBaseEx
    {
		private TeamViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Team);

			int seasonId = GlobalNavigation.GetAndRemoveParameter<int> (Intent);

			_viewModel = new TeamViewModel (seasonId);

			Title = _viewModel.Title;

			FindViewById<ListView>(Resource.Id.ListView).Adapter = new ObservableAdapter<Team>()
			{
				DataSource = _viewModel.Teams,
				GetTemplateDelegate = GetTeamTemplate
			};
        }

		private View GetTeamTemplate(int position, Team team, View convertView)
		{
			convertView = LayoutInflater.Inflate(global::Android.Resource.Layout.SimpleListItem2, null);

			var titleLabel = convertView.FindViewById<TextView>(global::Android.Resource.Id.Text1);
			var detailLabel = convertView.FindViewById<TextView>(global::Android.Resource.Id.Text2);

			titleLabel.Text = team.Name;
			detailLabel.Text = team.ShortName;
			titleLabel.SetTextColor(Color.ParseColor(Colors.ThemeTertiary));

			return convertView;
		}
    }
}