using System;
using UIKit;
using CoreGraphics;
using Shared.Common;
using Foundation;
using System.ComponentModel;

namespace iOS
{
	[Register("PSCompassButton"), DesignTimeVisible(true)]
	public class PSCompassButton : UIButton
	{

		public enum Context
		{
			Primary,
			Secondary,
			Plain,
		}

		public Context ContextType
		{
			get { return _contextType; }
			set 
			{ 
				_contextType = value; 
				SetContextType (value);
			}
		}

		private Context _contextType = Context.Primary;

		public PSCompassButton (IntPtr handle) : base (handle) { }
		public PSCompassButton () { }

		public override void Draw (CGRect rect)
		{
			base.Draw (rect);

			SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (Bounds), UIControlState.Disabled);
			SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (Bounds), UIControlState.Normal);
			SetTitleColor (SharedColors.White.ToUIColor (), UIControlState.Normal);

			TitleLabel.Font = UIFont.FromName ("FuturaStd-Bold", 16);
			VerticalAlignment = UIControlContentVerticalAlignment.Fill;

			Layer.CornerRadius = 5;
			ClipsToBounds = true;
		}

		private void SetContextType (Context contextType)
		{
			switch (contextType)
			{
			case Context.Plain:
				SetBackgroundImage (UIColor.Clear.ToImage (Bounds), UIControlState.Disabled);
				SetBackgroundImage (UIColor.Clear.ToImage (Bounds), UIControlState.Normal);
				SetTitleColor (SharedColors.CompassBlue.ToUIColor (), UIControlState.Normal);
				break;
			case Context.Secondary:
				SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (Bounds), UIControlState.Disabled);
				SetBackgroundImage (SharedColors.CompassBlue.ToUIColor().ToImage (Bounds), UIControlState.Normal);
				SetTitleColor (SharedColors.White.ToUIColor (), UIControlState.Normal);
				break;
			default:
				SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (Bounds), UIControlState.Disabled);
				SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (Bounds), UIControlState.Normal);
				SetTitleColor (SharedColors.White.ToUIColor (), UIControlState.Normal);
				break;
			}
		}
	}
}

