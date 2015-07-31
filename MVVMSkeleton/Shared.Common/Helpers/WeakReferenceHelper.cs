using System;

namespace Shared.Common
{
	public static class WeakReferenceHelper
	{
		public static T GetTarget<T>(WeakReference<T> reference) where T : class
		{
			T targ;
			if (reference.TryGetTarget (out targ)) {
				return targ;
			} else {
				throw new Exception ("The target object was never instantiated or has been garbage collected, review the lifecycle of your views.");
			}
		}
	}
}

