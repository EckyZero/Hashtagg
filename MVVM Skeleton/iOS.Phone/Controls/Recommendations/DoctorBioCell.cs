using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;
using Shared.VM;
using Shared.Common;

namespace iOS.Phone
{

	[Register("DoctorBioCell"), DesignTimeVisible(true)]
	public partial class DoctorBioCell : RecommendationMapCell
	{
		private DoctorRecommendationListItemViewModel _docVm;

		public  DoctorBioCell() 
		{
			var view = InitView ();
			Frame = view.Frame;
			AddSubview (view);

			this.ActionButton.TouchUpInside += (object sender, EventArgs e) => {
				OnActionButtonClick(_docVm);
			};
		}

		public DoctorBioCell (IntPtr handle) : base (handle) 
		{
			var view = InitView ();
			Frame = view.Frame;
			AddSubview (view);

			this.ActionButton.TouchUpInside += (object sender, EventArgs e) => {
				OnActionButtonClick(_docVm);
			};
		}

		protected override string CellNibFilename 
		{ 
			get
			{
				return "DoctorBioCell";
			} 
		} 

		public override void ConfigureCell(BaseRecommendationListItemViewModel docVm, Action<BaseRecommendationListItemViewModel> openMenu)
		{
			_docVm = (DoctorRecommendationListItemViewModel)docVm;

			this.StarView.AddStarRatingControl (_docVm.Rating);

			// Bind the remaining elements
			this.NameLabel.Text = _docVm.Title;
			this.SpecialtyLabel.Text = _docVm.SubTitle;
			this.ExperienceLabel.Text = _docVm.Experience;
			this.ReviewLabel.Text = _docVm.Reviews;
			this.AvailabilityLabel.Text = _docVm.Availability;
			this.DistanceLabel.Text = _docVm.Distance;

			if (_docVm.ShowFooter) {
				this.SuperlativeImageView.Image = UIImage.FromBundle (_docVm.SuperlativeImageName);
				this.SuperlativeLabel.Text = _docVm.Superlative;
				this.MoreLabel.Text = _docVm.More;
			} else {
				this.SuperlativeImageView.Image = null;
				this.SuperlativeLabel.Text = null;
				this.MoreLabel.Text = null;
				this.SuperlativeHorizontalLineView.Hidden = true;
			}

			this.CostTitleLabel.Hidden = !_docVm.ShowCost;
			this.CostValueLabel.Hidden = !_docVm.ShowCost;
			this.CostValueLabel.Text = _docVm.Cost;
			this.GenderImageView.Image = UIImage.FromBundle (_docVm.MainImageName);
			this.GenderImageViewHeightConstraint.Constant = _docVm.IsPremier ? 78 : 45;	
			this.NameLabel.TextColor = _docVm.TitleColor.ToUIColor();
			this.SuperlativeLabel.TextColor = _docVm.SuperlativeColor.ToUIColor ();
			this.SideView.BackgroundColor = _docVm.SideBarColor.ToUIColor ();

			this.AvailabilityLabel.TextColor = SharedColors.Black.ToUIColor();
			this.CostValueLabel.TextColor = SharedColors.Black.ToUIColor ();
			this.ExperienceLabel.TextColor = SharedColors.Black.ToUIColor ();
			this.DistanceLabel.TextColor = SharedColors.Black.ToUIColor ();

			if(_docVm.HideStarRatingControl)
			{
				StarViewWidthConstraint.Constant = 0;
				StarViewRightConstraint.Constant = 0;
			}
			else
			{
				StarViewWidthConstraint.Constant = 70;
				StarViewRightConstraint.Constant = 10;
			}

			_onRequestAction = openMenu;

			this.SetNeedsLayout ();
			this.LayoutIfNeeded ();
		}
			
	}
}

