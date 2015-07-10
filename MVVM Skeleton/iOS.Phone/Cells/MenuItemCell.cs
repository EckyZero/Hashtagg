// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;

namespace iOS.Phone
{
	public partial class MenuItemCell : BaseCell
	{
		public MenuItemCell (IntPtr handle) : base (handle)
		{
		}

		protected override async void ConfigureSubviews (IListItem item)
		{
			var viewModel = item as BaseMenuItemViewModel;

			await viewModel.DidLoad ();

			UserInteractionEnabled = viewModel.UserInteractionEnabled;
			TitleLabel.Text = viewModel.Title;
			SubtitleLabel.Text = viewModel.Subtitle;
			MainImageView.Image = UIImage.FromFile (viewModel.ImageName);
		}
	}
}
