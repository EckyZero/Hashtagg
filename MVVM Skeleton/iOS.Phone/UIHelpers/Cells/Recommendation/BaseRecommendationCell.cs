using System;
using UIKit;
using Shared.VM;
using System.Collections.Generic;

namespace iOS.Phone
{
	public abstract class BaseRecommendationCell : UITableViewCell
	{
		protected Action<object, EventArgs> _touchUpInside;
		protected List<UILabel> _expandingLabels = new List<UILabel>();

		protected BaseRecommendationCell (IntPtr handle) : base (handle) { }

		public virtual nfloat CalculateHeight (BaseRecommendationListItemViewModel itemViewModel) 
		{
			nfloat height = UITableView.AutomaticDimension;

			if(Device.OS < 8)
			{
				Configure (itemViewModel, null);

				height = ContentView.SystemLayoutSizeFittingSize (UILayoutFittingCompressedSize).Height;

				foreach(UILabel label in _expandingLabels)
				{
					height += label.HeightToFitContent ();
				}
			}
			return height;
		}

		public abstract void Configure (BaseRecommendationListItemViewModel viewModel, Action<object, EventArgs> touchUpAction);

		public static string GetIdentifier (BaseRecommendationListItemViewModel viewModel)
		{
			var identifier = "";

			switch (viewModel.RecommendationType)
			{
			case RecType.Facility:
				identifier = FacilityRecCell.Key;
				break;
			case RecType.Doctor:
				identifier = DrRecCell.Key;
				break;
			}
			return identifier;
		}

		protected void OnTouchUpInside (object sender, EventArgs args)
		{
			if(_touchUpInside != null)
			{
				_touchUpInside (this, args);
			}
		}
	}
}

