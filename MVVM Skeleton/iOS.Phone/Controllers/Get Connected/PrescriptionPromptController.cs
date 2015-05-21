

using System;
using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using Microsoft.Practices.Unity;
using GalaSoft.MvvmLight.Helpers;
using Shared.BL;
using System.Linq;

namespace iOS.Phone
{
	public partial class PrescriptionPromptController : UIViewController
	{
		private IPrescriptionPromptViewModel _viewModel;

		public PrescriptionPromptController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer().Resolve<IPrescriptionPromptViewModel> ();
			Application.VMStore.PrescriptionPromptVM = _viewModel;
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			bool shouldAppear = await Application.VMStore.PrescriptionPromptVM.ViewShouldAppear ();

			if (!shouldAppear)
			{
				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var controller = storyboard.InstantiateViewController ("PrescriptionPromptListController") as PrescriptionPromptListController;

				NavigationController.PushViewController (controller, false);
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			PromptButton.Text = Application.VMStore.PrescriptionPromptVM.Placeholder;
		}

		private void InitBindings ()
		{
			PromptButton.SetCommand ("Clicked", Application.VMStore.PrescriptionPromptVM.PrescriptionPromptCommand);

			_iDontHaveOneButton.SetCommand ("TouchUpInside", Application.VMStore.PrescriptionPromptVM.IDontHaveOneCommand);

			SkipButton.SetCommand ("TouchUpInside", Application.VMStore.PrescriptionPromptVM.SkipCommand);

			ProgressView.Progress = Application.VMStore.PrescriptionPromptVM.Progress;
		}
	}
}
