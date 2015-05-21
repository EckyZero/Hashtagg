using System;
using Shared.VM;
using Shared.Common;
using UIKit;

namespace iOS.Phone
{
	public class IncentiveCantCompletePromptController : BasePromptController
	{
		private IncentiveCantCompletePromptViewModel _viewModel;

		public IncentiveCantCompletePromptController (IncentiveAction incentiveAction, IncentiveActionAttestModel attest) : base ()
		{
			_viewModel = new IncentiveCantCompletePromptViewModel (incentiveAction, attest);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var controller = GenericPromptController.Create <Provider>(_viewModel, this);

			_viewModel.RequestPreviousPage -= controller.OnRequestPreviousPage;
			_viewModel.RequestPreviousPage += OnRequestPreviousPage;

			controller.Image = UIImage.FromFile ("Stethicon");
		}

		public override void OnRequestLookupPage (object sender, EventArgs args)
		{
			var storyboard = UIStoryboard.FromName("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("DoctorLookupListController") as DoctorLookupListController;
			var navigationController = new UINavigationController (controller);

			controller.OnSelectAndReturnProvider = _viewModel.OnLookupDismissed;

			NavigationController.PresentViewController (navigationController, true, null);
		}

		public override void OnRequestNextPage (object sender, object model)
		{
			var controller = new IncentiveCantCompletePromptResponseController (_viewModel.Model, _viewModel.IncentiveAction, _viewModel.Attest);

			NavigationController.PushViewController (controller, true);
		}

		public void OnRequestPreviousPage (object sender, EventArgs args)
		{
			NavigationController.PopToRootViewController (true);
		}
	}
}

