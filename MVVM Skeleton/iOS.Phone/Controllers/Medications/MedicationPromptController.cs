
using System;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;

namespace iOS.Phone
{
	public class MedicationPromptController : BasePromptController
	{
		private MedicationPromptViewModel _viewModel;

		public MedicationPromptController () : base ()
		{
			_viewModel = new MedicationPromptViewModel();
		}
			
		public override async void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var controller = GenericPromptDetailController.Create<Prescription> (_viewModel, this);
			var image = UIImage.FromFile("Med1.png");

			controller.Image = image;

			NavigationController.NavigationBarHidden = true;
		}

		public override async void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			await _viewModel.WillAppear ();
		}

		public override void OnRequestLookupPage (object sender, EventArgs args)
		{
			var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
			var controller = storyboard.InstantiateViewController ("PrescriptionLookupListController") as PrescriptionLookupListController;
			var navController = new UINavigationController (controller);
			 
			navController.ConfigureToCompassDefaults (false);

			this.ParentViewController.NavigationController.ParentViewController.PresentViewController (navController, true, null);
		}

		public override void OnRequestNextPage (object sender, object model)
		{
			var controller = new MedicationTableController ();

			NavigationController.PushViewController (controller, false);
		}
	}
}

