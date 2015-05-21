using System;
using Shared.Common;
using Shared.Common.Models.TuringTree;
using System.Linq;
using UIKit;

namespace iOS.Phone
{
	public abstract class BaseSurveyButtonCell : BaseSurveyCell
	{
		protected BaseSurveyButtonCell (IntPtr handle) : base(handle) { }

		protected void OnLeftButtonTouchUpInside (object sender, EventArgs args)
		{
			OnTouchUpInside (sender, Section.SectionActions.ElementAt (0));
		}

		protected void OnCenterButtonTouchUpInside (object sender, EventArgs args)
		{
			GenericAction action = null;

			if(Section.SectionActions.Count() == 1)
			{
				action = Section.SectionActions.ElementAt (0);
			}
			else
			{
				action = Section.SectionActions.ElementAt(1);	
			}
			OnTouchUpInside (sender, action);	
		}

		protected void OnRightButtonTouchUpInside (object sender, EventArgs args)
		{
			OnTouchUpInside (sender, Section.SectionActions.ElementAt (Section.SectionActions.Count() - 1));
		}

		protected void SetButtonDefaultEnabledState (UIButton button, GenericAction action)
		{
			button.Enabled = true;

			if(action.ActionDependencies.Any())
			{
				button.Enabled = action.Selected;	
			}
			button.SetBackgroundImage (SharedColors.Gray3.ToUIColor ().ToImage (button.Bounds), UIControlState.Disabled);
			button.ClipsToBounds = true;
		}
	}
}

