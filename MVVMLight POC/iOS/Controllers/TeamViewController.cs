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

namespace Demo.iOS.Controllers
{
	partial class TeamViewController : ObservableTableViewController<Team>
	{
		public int SeasonId { get; set; }

		private TeamViewModel _viewModel;

        public TeamViewController(IntPtr handle) : base()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			_viewModel = new TeamViewModel (SeasonId);

			Title = _viewModel.Title;

			this.DataSource = _viewModel.Teams;

			this.CreateCellDelegate = id => {
				return new UITableViewCell ();
			};

			this.BindCellDelegate = (cell, team, path) => {
				cell.TextLabel.Text = team.Name;
				cell.DetailTextLabel.Text = team.ShortName;
			};
        }
	}
}