
using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace iOS.Phone
{
	public abstract class BaseCalendarPromptController : UIViewController
	{
		public abstract void OnRequestChangePage (object sender, EventArgs args);
		public abstract void OnRequestNextPage (object sender, object model);
//		public abstract void OnRequestCalendarPage (object sender, EventArgs args);

		protected BaseCalendarPromptController (IntPtr handle) : base(handle) {}
		protected BaseCalendarPromptController () : base() {}
	}
}

