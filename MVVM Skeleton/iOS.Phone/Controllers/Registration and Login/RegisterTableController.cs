
using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using Shared.Common;
using GalaSoft.MvvmLight.Helpers;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

namespace iOS.Phone
{
	public partial class RegisterTableController : UITableViewController
	{
		private bool _isKeyboardShowing = false;

		public RegisterTableController (IntPtr handle) : base (handle) {}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			PopulateStaticData ();

			InitBindings ();

			_goButton.Enabled = false;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.DidShowNotification, KeyboardDidShow);
			NSNotificationCenter.DefaultCenter.AddObserver (UIKeyboard.WillHideNotification, KeyboardWillHide);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			NSNotificationCenter.DefaultCenter.RemoveObserver (this);
		}

		private void PopulateStaticData()
		{
			_legalFirstNameControl.PlaceholderText = Application.VMStore.RegistrationVM.LegalFirstNamePlaceholder;
			_legalFirstNameControl.EditingDidBegin += OnEditingDidBegin;
			_legalFirstNameControl.MaxCharacterCount = Application.VMStore.RegistrationVM.MaxNameCount;

			_preferredFirstNameControl.PlaceholderText = Application.VMStore.RegistrationVM.PreferredFirstNamePlaceholder;
			_preferredFirstNameControl.DetailDisclosureHidden = false;
			_preferredFirstNameControl.EditingDidBegin += OnEditingDidBegin;
			_preferredFirstNameControl.MaxCharacterCount = Application.VMStore.RegistrationVM.MaxNameCount;

			_lastNameControl.PlaceholderText = Application.VMStore.RegistrationVM.LastNamePlaceholder;
			_lastNameControl.EditingDidBegin += OnEditingDidBegin;
			_lastNameControl.MaxCharacterCount = Application.VMStore.RegistrationVM.MaxNameCount;

			_emailControl.PlaceholderText = Application.VMStore.RegistrationVM.EmailPlaceholder;
			_emailControl.KeyboardType = UIKeyboardType.EmailAddress;
			_emailControl.EditingDidBegin += OnEditingDidBegin;
			_emailControl.MaxCharacterCount = Application.VMStore.RegistrationVM.MaxEmailCount;

			_createPasswordControl.PlaceholderText = Application.VMStore.RegistrationVM.CreatePasswordPlaceholder;
			_createPasswordControl.SecureTextEntry = true;
			_createPasswordControl.DetailDisclosureHidden = false;
			_createPasswordControl.EditingDidBegin += OnEditingDidBegin;

			_confirmPasswordControl.PlaceholderText = Application.VMStore.RegistrationVM.ConfirmPasswordPlaceholder;
			_confirmPasswordControl.SecureTextEntry = true;
			_confirmPasswordControl.EditingDidBegin += OnEditingDidBegin;

			_birthDateControl.PlaceholderText = Application.VMStore.RegistrationVM.BirthdatePlaceholder;
			_birthDateControl.DateMax = Application.VMStore.RegistrationVM.MaxBirthDate.ToNSDate ();
			_birthDateControl.InitDate = Application.VMStore.RegistrationVM.InitBirthDate.ToNSDate ();
			_birthDateControl.DetailDisclosureHidden = false;
			_birthDateControl.EditingDidBegin += OnEditingDidBegin;

			_ssnControl.PlaceholderText = Application.VMStore.RegistrationVM.LastFourSocialPlaceholder;
			_ssnControl.KeyboardType = UIKeyboardType.NumberPad;
			_ssnControl.SecureTextEntry = true;
			_ssnControl.MaxCharacterCount = Application.VMStore.RegistrationVM.LengthSocial;
			_ssnControl.DetailDisclosureHidden = false;
			_ssnControl.EditingDidBegin += OnEditingDidBegin;

			_genderControl.PlaceholderText = Application.VMStore.RegistrationVM.GenderPlaceholder;
			_genderControl.PickerOptions = Application.VMStore.RegistrationVM.GenderData;
			_genderControl.EditingDidBegin += OnEditingDidBegin;
//			_genderControl.TextChanged += OnTextChanged;

			_goButton.SetBackgroundImage (SharedColors.Gray3.ToUIColor().ToImage (_goButton.Bounds), UIControlState.Disabled);
			_goButton.SetBackgroundImage (SharedColors.Orange.ToUIColor().ToImage (_goButton.Bounds), UIControlState.Normal);

			Application.VMStore.RegistrationVM.CanExecute += OnCanExecute;
		}

		private void OnCanExecute (object sender, EventArgs args)
		{
			var canExecuteArgs = (CanExecuteEventArgs)args;
			_goButton.Enabled = canExecuteArgs.CanExecute;
		}

		private async void OnEditingDidBegin (object sender, EventArgs args)
		{
			var control = (PSFloatingTextControl)sender;
			var tag = control.Tag;
			var rowCount = TableView.NumberOfRowsInSection (0);
			var nextRow = tag + 1 <= rowCount - 1 ? tag + 1 : rowCount - 1;
			var indexPath = NSIndexPath.FromRowSection (nextRow, 0);

			if(!_isKeyboardShowing)
			{
				await Task.Delay (700);
			}

			TableView.ScrollToRow (indexPath, UITableViewScrollPosition.None, true);
		}

		private void KeyboardDidShow(NSNotification notification)
		{
			_isKeyboardShowing = true;
			var keyboardFrame = UIKeyboard.FrameEndFromNotification (notification);

			Device.KeyboardHeight = keyboardFrame.Height;
		}

		private void KeyboardWillHide(NSNotification notification)
		{
			_isKeyboardShowing = false;
		}

//		private void TextChanged (object sender, EventArgs args)
//		{
//			var control = (PSFloatingTextControl)sender;
//
//			foreach(Button button in View.Subviews)
//			{
//				var canRegister = canregisterBL.Validate (control.Text);
//			}
//
//			_goButton.Enabled = canRegister;
//		}

		private void InitBindings()
		{

//			var valid = registerBL.Validate ();


			this.SetBinding (
				() => _legalFirstNameControl.Text,
				() => Application.VMStore.RegistrationVM.LegalFirstName
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _preferredFirstNameControl.Text,
				() => Application.VMStore.RegistrationVM.PreferredFirstName,
				BindingMode.TwoWay
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _lastNameControl.Text,
				() => Application.VMStore.RegistrationVM.LastName
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _emailControl.Text,
				() => Application.VMStore.RegistrationVM.Email
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _createPasswordControl.Text,
				() => Application.VMStore.RegistrationVM.Password
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _birthDateControl.Text,
				() => Application.VMStore.RegistrationVM.Birthdate
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _ssnControl.Text,
				() => Application.VMStore.RegistrationVM.Social
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _genderControl.Text,
				() => Application.VMStore.RegistrationVM.Gender
			).UpdateSourceTrigger ("TextChanged");

			this.SetBinding (
				() => _confirmPasswordControl.Text,
				() => Application.VMStore.RegistrationVM.ConfirmPassword
			).UpdateSourceTrigger ("TextChanged");

			_preferredFirstNameControl.SetCommand ("DetailTapped", Application.VMStore.RegistrationVM.PreferredFirstNameCommand);

			_createPasswordControl.SetCommand ("DetailTapped", Application.VMStore.RegistrationVM.CreatePasswordCommand);

			_birthDateControl.SetCommand ("DetailTapped", Application.VMStore.RegistrationVM.BirthDateCommand);

			_ssnControl.SetCommand ("DetailTapped", Application.VMStore.RegistrationVM.SSNCommand);

			_goButton.SetCommand("TouchUpInside", Application.VMStore.RegistrationVM.RegisterCommand);

			_cancelButton.SetCommand("TouchUpInside", Application.VMStore.RegistrationVM.CancelCommand);
		}
	}
}

