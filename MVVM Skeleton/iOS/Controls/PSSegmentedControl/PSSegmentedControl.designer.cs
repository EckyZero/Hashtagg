// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;
using System.ComponentModel;

namespace iOS
{
	[Register("PSSegmentedControl"), DesignTimeVisible(true)]
	partial class PSSegmentedControl
	{
		[Outlet]
		UIKit.UISegmentedControl SegmentedControl { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SegmentedControl != null) {
				SegmentedControl.Dispose ();
				SegmentedControl = null;
			}
		}
	}
}
