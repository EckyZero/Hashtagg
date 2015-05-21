using Foundation;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using Shared.VM;
using Microsoft.Practices.Unity;
using Shared.BL;
using System.Collections.Specialized;

namespace iOS.Phone
{
	partial class DoctorPromptListTableController : ExtendedObservableTableViewController<PatientProviderViewModel>
	{
		public DoctorPromptListTableController (IntPtr handle) : base (handle){
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			ConfigureDelegates ();
			await Application.VMStore.DoctorPromptListVM.Subscribe();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			TableView.ScrollToBottom (true);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			Application.VMStore.DoctorPromptListVM.Unsubscribe();
		}

		public override void ConfigureDelegates ()
		{
			this.TableView.RowHeight = 100;

			this.DataSource = Application.VMStore.DoctorPromptListVM.PatientProviderViewModels;

			this.CreateCellDelegate = (id, tableView, indexPath) => {	
				var cell = TableView.DequeueReusableCell (PSListCell.Key) ?? new PSListCell ();
				return cell;
			};

			this.BindCellDelegate = (UITableViewCell cell, PatientProviderViewModel patientProviderViewModel, NSIndexPath indexPath) => {

				var psCell = (PSListCell)cell;
				var patientProvider = patientProviderViewModel.Model;

				psCell.HeaderText = patientProvider.SpecialtiesDescription;
				psCell.ContentText = patientProvider.Name;
				psCell.FooterText = patientProvider.AddressDisplayLine;
				psCell.SecondFooterText = patientProvider.CityStateDisplayLine;

				psCell.SetCommand("TouchUpInside", patientProviderViewModel.ModifyCommand);
			};
		}
			
		private void InitBindings ()
		{
			NextButton.SetCommand ("TouchUpInside", Application.VMStore.DoctorPromptListVM.NextCommand);
			AddButton.SetCommand ("TouchUpInside", Application.VMStore.DoctorPromptListVM.AddCommand);
		}
	}
}
