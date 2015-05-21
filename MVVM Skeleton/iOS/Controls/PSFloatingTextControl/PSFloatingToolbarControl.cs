using System;
using UIKit;

namespace iOS
{
	public abstract class PSFloatingToolbarControl : PSFloatingTextControl
	{
		#region Events

		public event EventHandler Cancel;

		#endregion

		#region Private Properties

		protected UIToolbar _toolbar;
		protected UIBarButtonItem _doneButton;
		protected UIBarButtonItem _cancelButton;

		#endregion

		#region Constructors

		protected PSFloatingToolbarControl (IntPtr handle) : base (handle) 
		{
			InitToolbar ();

			TextField.InputAccessoryView = _toolbar;
			TextField.TintColor = UIColor.Clear;
		}

		#endregion 

		#region Methods

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			if(_doneButton != null && _cancelButton != null)
			{
				var color = UIColor.FromRGB (61.0f / 255.0f, 137.0f / 255.0f, 204.0f / 255.0f);

				_doneButton.TintColor = color;
				_cancelButton.TintColor = color;	
			}
		}

		protected abstract void OnDoneButtonTapped(object sender, EventArgs args);

		private void InitToolbar ()
		{
			_toolbar = new UIToolbar ();
			_toolbar.SizeToFit();

			_doneButton = new UIBarButtonItem ("Done", UIBarButtonItemStyle.Plain, OnDoneButtonTapped);
			_cancelButton = new UIBarButtonItem ("Cancel", UIBarButtonItemStyle.Plain, OnCancelButtonTapped);

			UIBarButtonItem btnFlexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace, null);
			UIBarButtonItem[] btnItems = new UIBarButtonItem[] { _cancelButton, btnFlexibleSpace, _doneButton }; 

			_toolbar.SetItems(btnItems, true);
		}

		private void OnCancelButtonTapped (object sender, EventArgs args)
		{
			TextField.ResignFirstResponder ();

			if(Cancel != null)
			{
				Cancel (this, args);
			}
		}

		#endregion
	}
}

