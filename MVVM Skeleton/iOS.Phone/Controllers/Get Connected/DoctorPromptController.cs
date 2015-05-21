using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Shared.VM;
using Shared.BL;

namespace iOS.Phone
{
	partial class DoctorPromptController : UIViewController
	{
		private IDoctorPromptViewModel _viewModel;

		public DoctorPromptController (IntPtr handle) : base (handle) 
		{
			_viewModel = IocContainer.GetContainer().Resolve<IDoctorPromptViewModel> ();
			Application.VMStore.DoctorPromptVM = _viewModel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.SetNavigationBarHidden (true, true);

			bool shouldAppear = await Application.VMStore.DoctorPromptVM.ViewShouldAppear ();

			if (!shouldAppear) {

				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("DoctorPromptListController") as DoctorPromptListController;

				NavigationController.PushViewController (controller, false);
			}
		}

		private void InitBindings ()
		{
			PromptButton.SetCommand ("Clicked", Application.VMStore.DoctorPromptVM.DoctorPromptCommand);

			_iDontHaveOneButton.SetCommand ("TouchUpInside", Application.VMStore.DoctorPromptVM.IDontHaveOneCommand);

			ProgressView.Progress = Application.VMStore.DoctorPromptVM.Progress;
		}
	}
}
 