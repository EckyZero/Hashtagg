// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace iOS.Phone
{
	[Register ("WebViewController")]
	partial class WebViewController
	{
		[Outlet]
		UIKit.UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem BackButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem CloseButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem ForwardButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem RefreshButton { get; set; }

		[Outlet]
		UIKit.UIToolbar Toolbar { get; set; }

		[Outlet]
		UIKit.UIWebView WebView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}

			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}

			if (CloseButton != null) {
				CloseButton.Dispose ();
				CloseButton = null;
			}

			if (ForwardButton != null) {
				ForwardButton.Dispose ();
				ForwardButton = null;
			}

			if (RefreshButton != null) {
				RefreshButton.Dispose ();
				RefreshButton = null;
			}

			if (WebView != null) {
				WebView.Dispose ();
				WebView = null;
			}

			if (Toolbar != null) {
				Toolbar.Dispose ();
				Toolbar = null;
			}
		}
	}
}
