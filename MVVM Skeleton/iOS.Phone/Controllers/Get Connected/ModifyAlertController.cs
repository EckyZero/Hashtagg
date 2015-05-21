
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.Common;
using Shared.VM;
using Microsoft.Practices.Unity;

namespace iOS.Phone
{
	public class ModifyAlertController<T>
	{
		private IEditableViewModel<T> _viewModel;

		public UIAlertController Alert { get; private set; }

		public ModifyAlertController (IEditableViewModel<T> viewModel)
		{
			_viewModel = viewModel;

			if(Device.OS >= 8)
			{
				Alert = UIAlertController.Create (null, null , UIAlertControllerStyle.ActionSheet);

				if (_viewModel.EditEnabled) {
					var editAction = UIAlertAction.Create (ApplicationResources.Edit, UIAlertActionStyle.Default, OnEditTapped);
					Alert.AddAction (editAction);
				}
				var removeAction = UIAlertAction.Create (ApplicationResources.Remove, UIAlertActionStyle.Destructive, OnRemoveTapped);
				var cancelAction = UIAlertAction.Create (ApplicationResources.Cancel, UIAlertActionStyle.Cancel, OnCancelTapped);


				Alert.AddAction (removeAction);
				Alert.AddAction (cancelAction);	
			}
		}

		public void Show()
		{
			var actionSheet = new UIActionSheet();
			var navService = IocContainer.GetContainer ().Resolve<ExtendedNavigationService> ();
			var controller = navService.CurrentNavigationController ();

			if (_viewModel.EditEnabled) {
				actionSheet.AddButton(ApplicationResources.Edit);
			}

			actionSheet.AddButton(ApplicationResources.Remove);
			actionSheet.AddButton(ApplicationResources.Cancel);

			actionSheet.DestructiveButtonIndex = 2;
			if (!_viewModel.EditEnabled)
				actionSheet.DestructiveButtonIndex = 1;
			actionSheet.WillDismiss += (object sender, UIButtonEventArgs e) => {

				switch(e.ButtonIndex)
				{
					case 0:
						if(_viewModel.EditEnabled)
							OnEditTappedSheet();
						else
							OnDeleteTappedSheet();
						break;
					case 1:
						if(_viewModel.EditEnabled)
							OnDeleteTappedSheet();
						break;
				default:
					break;
				}
			};
			System.Diagnostics.Debug.WriteLine ( "MODIFYCONTROLLER: " + controller);
			actionSheet.ShowInView (controller.View);
		}

		private void OnEditTapped (UIAlertAction action)
		{
			_viewModel.EditCommand.Execute (null);
		}

		private void OnRemoveTapped (UIAlertAction action)
		{
			var alert = UIAlertController.Create (ApplicationResources.RemoveAlertTitle, ApplicationResources.RemoveAlertMessage, UIAlertControllerStyle.Alert);
			var cancelAction = UIAlertAction.Create (ApplicationResources.Cancel, UIAlertActionStyle.Cancel, OnCancelTapped);
			var removeAction = UIAlertAction.Create (ApplicationResources.Remove, UIAlertActionStyle.Destructive, OnRemoveConfirmed);

			alert.AddAction (cancelAction);
			alert.AddAction (removeAction);


			UIApplication.SharedApplication.KeyWindow.RootViewController.FindViewControllerClass(typeof(UINavigationController)).PresentViewController (alert, true, null);
		}

		private void OnEditTappedSheet ()
		{
			_viewModel.EditCommand.Execute (null);
		}

		private void OnDeleteTappedSheet ()
		{
			var alert = new UIAlertView ();

			alert.Title = ApplicationResources.RemoveAlertTitle;
			alert.Message = ApplicationResources.RemoveAlertMessage;
			alert.AddButton (ApplicationResources.Cancel);
			alert.AddButton (ApplicationResources.Remove);

			alert.WillDismiss += (object sender, UIButtonEventArgs e) => {
				switch(e.ButtonIndex)
				{
					case 1:
					OnRemoveConfirmedSheet();
						break;
				}
			};

			alert.Show ();
		}

		private void OnCancelTapped (UIAlertAction action) {}

		private void OnRemoveConfirmed (UIAlertAction action)
		{
			_viewModel.DeleteCommand.Execute (_viewModel);
		}

		private void OnRemoveConfirmedSheet ()
		{
			_viewModel.DeleteCommand.Execute (_viewModel);
		}
	}
}

