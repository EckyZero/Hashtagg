using System;
using Shared.VM;
using Shared.Common;
using UIKit;
using System.Linq;

namespace iOS.Phone
{
	public class IncentiveCantCompletePromptResponseController : BaseCalendarPromptController
	{
		private IncentiveCantCompletePromptResponseViewModel _viewModel;

		public Provider Model 
		{
			get { return _viewModel.Model; }
			set { _viewModel.Model = value; }
		}

		public IncentiveCantCompletePromptResponseController (Provider model, IncentiveAction action, IncentiveActionAttestModel attest) : base ()
		{
			_viewModel = new IncentiveCantCompletePromptResponseViewModel (model, action, attest);
		}

		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var controller = GenericCalendarPromptController.Create<Provider> (_viewModel, this);

			controller.Image = UIImage.FromFile ("Stethicon");

			_viewModel.RequestPreviousPage -= controller.OnRequestPreviousPage;
			_viewModel.RequestPreviousPage += OnRequestPreviousPage;

			await _viewModel.DidLoad();
		}

		public override void OnRequestChangePage (object sender, EventArgs args)
		{
			var storyboard = UIStoryboard.FromName("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("DoctorLookupListController") as DoctorLookupListController;
			var navigationController = new UINavigationController (controller);

			controller.OnSelectAndReturnProvider = _viewModel.OnChangeDismissed;

			NavigationController.PresentViewController (navigationController, true, null);
		}

		public override void OnRequestNextPage (object sender, object model)
		{
			var controller = new IncentiveCantCompleteAttestationController(_viewModel.IncentiveAction,_viewModel.Attest);

			NavigationController.PushViewController (controller, true);
		}

		public void OnRequestPreviousPage (object sender, EventArgs args)
		{
			NavigationController.PopToRootViewController (true);
		}
	}
}

