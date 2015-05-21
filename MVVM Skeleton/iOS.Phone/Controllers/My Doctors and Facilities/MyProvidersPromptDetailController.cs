using System;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
{
	public partial class MyProvidersPromptDetailController : UIViewController
	{
		private MyDoctorsPromptViewModel _viewModel;

		public MyProvidersPromptDetailController (IntPtr handle) : base (handle)
		{
			_viewModel = new MyDoctorsPromptViewModel();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			AddDoctorButton.SetCommand("TouchUpInside", _viewModel.AddADoctorCommand);

			DocRecButton.SetCommand("TouchUpInside", _viewModel.NeedADoctorCommand);

			_viewModel.RequestPromptListPage += _viewModel_RequestPromptListPage;
			_viewModel.RequestLookupPage += _viewModel_RequestLookupPage;
			_viewModel.RequestDocRec += _viewModel_RequestDocRec;
		}

		private void _viewModel_RequestPromptListPage (object sender, EventArgs e)
		{
			GoToPromptList ();
		}

		private void GoToPromptList()
		{
			//var controller = new MyProvidersTableController ();

			var storyboard = UIStoryboard.FromName ("DoctorsFacilitiesStoryboard", null);
			var controller = storyboard.InstantiateViewController ("MyProvidersTableController") as MyProvidersTableController;

			NavigationController.PushViewController (controller, false);
		}

		void _viewModel_RequestLookupPage (object sender, EventArgs e)
		{
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("DoctorLookupListController") as DoctorLookupListController;
			controller.OnSelectPatientProvider = GoToPromptList;
			var navController = new UINavigationController (controller);

			navController.ConfigureToCompassDefaults (false);

			this.PresentViewController (navController, true, null);
		}

		void _viewModel_RequestDocRec (object sender, SurveyViewModel e)
		{
			if (e != null) {
				UIViewController controller = new BaseSurveyTableController(e);

				ParentViewController.NavigationController.PushViewController (controller, false);
			}
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			await _viewModel.WillAppear ();
		}
	}
}
