using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.VM;
using System.Linq;

namespace iOS.Phone
{
	partial class SurveyDoctorPromptListController : UIViewController
	{
		private SurveyDoctorPromptListTableController _tableController;

		public SurveyDoctorPromptListViewModel ViewModel { get; private set; }

		public SurveyDoctorPromptListController (IntPtr handle) : base (handle)
		{
			ViewModel = new SurveyDoctorPromptListViewModel ();
		}

		private void OnBackButtonTapped(object sender, EventArgs eventArgs)
		{
			//Pop off the prompt list as well as the prompt if we are on the prompt list
			var controller = NavigationController.ChildViewControllers.Last(c => c.GetType() == typeof(BaseSurveyTableController));
			NavigationController.PopToViewController(controller,true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var backButton = new UIBarButtonItem (UIImage.FromBundle ("BackButton.png"),UIBarButtonItemStyle.Plain, OnBackButtonTapped);

			NavigationItem.SetLeftBarButtonItem (backButton,false);

			_tableController = ChildViewControllers [0] as SurveyDoctorPromptListTableController;

			_tableController.TableView.Scrolled += HandleScrolled;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			Title = ViewModel.Title;

			ProgressView.Progress = ViewModel.Progress;
		}

		public override async void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			if(ParentViewController == null || ParentViewController.NavigationController == null)
			{
				await ViewModel.OnDispose ();
			}
		}

		private void HandleScrolled (object sender, EventArgs args)
		{
			_tableController.TableView.AddParrallax (HeaderView);
		}
	}
}
