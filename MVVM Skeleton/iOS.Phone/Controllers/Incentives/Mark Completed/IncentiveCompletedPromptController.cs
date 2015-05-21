
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;
using System.Linq;

namespace iOS.Phone
{
	public class IncentiveCompletedPromptController : BasePromptController
	{	
		private IncentiveCompletedPromptViewModel _viewModel;

		public IncentiveCompletedPromptController (IncentiveAction incentiveAction) : base ()
		{
			_viewModel = new IncentiveCompletedPromptViewModel(incentiveAction);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var controller = GenericPromptController.Create <Provider>(_viewModel, this);

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
			string selectedProcedureName = _viewModel.SelectedText;
			IncentiveActionProcedure procedure = null;

			if(!string.IsNullOrWhiteSpace(selectedProcedureName))
			{
				procedure = _viewModel.IncentiveAction.Procedures.Single(p => p.Description == selectedProcedureName);
			}

			var controller = new IncentiveCalendarPromptController (_viewModel.Model, _viewModel.IncentiveAction, procedure);

			NavigationController.PushViewController (controller, true);
		}
	}
}

