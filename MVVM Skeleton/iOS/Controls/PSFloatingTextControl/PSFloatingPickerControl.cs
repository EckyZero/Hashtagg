using System;
using System.Collections.Generic;
using UIKit;
using System.ComponentModel;
using Foundation;

namespace iOS
{
	[Register ("PSFloatingPickerControl"), DesignTimeVisible(true)]
	public class PSFloatingPickerControl : PSFloatingToolbarControl
	{
		#region Events

		public event EventHandler PickerSelection;

		#endregion

		#region Member Properties

		public IList<string> PickerOptions { 
			get { return _pickerOptions; }
			set 
			{
				_pickerOptions = value;
				_picker.Model = new PickerModel (_pickerOptions);
			} 
		}

		#endregion

		#region Private Properties

		private IList<string> _pickerOptions;
			
		private UIPickerView _picker = new UIPickerView(); 

		#endregion

		#region Constructors

		public PSFloatingPickerControl (IntPtr handle) : base (handle) 
		{
			TextField.InputView = _picker;

			DetailImage = UIImage.FromBundle ("Downarrow");
			DetailDisclosureHidden = false;
		}

		#endregion

		#region Methods

		public void SelectRow  (int row, bool animated = true) 
		{
			_picker.Select ((nint)row, 0, animated);
		}

		protected override void OnDoneButtonTapped (object sender, EventArgs args) 
		{
			var index = _picker.SelectedRowInComponent (0);
			PSFloatingPickerControlEventArgs psArgs = new PSFloatingPickerControlEventArgs ((int)index);
			TextField.Text = PickerOptions [(int)index];
			TextField.ResignFirstResponder ();

			OnEditingChanged (TextField, null);

			if(PickerSelection != null)
			{
				PickerSelection (this, psArgs);
			}
		}

		#endregion

		private class PickerModel : UIPickerViewModel
		{
			private IList<string> _values;

			public PickerModel (IList<string> values)
			{
				_values = values;
			}

			public override nint GetComponentCount (UIPickerView picker)
			{
				return 1;
			}

			public override nint GetRowsInComponent (UIPickerView picker, nint component)
			{
				return _values.Count;
			}

			public override string GetTitle (UIPickerView picker, nint row, nint component)
			{
				var value = _values [(int)row];

				return value;
			}
		}

		public override bool IsFirstResponder {
			get {
				return TextField.IsFirstResponder;
			}
		}
	}
}