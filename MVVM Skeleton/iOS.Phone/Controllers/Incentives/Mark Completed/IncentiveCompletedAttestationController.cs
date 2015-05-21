
using System;
using System.Drawing;

using Foundation;
using UIKit;
using Shared.VM;
using Shared.Common;

namespace iOS.Phone
{
	public class IncentiveCompletedAttestationController : BaseAttestationController
	{
		public IncentiveCompletedAttestationController (Provider model, IncentiveAction incentiveAction, DateTime? dateTime, IncentiveActionProcedure procedure, IncentiveCompletedAttestationControllerMode mode =  IncentiveCompletedAttestationControllerMode.GeneralAttestation) : base()
		{
			_viewModel = new IncentiveCompletedAttestationViewModel(model, incentiveAction, dateTime, procedure, mode == IncentiveCompletedAttestationControllerMode.LifestyleAttestation);
		}
	}

	public enum IncentiveCompletedAttestationControllerMode{
		GeneralAttestation,
		LifestyleAttestation
	}
}

