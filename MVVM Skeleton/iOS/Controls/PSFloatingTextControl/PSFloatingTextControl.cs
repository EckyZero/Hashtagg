using System;
using Foundation;
using UIKit;
using ObjCRuntime;
using CoreGraphics;
using CoreAnimation;

namespace iOS
{
	public partial class PSFloatingTextControl : UIView
	{
		#region Events

		public event EventHandler ShouldReturn;

		public event EventHandler TextChanged;

		public event EventHandler DetailTapped;

		public event EventHandler EditingDidBegin;

		#endregion

		#region Private Variables

		private nint _maxCharacterCount = 100;

		private bool _initialSecureTextEntryState = false;

		private bool _initialDetailDisclosureHiddenState = true;

		private nfloat _topBottomBorderWidth = 1.0f;

		private UIColor _topBottomBorderColor = UIColor.FromRGB(247.0f/255.0f,243.0f/255.0f,222.0f/255.0f);

		protected UITextField TextField { get { return _textField; }}

		#endregion

		#region Member Properties

		public string Text
		{
			get { return _textField.Text; }
			set { 
				_textField.Text = value;
				OnEditingChanged (_textField, new EventArgs ());
			}
		}

		public string PlaceholderText
		{
			get { return _label.Text; }
			set 
			{ 
				_label.Text = value;
				_textField.Placeholder = value;
			}
		}

		public nint MaxCharacterCount 
		{
			get{ return _maxCharacterCount;}
			set { _maxCharacterCount = value; }
		}

		public UIFont TextFont
		{
			get { return _textField.Font; }
			set { _textField.Font = value; }
		}

		public UIFont PlaceholderFont
		{
			get { return _label.Font; }
			set { _label.Font = value; }
		}

		public UIColor TextColor
		{
			get { return _textField.TextColor; }
			set { _textField.TextColor = value; }
		}

		public UIColor PlaceholderTextColor
		{
			get { return _label.TextColor; }
			set { _label.TextColor = value; }
		}
			
		public bool SecureTextEntry
		{
			get { return _textField.SecureTextEntry; }
			set 
			{ 
				_textField.SecureTextEntry = value;
				_initialSecureTextEntryState = value;
			}
		}

		public UIKeyboardType KeyboardType
		{
			get { return _textField.KeyboardType; }
			set { _textField.KeyboardType = value; }
		}
			
		public bool DetailDisclosureHidden 
		{ 
			get { return _initialDetailDisclosureHiddenState; } 
			set 
			{ 
				_detailButton.Hidden = value; 
				_initialDetailDisclosureHiddenState = value;
			}
		}

		public UIImage ClearImage
		{
			get { return _clearButton.ImageForState (UIControlState.Normal); }
			set { _clearButton.SetImage (value, UIControlState.Normal); }
		}

		public UIImage DetailImage
		{
			get { return _detailButton.ImageForState (UIControlState.Normal); }
			set { _detailButton.SetImage (value, UIControlState.Normal); }
		}

		public nfloat TopBottomBorderWidth
		{
			get { return _topBottomBorderWidth; }
			set { _topBottomBorderWidth = value;}
		}

		public UIColor TopBottomBorderColor
		{
			get { return _topBottomBorderColor; }
			set { _topBottomBorderColor = value;}
		}

		public bool ShouldNotAnimateOnEditing {
			get;
			set;
		}

		#endregion

		#region Constructors

		private PSFloatingTextControl() {}

		public PSFloatingTextControl (IntPtr handle) : base (handle) 
		{
			var nibs = NSBundle.MainBundle.LoadNib("PSFloatingTextControl", this, null);
			var view = Runtime.GetNSObject(nibs.ValueAt(0)) as UIView;

			view.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);

			AddSubview (view);

			_textField.EditingChanged += OnEditingChanged;
			_textField.ShouldReturn += OnShouldReturn;
			_textField.ShouldChangeCharacters += OnShouldChangeCharactersInRange;
			_textField.EditingDidBegin += OnEditingDidBegin;
			_textField.EditingDidEnd += OnEditingDidEnd;

			_detailButton.Hidden = true;
			_detailButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			_detailButton.TouchUpInside += OnDetailButtonTapped;

			_showButton.Hidden = true;
			_showButton.TouchUpInside += OnShowButtonTapped;

			_hideButton.Hidden = true;
			_hideButton.TouchUpInside += OnHideButtonTapped;

			_clearButton.Hidden = true;
			_clearButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			_clearButton.TouchUpInside += OnClearButtonTapped;

			ClearImage = UIImage.FromFile ("clear");
			DetailImage = UIImage.FromFile("Tooltip");

			// Add borders
			CALayer topBorder = new CALayer ();
			topBorder.BackgroundColor = _topBottomBorderColor.CGColor;
			topBorder.Frame = new CGRect (0, 0, _view.Frame.Size.Width, _topBottomBorderWidth);

			Layer.AddSublayer (topBorder);

			CALayer bottomBorder = new CALayer ();
			bottomBorder.BackgroundColor = _topBottomBorderColor.CGColor;
			bottomBorder.Frame = new CGRect (0, _view.Frame.Size.Height - _topBottomBorderWidth, _view.Frame.Size.Width, _topBottomBorderWidth);

			Layer.AddSublayer (bottomBorder);

			// TODO: Reference a real color/font file
			var firstAttributes = new UIStringAttributes {
				ForegroundColor = UIColor.FromRGB (169.0f / 255.0f, 169.0f / 255.0f, 169.0f / 255.0f),
				Font = UIFont.FromName("CenturyGothic", 14f)
			};
			_textField.Font = UIFont.FromName ("CenturyGothic", 14);
			_textField.AttributedPlaceholder = new NSAttributedString (_textField.Placeholder, firstAttributes);

			_label.Font = UIFont.FromName ("CenturyGothic-Bold",10);
		}

		#endregion 

		#region Methods

		public override bool ResignFirstResponder ()
		{
			return _textField.ResignFirstResponder ();
		}

		public override bool BecomeFirstResponder()
		{
			return _textField.BecomeFirstResponder ();
		}

		private void ShowButton (UIButton button)
		{
			_clearButton.Hidden = true;
			_detailButton.Hidden = true;
			_hideButton.Hidden = true;
			_showButton.Hidden = true;

			if(button != null)
			{
				button.Hidden = false;
				BringSubviewToFront (button);
			}
		}
			
		private void PickButtonToShow ()
		{
			if(_textField.IsFirstResponder)
			{
				if(SecureTextEntry)
				{
					ShowButton (_showButton);
				}
				else if (_initialSecureTextEntryState)
				{
					ShowButton (_hideButton);
				}
				else
				{
					ShowButton (_clearButton);
				}
			}
		}

		private void OnEditingDidBegin (object sender, EventArgs args)
		{
			UITextField textField = (UITextField)sender;

			if(!string.IsNullOrWhiteSpace (textField.Text))
			{
				PickButtonToShow ();
			}
			_label.TextColor = UIColor.FromRGB (61.0f / 255.0f, 137.0f / 255.0f, 204.0f / 255.0f);

			if(EditingDidBegin != null)
			{
				EditingDidBegin (this, args);
			}
		}

		private void OnEditingDidEnd (object sender, EventArgs args)
		{
			SecureTextEntry = _initialSecureTextEntryState;

			ShowButton (null);

			_detailButton.Hidden = _initialDetailDisclosureHiddenState;
			_label.TextColor = UIColor.FromRGB (169.0f / 255.0f, 169.0f / 255.0f, 169.0f / 255.0f);
		}

		protected async void OnEditingChanged (object sender, EventArgs args)
		{
			UITextField textField = (UITextField)sender;
			_label.Text = _textField.Placeholder;
			var alpha = 0;
			var hide = string.IsNullOrWhiteSpace (textField.Text);

			if(hide)
			{
				ShowButton (null);

				_detailButton.Hidden = _initialDetailDisclosureHiddenState;

				_textFieldCenterYConstraint.Constant = 0;
				_labelCenterYConstraint.Constant = 0;
			}
			else
			{
				PickButtonToShow ();

				_textFieldCenterYConstraint.Constant = -9;
				_labelCenterYConstraint.Constant = 9;
				alpha = 1;
			}

			if (ShouldNotAnimateOnEditing) {
				_view.LayoutIfNeeded ();
				_label.Alpha = alpha;
			} else {
				await UIView.AnimateAsync (0.5, () => {
					_view.LayoutIfNeeded ();
					_label.Alpha = alpha;
				});
			}

			// refire
			if (TextChanged != null) {
				TextChanged (sender, args);
			}
		}

		private bool OnShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder ();

			if(ShouldReturn != null)
			{
				ShouldReturn (this, new EventArgs());
			}
			return true;
		}

		private bool OnShouldChangeCharactersInRange (UITextField textField, NSRange range, string replacementString)
		{
			var oldLength = textField.Text.Length;
			var replacementLength = replacementString.Length;
			var rangeLength = range.Length;
			var newLength = oldLength - rangeLength + replacementLength;

			return newLength <= _maxCharacterCount;
		}

		private void OnClearButtonTapped (object sender, EventArgs args)
		{
			_textField.Text = "";

			OnEditingChanged (_textField, new EventArgs ());
		}

		private void OnShowButtonTapped (object sender, EventArgs args)
		{
			var tmpString = _textField.Text;

			ShowButton (_hideButton);

			_textField.SecureTextEntry = false;
			_textField.Text = " ";
			_textField.Text = tmpString;
		}

		private void OnHideButtonTapped(object sender, EventArgs args)
		{
			var tmpString = _textField.Text;

			ShowButton (_showButton);

			_textField.SecureTextEntry = true;
			_textField.Text = " ";
			_textField.Text = tmpString;
		}

		private void OnDetailButtonTapped (object sender, EventArgs args)
		{
			if(DetailTapped != null)
			{
				DetailTapped (this, args);
			}
		}

		public override bool IsFirstResponder {
			get {
				return _textField.IsFirstResponder;
			}
		}

		#endregion
	}
}