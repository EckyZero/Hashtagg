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
	[Register ("SettingsController")]
	partial class SettingsController
	{
		[Outlet]
		UIKit.UIButton EditUsernameButton { get; set; }

		[Outlet]
		UIKit.UIButton InviteFamilyMemberButton { get; set; }

		[Outlet]
		UIKit.UIButton LogoutButton { get; set; }

		[Outlet]
		UIKit.UIButton ResetPasswordButton { get; set; }

		[Outlet]
		UIKit.UIButton ResetPINButton { get; set; }

		[Outlet]
		UIKit.UIButton SupportButton { get; set; }

		[Outlet]
		UIKit.UILabel UsernameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (EditUsernameButton != null) {
				EditUsernameButton.Dispose ();
				EditUsernameButton = null;
			}

			if (InviteFamilyMemberButton != null) {
				InviteFamilyMemberButton.Dispose ();
				InviteFamilyMemberButton = null;
			}

			if (LogoutButton != null) {
				LogoutButton.Dispose ();
				LogoutButton = null;
			}

			if (ResetPasswordButton != null) {
				ResetPasswordButton.Dispose ();
				ResetPasswordButton = null;
			}

			if (ResetPINButton != null) {
				ResetPINButton.Dispose ();
				ResetPINButton = null;
			}

			if (UsernameLabel != null) {
				UsernameLabel.Dispose ();
				UsernameLabel = null;
			}

			if (SupportButton != null) {
				SupportButton.Dispose ();
				SupportButton = null;
			}
		}
	}
}
