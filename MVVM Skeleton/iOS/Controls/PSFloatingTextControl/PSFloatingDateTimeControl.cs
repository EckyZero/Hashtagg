using System;
using UIKit;
using Foundation;
using System.ComponentModel;

namespace iOS
{
	[Register ("PSFloatingDateTimeControl"), DesignTimeVisible(true)]
	public class PSFloatingDateTimeControl : PSFloatingToolbarControl
	{
		public event EventHandler DatePickerSelection;

		#region Public Properties

		public string DateFormat { get; set; }

		public UIDatePickerMode DateMode
		{
			get { return _datePicker.Mode; }
			set { _datePicker.Mode = value; }
		}

		public NSDate InitDate
		{
			get { return _datePicker.Date; }
			set 
			{ 
				_datePicker.Date = value;
			}
		}

		public NSDate DateMax
		{
			get { return _datePicker.MaximumDate; }
			set 
			{ 
				_datePicker.MaximumDate = value; 
				_datePicker.Date = SubtractFrom (value, 1);
			}
		}

		public NSDate DateMin 
		{ 
			get { return _datePicker.MinimumDate; } 
			set { _datePicker.MinimumDate = value; }
		}

		#endregion

		#region Private Properties

		private UIDatePicker _datePicker;

		#endregion

		#region Constructors

		public PSFloatingDateTimeControl (IntPtr handle) : base (handle) 
		{
			InitDatePicker ();

			TextField.InputView = _datePicker;
			DateFormat = "MM/dd/yyyy";
		}

		#endregion

		#region Methods

		private void InitDatePicker ()
		{
			// Minimum Date
			NSCalendar calendar = NSCalendar.CurrentCalendar;
			NSDateComponents components = calendar.Components (NSCalendarUnit.Year | NSCalendarUnit.Month | NSCalendarUnit.Day, NSDate.Now);

			components.Year = components.Year - 125;

			_datePicker = new UIDatePicker ();

			_datePicker.Mode = UIDatePickerMode.Date;
			_datePicker.MaximumDate = DateMax;
			_datePicker.MinimumDate = DateMin;
			_datePicker.MinimumDate = calendar.DateFromComponents (components);
			_datePicker.MaximumDate = NSDate.Now;
			_datePicker.Date = SubtractFrom (DateMax, 1);
		}

		private NSDate SubtractFrom(NSDate date, nint years)
		{
			NSCalendar calendar = NSCalendar.CurrentCalendar;
			NSDateComponents components = calendar.Components (NSCalendarUnit.Year, date);

			components.Year = components.Year - years;

			NSDate newDate = calendar.DateFromComponents (components);

			return newDate;
		}

		protected override void OnDoneButtonTapped (object sender, EventArgs args) 
		{
			NSDateFormatter formatter = new NSDateFormatter ();
			PSFloatingDateTimeControlEventArgs psArgs = new PSFloatingDateTimeControlEventArgs (_datePicker.Date.ToDateTime ());

			formatter.DateFormat = DateFormat;
			TextField.Text = formatter.StringFor (_datePicker.Date);
			TextField.ResignFirstResponder ();

			OnEditingChanged (TextField, null);

			if(DatePickerSelection != null)
			{
				DatePickerSelection (this, psArgs);
			}
		}

		#endregion
	}
}