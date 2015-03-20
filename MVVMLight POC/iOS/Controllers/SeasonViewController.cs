using System;
using System.Collections.Generic;
using Demo.iOS.Utils;
using Demo.Shared;
using Demo.Shared.Helpers;
using Demo.Shared.Models;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Demo.iOS.Controllers
{
	partial class SeasonViewController : ObservableTableViewController<Season>
    {
		private SeasonViewModel _viewModel;

		public SeasonViewController(IntPtr handle) : base()
        {
			_viewModel = new SeasonViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			Title = _viewModel.Title;

			this.DataSource = _viewModel.Seasons;

			this.CreateCellDelegate = id => {
				return new UITableViewCell ();
			};

			this.BindCellDelegate = (cell, season, path) => {
				cell.TextLabel.Text = season.Caption;
				cell.DetailTextLabel.Text = season.League;
			};

			this.SelectionChanged +=
				(s, e) => _viewModel.ShowTeamsCommand.Execute(this.SelectedItem);
        }
    }
}