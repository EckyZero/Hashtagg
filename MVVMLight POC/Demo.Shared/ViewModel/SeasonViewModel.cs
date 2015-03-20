using System;
using Demo.Shared.Helpers;
using System.Collections.ObjectModel;
using Demo.Shared.Models;
using Xamarin;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using Demo.Shared.ViewModel;

namespace Demo.Shared
{
	public class SeasonViewModel : DemoViewModelBase
	{
		public override string Title { get { return Strings.AppConstants.SoccerSeasons; } }

		public RelayCommand<Season> ShowTeamsCommand { get; private set; }

		public ObservableCollection<Season> Seasons { get; private set;}
			
		public SeasonViewModel ():base()
		{
			InitData ();
		}

		protected override void InitCommands ()
		{
			ShowTeamsCommand = new RelayCommand<Season>(ShowTeamsCommandExecute); 
		}

		private void ShowTeamsCommandExecute(Season season)
		{
			Insights.Track(String.Format("'{0}' was selected", season.League));

			var nav = ServiceLocator.Current.GetInstance<INavigationService> ();
			nav.NavigateTo (ViewModelLocator.TEAM_VIEW_KEY, season.Id);
		}
			
		private async void InitData()
		{
			try
			{
				Seasons = new ObservableCollection<Season>();
				List<Season> seasons = await SeasonBL.GetAll(false);
				seasons.ForEach(s=>Seasons.Add(s));
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

