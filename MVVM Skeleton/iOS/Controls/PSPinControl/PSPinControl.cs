using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;
using CoreAnimation;
using System.Threading.Tasks;

namespace iOS
{
	[Register("PSPinControl"), DesignTimeVisible(true)]
	public partial class PSPinControl : UIView
	{
		#region Events

		public event EventHandler DidFinish;

		public event EventHandler TextChanged;

		#endregion

		#region Private Variables

		private UIImage _currentPinImage = new UIImage("pin_current.png");
		private UIImage _defaultPinImage = new UIImage("pin_default.png");
		private UIImage _donePinImage = new UIImage("pin_done.png");

		#endregion

		#region Member Properties

		public string Text 
		{
			get { return _textField.Text; }
			set 
			{ 
				_textField.Text = value; 
				OnEditingChanged (_textField, new EventArgs());			
			}
		}

		public UIImage DonePinImage
		{
			get { return _donePinImage; }
			set 
			{ 
				_donePinImage = value; 
				OnEditingChanged (_textField, new EventArgs());
			}
		}

		public UIImage CurrentPinImage
		{
			get { return _currentPinImage; }
			set 
			{ 
				_currentPinImage = value; 
				OnEditingChanged (_textField, new EventArgs());
			}
		}

		public UIImage DefaultPinImage
		{
			get { return _defaultPinImage; }
			set 
			{ 
				_defaultPinImage = value; 
				OnEditingChanged (_textField, new EventArgs());
			}
		}

		public UIKeyboardType KeyboardType
		{
			get { return _textField.KeyboardType; }
			set { _textField.KeyboardType = value; }
		}

		public UIView InputAccessoryView
		{ 
			get { return _textField.InputAccessoryView; } 
			set { _textField.InputAccessoryView = value; } 
		}

		#endregion

		#region Constructors

		private PSPinControl() {}

		public PSPinControl (IntPtr handle) : base (handle) 
		{
			var nib = NSBundle.MainBundle.LoadNib("PSPinControl", this, null);
			var view = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;

			view.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);

			AddSubview (view);

			//C5C5B9
			_textField.EditingChanged += OnEditingChanged;
			var defaultColor = UIColor.FromRGB (85.0f / 255, 85.0f / 255, 85.0f / 255);
			var doneColor = UIColor.FromRGB (197.0f / 255, 197.0f / 255, 185.0f / 255);
			var currentColor = UIColor.FromRGB (239.0f / 255, 75.0f / 255, 36.0f / 255);

			DefaultPinImage = DefaultPinImage.AddColor (defaultColor);
			CurrentPinImage = CurrentPinImage.AddColor (currentColor);
			DonePinImage = DonePinImage.AddColor (doneColor);
		}
			
		#endregion

		#region Methods

		// Required to force loading the bundle
		public static void  InitializeInView (UIView view) 
		{
			var temp = new PSPinControl ();

			view.AddSubview (temp);
			temp.RemoveFromSuperview ();
		}

		public override bool BecomeFirstResponder ()
		{
			return _textField.BecomeFirstResponder ();
		}

		public override bool ResignFirstResponder ()
		{
			return _textField.ResignFirstResponder ();
		}
			
		public async Task Shake()
		{
			await AnimateHorizontalMovementAsync(-10);
			await AnimateHorizontalMovementAsync(20);
			await AnimateHorizontalMovementAsync(-20);
			await AnimateHorizontalMovementAsync(20);
			await AnimateHorizontalMovementAsync(-15);
			await AnimateHorizontalMovementAsync(10);
			await AnimateHorizontalMovementAsync(-5);
			await AnimateHorizontalMovementAsync(0);
		}

		private async Task AnimateHorizontalMovementAsync(float horizontalOffset)
		{
			_centerXConstraint.Constant = horizontalOffset;

			await UIView.AnimateAsync(0.065, () => 
				_view.LayoutIfNeeded()
			);
		}
			
		private void OnEditingChanged (object sender, EventArgs args)
		{
			var count = _textField.Text.Length;
			_firstWidthConstraint.Constant = 15;
			_secondWidthConstraint.Constant = 15;
			_thirdWidthConstraint.Constant = 15;
			_fourthWidthConstraint.Constant = 15;

			// refire
			if (TextChanged != null) {
				TextChanged (this, args);
			}

			if(count == 0)
			{
				_firstImageView.Image = _currentPinImage;
				_secondImageView.Image = _defaultPinImage;
				_thirdImageView.Image = _defaultPinImage;
				_fourthImageView.Image = _defaultPinImage;

				_firstWidthConstraint.Constant = 20;
			}
			else if(count == 1)
			{
				_firstImageView.Image = _donePinImage;
				_secondImageView.Image = _currentPinImage;
				_thirdImageView.Image = _defaultPinImage;
				_fourthImageView.Image = _defaultPinImage;

				_secondWidthConstraint.Constant = 20;
			}
			else if (count == 2)
			{
				_firstImageView.Image = _donePinImage;
				_secondImageView.Image = _donePinImage;
				_thirdImageView.Image = _currentPinImage;
				_fourthImageView.Image = _defaultPinImage;

				_thirdWidthConstraint.Constant = 20;
			}
			else if (count == 3)
			{
				_firstImageView.Image = _donePinImage;
				_secondImageView.Image = _donePinImage;
				_thirdImageView.Image = _donePinImage;
				_fourthImageView.Image = _currentPinImage;

				_fourthWidthConstraint.Constant = 20;
			}
			else if (count == 4)
			{
				_firstImageView.Image = _donePinImage;
				_secondImageView.Image = _donePinImage;
				_thirdImageView.Image = _donePinImage;
				_fourthImageView.Image = _donePinImage;

				if(DidFinish != null)
				{
					DidFinish (this, new PSPinControlEventArgs(_textField.Text));
				}
			}
			else if (count > 4)
			{
				_textField.Text = _textField.Text.Substring (0, 4);
			}
		}

		#endregion
	}
}

