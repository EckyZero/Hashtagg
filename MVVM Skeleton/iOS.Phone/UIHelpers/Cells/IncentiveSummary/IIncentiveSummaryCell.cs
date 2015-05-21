using System;
using UIKit;
using Shared.Common;

namespace iOS.Phone
{
	public interface IIncentiveSummaryCell
	{
		void Configure (Incentive incentive);
		void Configure (IncentiveAction incentiveAction);
	}
}

