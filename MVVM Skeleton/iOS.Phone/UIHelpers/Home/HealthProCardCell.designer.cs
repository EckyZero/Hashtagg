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
	[Register ("HealthProCardCell")]
	partial class HealthProCardCell
	{
		[Outlet]
		UIKit.UIButton ActionButton { get; set; }

		[Outlet]
		UIKit.UIButton CallButton { get; set; }

		[Outlet]
		UIKit.UIView ContainerView { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ContainerViewBottomConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ContainerViewLeftConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ContainerViewRightConstraint { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ContainerViewTopConstraint { get; set; }

		[Outlet]
		UIKit.UIButton EmailButton { get; set; }

		[Outlet]
		UIKit.UIImageView MainImageView { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }

		[Outlet]
		UIKit.UILabel SubtitleLabel { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (ActionButton != null) {
				ActionButton.Dispose ();
				ActionButton = null;
			}

			if (MainImageView != null) {
				MainImageView.Dispose ();
				MainImageView = null;
			}

			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (SubtitleLabel != null) {
				SubtitleLabel.Dispose ();
				SubtitleLabel = null;
			}

			if (CallButton != null) {
				CallButton.Dispose ();
				CallButton = null;
			}

			if (EmailButton != null) {
				EmailButton.Dispose ();
				EmailButton = null;
			}

			if (ContainerView != null) {
				ContainerView.Dispose ();
				ContainerView = null;
			}

			if (ContainerViewRightConstraint != null) {
				ContainerViewRightConstraint.Dispose ();
				ContainerViewRightConstraint = null;
			}

			if (ContainerViewLeftConstraint != null) {
				ContainerViewLeftConstraint.Dispose ();
				ContainerViewLeftConstraint = null;
			}

			if (ContainerViewBottomConstraint != null) {
				ContainerViewBottomConstraint.Dispose ();
				ContainerViewBottomConstraint = null;
			}

			if (ContainerViewTopConstraint != null) {
				ContainerViewTopConstraint.Dispose ();
				ContainerViewTopConstraint = null;
			}
		}
	}
}
