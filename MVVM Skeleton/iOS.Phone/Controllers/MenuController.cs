// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using System.ComponentModel;

namespace iOS.Phone
{
	public partial class MenuController : UIViewController
	{
		#region Variables

		private PSObservableTableController _tableController;

		#endregion

		#region Properties

		public MenuViewModel ViewModel { get; set; }

		#endregion

		#region Methods

		public MenuController (IntPtr handle) : base (handle)
		{
			if(ViewModel == null) {
				ViewModel = new MenuViewModel ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitUI ();
			InitBindings ();
		}

		private void InitUI ()
		{
			// TODO: Fade-in and out depending on the JASidePanel gesture
			SubtitleLabel.Alpha = ViewModel.ItemViewModels.Count == 0 ? 1 : 0;
			TitleLabel.Text = ViewModel.Title;

			PrimaryButton.Layer.CornerRadius = 6;
			PrimaryButton.Layer.BorderColor = PrimaryButton.TitleLabel.TextColor.CGColor;
			PrimaryButton.Layer.BorderWidth = 1;
		}

		private void InitBindings ()
		{
			ViewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				if(e.PropertyName.Equals("PrimaryButtonText")) {
					PrimaryButton.SetTitle(ViewModel.PrimaryButtonText, UIControlState.Normal);
				}
			};

			PrimaryButton.SetCommand ("TouchUpInside", ViewModel.PrimaryCommand);

//			ViewModel.RequestRowUpdate = OnRequestRowUpdate;
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(PSObservableTableController)) {

				_tableController = segue.DestinationViewController as PSObservableTableController;

				_tableController.Collection = ViewModel.ItemViewModels;
				_tableController.SetEstimatedHeight (40);

				_tableController.View.Alpha = ViewModel.ItemViewModels.Count == 0 ? 0 : 1;
				_tableController.AddAnimation = UITableViewRowAnimation.Left;
				_tableController.DeleteAnimation = UITableViewRowAnimation.Left;
			}
		}

//		private void OnRequestRowUpdate (BaseMenuItemViewModel viewModel, int index)
//		{
//			_tableController.TableView.ReloadData ();
//			var indexPaths = new NSIndexPath[] { NSIndexPath.FromRowSection (index, 0) };

//			_tableController.TableView.ReloadRows (indexPaths, UITableViewRowAnimation.Automatic);
//		}

		#endregion
	}
}
