using System;
using Foundation;
using System.ComponentModel;
using Shared.VM;
using Shared.Common;
using UIKit;
using ObjCRuntime;

namespace iOS.Phone
{
	[Register("FacilityBioCell"), DesignTimeVisible(true)]
	public partial class FacilityBioCell : RecommendationMapCell
	{
		private FacilityRecommendationListItemViewModel _facVm;

		public  FacilityBioCell() 
		{
			var view = InitView ();
			Frame = view.Frame;
			AddSubview (view);

			this.ActionButton.TouchUpInside += (object sender, EventArgs e) => {
				OnActionButtonClick(_facVm);
			};
		}

		public FacilityBioCell (IntPtr handle) : base (handle) 
		{
			var view = InitView ();
			Frame = view.Frame;
			AddSubview (view);

			this.ActionButton.TouchUpInside += (object sender, EventArgs e) => {
				OnActionButtonClick(_facVm);
			};
		}


		protected override string CellNibFilename 
		{ 
			get
			{
				return "FacilityBioCell";
			} 
		} 

		public override void ConfigureCell (BaseRecommendationListItemViewModel baseVM, Action<BaseRecommendationListItemViewModel> openMenu)
		{
			var viewModel = (FacilityRecommendationListItemViewModel)baseVM;
			_facVm = viewModel;

			_onRequestAction = openMenu;

			this.NameLabel.Text = viewModel.Title;
			this.SubtitleLabel.Text = viewModel.SubTitle;
			this.MainImageView.Image = UIImage.FromBundle (viewModel.MainImageName);
			this.CostTitleLabel.Hidden = !viewModel.ShowCost;
			this.CostValueLabel.Hidden = !viewModel.ShowCost;
			this.CostValueLabel.Text = viewModel.Cost;
			this.NameLabel.TextColor = viewModel.TitleColor.ToUIColor();
			this.SuperlativeLabel.TextColor = viewModel.SuperlativeColor.ToUIColor ();
			this.SideView.BackgroundColor = viewModel.SideBarColor.ToUIColor ();
			this.SuperlativeImageView.Image = UIImage.FromBundle (viewModel.SuperlativeImageName);
			this.SuperlativeLabel.Text = viewModel.Superlative;
			this.MoreLabel.Text = viewModel.More;
			this.AccreditedLabel.Text = viewModel.AccreditedText;
			this.DistanceLabel.Text = viewModel.Distance;

			this.DistanceLabel.TextColor = SharedColors.Black.ToUIColor ();
			this.CostValueLabel.TextColor = SharedColors.Black.ToUIColor ();

			if(!String.IsNullOrWhiteSpace(this.AccreditedLabel.Text))
			{
				var font = UIFont.FromName ("CenturyGothic-Bold", this.AccreditedLabel.Font.PointSize);
				var range = new NSRange(0, this.AccreditedLabel.Text.IndexOf(" "));
				var text = new NSMutableAttributedString (this.AccreditedLabel.Text);

				text.SetAttributes (new UIStringAttributes () { Font = font }, range);

				this.AccreditedLabel.AttributedText  = text;	
			}

			this.SeperatorView.Hidden = !viewModel.ShowFooter;

			SetNeedsLayout ();
			LayoutIfNeeded ();
		}



	}
}

