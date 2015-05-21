using System;
using Shared.VM;
using Shared.Common;

namespace iOS.Phone
{
	public class IncentiveCantCompleteAttestationController : BaseAttestationController
	{
		public IncentiveCantCompleteAttestationController (IncentiveAction incentiveAction, IncentiveActionAttestModel attest) : base ()
		{
			_viewModel = new IncentiveCantCompleteAttestationViewModel (incentiveAction, attest);
		}
	}
}

