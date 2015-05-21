using System;
using Shared.VM;
using Shared.Common;
using UIKit;
using System.Linq;

namespace iOS.Phone
{
	public class IncentiveCantCompletePickerPromptController : BasePickerPromptController
	{
		private IncentiveCantCompletePickerPromptViewModel _viewModel;
		private GenericPickerPromptController _childController;

		public IncentiveCantCompletePickerPromptController (IncentiveAction incentiveAction, IncentiveActionStep incentiveActionStep) : base ()
		{
			_viewModel = new IncentiveCantCompletePickerPromptViewModel (incentiveAction, incentiveActionStep);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			 
			_childController = GenericPickerPromptController.Create (_viewModel, this);

			_viewModel.RequestPreviousPage -= _childController.OnRequestPreviousPage;
			_viewModel.RequestPreviousPage += OnRequestPreviousPage;

			_childController.Image = UIImage.FromFile ("Clipboard");
		}

		public override void OnRequestNextPage (object sender, object model)
		{
			var controller = new IncentiveCantCompletePromptController (_viewModel.IncentiveAction, _viewModel.Attest);

			NavigationController.PushViewController (controller, true);
		}

		public override void OnPickerSelection (object sender, EventArgs args)
		{
			var e = (PSFloatingPickerControlEventArgs)args;

			_viewModel.Model = _viewModel.IncentiveActionStep.AttestationReasons [e.Index];
			_childController.TextField.Hidden = !_viewModel.IncentiveActionStep.AttestationReasons.Last ().Equals (_viewModel.Model);

			_childController.TextField.Placeholder = _viewModel.OtherTextPlaceholder;
			_childController.TextField.ShouldReturn += (textField) => {
				textField.ResignFirstResponder ();
				return true;
			};
				
		}

		public void OnRequestPreviousPage (object sender, EventArgs args)
		{
			NavigationController.PopToRootViewController (true);
		}
	}
}

