using System;
using UIKit;
using Shared.VM;
using Shared.Common;

namespace iOS.Phone
{
	public class BaseCardCell : UITableViewCell
	{
		#region Private Variables

		public static nfloat DefaultMargin = 8;

		#endregion

		public BaseCardCell (IntPtr handle) : base (handle)
		{
		}

		#region Methods

		public virtual nfloat CalculateHeight (BaseCardViewModel viewModel, bool isHeader = false)
		{
			var height = UITableView.AutomaticDimension;

			if(Device.OS < 8)
			{
				Configure (viewModel, isHeader);
				height = ContentView.SystemLayoutSizeFittingSize (UILayoutFittingCompressedSize).Height;
			}
			return height;
		}

		public virtual void Configure (BaseCardViewModel baseViewModel,  bool isHeader = false)
		{
			
		}

		public static string GetIdentifier (BaseCardViewModel viewModel)
		{
			var identifier = HealthProCardCell.Key;

			if (viewModel != null)
			{
				// TODO: Add other conditions here as new cards are created
				switch(viewModel.CardType)
				{
				case CardType.HealthProCard:
					identifier = HealthProCardCell.Key;
					break;
				default:
					identifier = HealthProCardCell.Key;
					break; 
				}	
			}
			return identifier;
		}
		#endregion
	}
}

