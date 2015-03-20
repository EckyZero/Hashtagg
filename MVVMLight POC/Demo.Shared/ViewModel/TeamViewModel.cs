using System;
using Demo.Shared.Models;
using Demo.Shared.Helpers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ObjectBuilder2;

namespace Demo.Shared
{
	public class TeamViewModel :DemoViewModelBase
	{
		public override string Title {get { return Strings.AppConstants.Teams; }}

		public ObservableCollection<Team> Teams { get; private set; }

		public TeamViewModel (int seasonID)
		{
			InitData (seasonID);
		}

		private async void InitData(int seasonID)
		{
			try
			{
				Teams = new ObservableCollection<Team>();
				List<Team> teams = await TeamBL.GetTeamsWithSeasonId(seasonID ,false);
				teams.ForEach(t=>Teams.Add(t));
			}
			catch (BaseException exception)
			{
				Insights.Report(exception, ReportSeverity.Error);
				var dialog = ServiceLocator.Current.GetInstance<IDialogService> ();
				await dialog.ShowMessageBox(exception.Message, Strings.AppConstants.TryAgainLater);
			}
		}
	}
}

