using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using Shared.VM;
using Microsoft.Practices.Unity;
using Shared.BL;
using System.Collections.Generic;

namespace iOS.Phone
{
	partial class SurveyDoctorPromptListTableController : ExtendedObservableTableViewController<ProviderViewModel>
	{
		public SurveyDoctorPromptListViewModel ViewModel { get { return (ParentViewController as SurveyDoctorPromptListController).ViewModel; } }

		private bool _isFirstLoad = true;

		public SurveyDoctorPromptListTableController (IntPtr handle) : base (handle){
		}

		public override void ViewDidLoad (){}

		public override async void ViewWillAppear (bool animated)
		{
			if (_isFirstLoad) {
				base.ViewDidLoad ();	
				InitBindings ();
				_isFirstLoad = false;
			}

			base.ViewWillAppear (animated);
			ConfigureDelegates ();
			await ViewModel.Subscribe();


			if(ViewModel.Providers.Count == 0)
			{
				NavigationController.PopViewController (true);
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			TableView.ScrollToBottom (true);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			ViewModel.Unsubscribe();
		}


		public override void ConfigureDelegates ()
		{
			this.TableView.RowHeight = 100;

			this.DataSource = ViewModel.ProviderViewModels;

			this.CreateCellDelegate = (id, tableView, indexPath) => {	
				var cell = TableView.DequeueReusableCell (PSListCell.Key) ?? new PSListCell ();
				return cell;
			};

			this.BindCellDelegate = (UITableViewCell cell, ProviderViewModel providerViewModel, NSIndexPath indexPath) => {

				var psCell = (PSListCell)cell;
				var provider = providerViewModel.Model;


				psCell.HeaderText = provider.SpecialtiesDescription;
				psCell.ContentText = provider.DoctorName;
				psCell.FooterText = provider.AddressDisplayLine;
				psCell.SecondFooterText = provider.CityStateDisplayLine;

				psCell.SetCommand("TouchUpInside", providerViewModel.ModifyCommand);
			};
		}

		private void ViewModel_RequestSkipPage()
		{
			var storyboard = UIStoryboard.FromName ("SurveyStoryboard", null);

			var controller = storyboard.InstantiateViewController ("DoctorPreferencesController") as DoctorPreferencesController;

			controller.FlowData = ViewModel.DestinationViewModel.FlowData;
			controller.TreeMap = ViewModel.DestinationViewModel.Map;

			NavigationController.PushViewController (controller, true);
		}

		private void ViewModel_RequestCancelPage()
		{
			var controller = UIStoryboard.FromName ("MainStoryboard", null).InstantiateViewController ("HomeContainerController");
			NavigationController.ParentViewController.NavigationController.SetViewControllers (new UIViewController[] { controller }, true);
		}

		private void ViewModel_RequestNextPage(SurveyViewModel destViewModel)
		{
			var controller = new BaseSurveyTableController (destViewModel);

			NavigationController.PushViewController (controller, true);
		}

		private void ViewModel_RequestLookupPage(IList<ILookupFilter> filters)
		{
			UIStoryboard storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController("DoctorLookupListController") as DoctorLookupListController;

			controller.OnSelectAndReturnProvider = OnSelectAndReturnProvider;
			controller.Filters = filters;

			var navController = new UINavigationController(controller);
			NavigationController.PresentViewController (navController, true, null);
		}

		private void OnSelectAndReturnProvider(Provider provider)
		{
			if (ViewModel.Providers.Contains (provider)) {
				IocContainer.GetContainer().Resolve<IExtendedDialogService> ().ShowMessage (ApplicationResources.DuplicateDoctorAdd, ApplicationResources.DuplicateDoctorAddTitle );
			} 
			else
			{
				ViewModel.Providers.Add (provider);
			}
		}
		private void InitBindings ()
		{
			ViewModel.RequestNextPage = ViewModel_RequestNextPage;
			NextButton.SetCommand ("TouchUpInside", ViewModel.NextCommand);

			ViewModel.RequestLookupPage = ViewModel_RequestLookupPage;
			AddButton.SetCommand ("TouchUpInside", ViewModel.AddCommand);

			ViewModel.RequestSkipPage = ViewModel_RequestSkipPage;
			SkipThisPartButton.SetCommand ("TouchUpInside", ViewModel.SkipCommand);

			ViewModel.RequestCancelPage = ViewModel_RequestCancelPage;
			CancelButton.SetCommand ("TouchUpInside", ViewModel.CancelCommand);

		}
	}
}
