using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Shared.Common;
using Microsoft.Practices.Unity;
using Shared.VM;

namespace iOS.Phone
{
	partial class DoctorPromptListController : UIViewController
	{
		private DoctorPromptListTableController _tableController;
		private IDoctorPromptListViewModel _viewModel;

		public DoctorPromptListController (IntPtr handle) : base (handle)
		{
			_viewModel = IocContainer.GetContainer().Resolve<IDoctorPromptListViewModel> ();
			Application.VMStore.DoctorPromptListVM = _viewModel;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_tableController = ChildViewControllers [0] as DoctorPromptListTableController;
			//_tableController.TableView.Scrolled += HandleScrolled;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NavigationController.SetNavigationBarHidden (true, true);

			ProgressView.Progress = IocContainer.GetContainer ().Resolve<IDoctorPromptViewModel> ().Progress;
		}

		private void HandleScrolled (object sender, EventArgs args)
		{
			_tableController.TableView.AddParrallax (HeaderView);
		}
	}
}
