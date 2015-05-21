using System;
using SegmentedControl;
using System.Drawing;
using Foundation;
using UIKit;
using Shared.VM;
using GalaSoft.MvvmLight.Helpers;
using Shared.Common;

namespace iOS.Phone
{
	public partial class HealthDetailsController : UIViewController
	{
		private HealthDetailsViewModel _viewModel;

		public HealthDetailsController (IntPtr handle) : base (handle)
		{
			_viewModel = new HealthDetailsViewModel ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitBindings ();

			InitUI ();
		}

		private void InitUI ()
		{

			if (!_viewModel.HasIncentives) 
			{
				SegmentedControl.RemoveSegmentAtIndex (2, false);
				ResultView.RemoveFromSuperview ();
			}
			// Note: This foreach implementation is a 'hack'. We could find no other way to get it to work
			// Customize the SDSegmentedControl
			SegmentedControl.BackgroundColor = SharedColors.DarkBlue.ToUIColor ();
			SegmentedControl.ArrowSize = 3;
			SegmentedControl.BorderColor = SharedColors.Tan2.ToUIColor ();

			foreach (UIView view in SegmentedControl.Subviews)
			{
				foreach(UIView subview in view.Subviews)
				{
					// Customize the "hover" view of the selected tab
					if(subview.GetType() == typeof(SDStainView))
					{
						var stainView = (SDStainView)subview;

						stainView.BackgroundColor = SharedColors.CompassBlue.ToUIColor ();
						stainView.CornerRadius = 5;
					}
					// Customize the label of each tab
					else if(subview.GetType() == typeof(SDSegmentView))
					{
						var segmentView = (SDSegmentView)subview;

						segmentView.SetTitleColor (SharedColors.White.ToUIColor (), UIControlState.Normal);
						segmentView.SetTitleColor (SharedColors.White.ToUIColor (), UIControlState.Selected);
						segmentView.SetTitleColor (SharedColors.White.ToUIColor (), UIControlState.Highlighted);
						segmentView.TitleFont = UIFont.FromName ("CenturyGothic-Bold", 12);
						segmentView.SelectedTitleFont = UIFont.FromName ("CenturyGothic-Bold", 12);
					}
				}
			}
		}

		private void InitBindings ()
		{
			Title = _viewModel.Title;

			// Bind Commands
			SegmentedControl.ValueChanged += (object sender, EventArgs e) => 
			{
				_viewModel.PageCommand.Execute((int)SegmentedControl.SelectedSegment);
			};

			// Bind Events
			_viewModel.RequestProceduresPage += OnRequestProceduresPage;
			_viewModel.RequestConditionsPage += OnRequestConditionsPage;
			_viewModel.RequestResultsPage += OnRequestResultsPage;
		}

		private void OnRequestProceduresPage (object sender, EventArgs args)
		{
			View.BringSubviewToFront (ProcedureView);
		}

		private void OnRequestConditionsPage (object sender, EventArgs args)
		{
			View.BringSubviewToFront (ConditionView);
		}

		private void OnRequestResultsPage (object sender, EventArgs args)
		{
			View.BringSubviewToFront (ResultView);
		}
	}
}
