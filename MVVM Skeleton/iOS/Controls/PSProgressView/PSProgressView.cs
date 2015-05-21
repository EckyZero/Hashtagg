using System;
using UIKit;
using Foundation;
using Shared.Common;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;

namespace iOS
{
	[Register("PSProgressView"), DesignTimeVisible(true)]
	public partial class PSProgressView : UIView
	{
		public event EventHandler ProgressChanged;

		#region Private Variables

		nfloat _labelWidthConstraintConstant;

		nfloat _labelLeadingConstraintConstant;

		#endregion

		#region Member Properties

		public UIColor TrackTintColor
		{
			get { return ProgressView.TrackTintColor; }
			set { ProgressView.TrackTintColor = value; }
		}

		public UIColor ProgressTintColor
		{
			get { return ProgressView.ProgressTintColor; }
			set { ProgressView.ProgressTintColor = value; }
		}

		public UIColor BorderColor
		{
			get { return UIColor.FromCGColor (ProgressView.Layer.BorderColor); }
			set { ProgressView.Layer.BorderColor = value.CGColor; }
		}
			
		public float Progress
		{
			get { return ProgressView.Progress; }
			set 
			{ 
				ProgressView.Progress = value;

				if(ProgressChanged != null)
				{
					ProgressChanged (this, new EventArgs());
				}
			}
		}

		public nfloat BorderWidth
		{
			get { return ProgressView.Layer.BorderWidth; }
			set { ProgressView.Layer.BorderWidth = value; }
		}

		public UIColor TextColor
		{
			get { return Label.TextColor; }
			set { Label.TextColor = value; }
		}

		public string Text
		{
			get { return Label.Text; }
			set { Label.Text = value; }
		}

		public UIFont Font
		{
			get { return Label.Font; }
			set { Label.Font = value; }
		}

		public bool LabelHidden
		{
			get { return (LabelWidthConstraint.Constant == 0 && LabelLeadingConstraint.Constant == 0); }
			set
			{
				LabelWidthConstraint.Constant = value ? 0 : _labelWidthConstraintConstant;
				LabelLeadingConstraint.Constant = value ? 0 : _labelLeadingConstraintConstant;
			}
		}

		public nfloat CornerRadius
		{
			get { return ProgressView.Layer.CornerRadius; }
			set { ProgressView.Layer.CornerRadius = value; }
		}

		#endregion

		private PSProgressView() {}

		public PSProgressView (IntPtr handle) : base (handle) 
		{
			var nib = NSBundle.MainBundle.LoadNib("PSProgressView", this, null);
			var view = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;

			view.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);

			AddSubview (view);

			ProgressView.Layer.BorderColor = SharedColors.Tan2.ToUIColor ().CGColor;

			_labelLeadingConstraintConstant = LabelLeadingConstraint.Constant;
			_labelWidthConstraintConstant = LabelWidthConstraint.Constant;
		}

		#region Methods

		public void SetProgress (float progress, bool animated)
		{
			ProgressView.SetProgress (progress, animated);
		}

		#endregion
	}
}

