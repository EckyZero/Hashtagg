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
using System.Linq;

namespace iOS.Phone
{
	partial class SurveyFacilityPromptListTableController : ExtendedObservableTableViewController<FacilityViewModel>
	{
		public SurveyFacilityPromptListViewModel ViewModel { get { return (ParentViewController as SurveyFacilityPromptListController).ViewModel; } }

		private bool _isFirstLoad = true;

		public SurveyFacilityPromptListTableController (IntPtr handle) : base (handle){
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


			if(ViewModel.Facilities.Count == 0)
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

			this.DataSource = ViewModel.FacilityViewModels;

			this.CreateCellDelegate = (id, tableView, indexPath) => {	
				var cell = TableView.DequeueReusableCell (PSListCell.Key) ?? new PSListCell ();
				return cell;
			};

			this.BindCellDelegate = (UITableViewCell cell, FacilityViewModel facilityViewModel, NSIndexPath indexPath) => {

				var psCell = (PSListCell)cell;
				var facility = facilityViewModel.Model;

				psCell.HeaderText = facility.SpecialtiesDescription;
				psCell.ContentText = facility.Name;

				psCell.FooterText = facility.RecommendedAddress != null ? facility.RecommendedAddress.AddressDisplayLine : string.Empty ;
				psCell.SecondFooterText = facility.RecommendedAddress != null ? facility.RecommendedAddress.CityStateDisplayLine : string.Empty ;

				psCell.SetCommand("TouchUpInside", facilityViewModel.ModifyCommand);
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

			var lookupViewModel = new FacilityLookupViewModel (IocContainer.GetContainer ().Resolve<IFacilityBL> (), IocContainer.GetContainer ().Resolve<IGeolocator> ());

			lookupViewModel.Filters = filters;

			var controller = new FacilityLookupListController(lookupViewModel);

			controller.OnSelectAndReturn = OnSelectAndReturnFacility;

			var navController = new UINavigationController(controller);

			NavigationController.PresentViewController (navController, true, null);
		}

		private void OnSelectAndReturnFacility(Facility facility)
		{
			if (ViewModel.Facilities.Contains (facility)) {
				IocContainer.GetContainer().Resolve<IExtendedDialogService> ().ShowMessage (ApplicationResources.DuplicateAddMessage, ApplicationResources.DuplicateAddTitle );
			} 
			else
			{
				ViewModel.Facilities.Add (facility);
			}
			NavigationController.DismissViewController (true, null);
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
