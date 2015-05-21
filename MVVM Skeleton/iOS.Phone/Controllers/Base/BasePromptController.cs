using System;
using UIKit;
using Shared.Common;

namespace iOS.Phone
{
	public abstract class BasePromptController : UIViewController
	{
		public abstract void OnRequestLookupPage (object sender, EventArgs args);
		public abstract void OnRequestNextPage (object sender, object model);

		protected BasePromptController (IntPtr handle) : base(handle) {}
		protected BasePromptController () : base() {}
	}
}

