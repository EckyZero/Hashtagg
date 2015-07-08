// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
{
	public partial class MenuController : UIViewController
	{
		#region Properties

		public MenuViewModel ViewModel { get; set; }

		#endregion

		public MenuController (IntPtr handle) : base (handle)
		{
			if(ViewModel == null) {
				ViewModel = new MenuViewModel ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			if(segue.DestinationViewController.GetType() == typeof(PSObservableTableController)) {

				var controller = segue.DestinationViewController as PSObservableTableController;

				controller.Collection = ViewModel.ItemViewModels;
				controller.SetEstimatedHeight (40);
			}
		}
	}
}
