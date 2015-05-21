
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using System.Linq;

namespace iOS.Phone
{
	public class IncentiveCalendarPromptController : BaseCalendarPromptController
	{
		private IncentiveCalendarPromptViewModel _viewModel;

		public Provider Model 
		{
			get { return _viewModel.Model; }
			set { _viewModel.Model = value; }
		}

		public IncentiveCalendarPromptController (Provider model, IncentiveAction incentiveAction, IncentiveActionProcedure procedure = null) : base ()
		{
			_viewModel = new IncentiveCalendarPromptViewModel(model, incentiveAction, procedure);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var controller = GenericCalendarPromptController.Create<Provider> (_viewModel, this);

			controller.Image = UIImage.FromFile ("Calendar");

			_viewModel.RequestPreviousPage -= controller.OnRequestPreviousPage;
			_viewModel.RequestPreviousPage += OnRequestPreviousPage;
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
			var controller = new IncentiveCompletedAttestationController(_viewModel.Model, _viewModel.IncentiveAction, _viewModel.CalendarDate, _viewModel.IncentiveActionProcedure);

			NavigationController.PushViewController (controller, true);
		}

		public void OnRequestPreviousPage (object sender, EventArgs args)
		{
			NavigationController.PopToRootViewController(true);
		}
	}
}

