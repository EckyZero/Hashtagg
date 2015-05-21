using System;
using UIKit;

namespace iOS.Phone
{
	public abstract class BasePickerPromptController : UIViewController
	{
		public abstract void OnRequestNextPage (object sender, object model);
		public abstract void OnPickerSelection (object sender, EventArgs args);

		protected BasePickerPromptController (IntPtr handle) : base(handle) {}
		protected BasePickerPromptController () : base() {}
	}
}

