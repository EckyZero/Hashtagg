using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;
using CoreAnimation;

namespace iOS.Phone
{
	[Register("PSAlertBarButtonControl"), DesignTimeVisible(true)]
	public partial class PSAlertBarButtonControl : UIView
	{
		#region Private Properties

		UIBarButtonItem _barButtonItem;

		#endregion

		#region Events

		public event EventHandler Clicked;

		#endregion

		#region Member Properties

		public UIView View { get; set; }

		public int Count 
		{
			get { return int.Parse(CountLabel.Text); }
			set 
			{ 
				CountLabel.Text = value.ToString (); 
				BadgeView.Hidden = (value == 0);
			}
		}

		public UIImage Image
		{
			get { return MainImageView.Image; }
			set { MainImageView.Image = value; }
		}

		public UIColor BadgeBackgroundColor
		{
			get { return BadgeView.BackgroundColor; }
			set { BadgeView.BackgroundColor = value; }
		}

		public UIColor BadgeBorderColor
		{
			get { return UIColor.FromCGColor(BadgeView.Layer.BorderColor); }
			set { BadgeView.Layer.BorderColor = value.CGColor; }
		}

		public nfloat BadgeBorderWidth
		{
			get { return BadgeView.Layer.BorderWidth; }
			set { BadgeView.Layer.BorderWidth = value; }
		}

		#endregion

		#region Methods

		public PSAlertBarButtonControl(int count = 0) : base ()
		{
			Initialize (count);
		}

		protected PSAlertBarButtonControl (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		private void Initialize (int count = 0)
		{
			var nib = NSBundle.MainBundle.LoadNib("PSAlertBarButtonControl", this, null);

			View = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;

			BadgeView.Layer.CornerRadius = BadgeView.Frame.Height / 2;
			BadgeBorderWidth = 1;
			BadgeBorderColor = UIColor.White;

			Count = count;
		}

		public UIBarButtonItem ToUIBarButtonItem ()
		{
			if(_barButtonItem == null)
			{
				_barButtonItem = new UIBarButtonItem (View);
				var tap = new UITapGestureRecognizer (() => {
					if(Clicked != null)
					{
						Clicked(this, new EventArgs());
					}	
				});
				tap.NumberOfTapsRequired = 1;
				View.AddGestureRecognizer (tap);
			}
			return _barButtonItem;
		}

		public void SetCount (int count, bool animated = true)
		{
			if(animated || count == 0)
			{
				CAKeyFrameAnimation animation = CAKeyFrameAnimation.FromKeyPath ("transform");

				animation.Values = new NSObject[] { 
					NSValue.FromCATransform3D (CATransform3D.MakeTranslation (0.0f, -5.0f, 0.0f)),
					NSValue.FromCATransform3D (CATransform3D.MakeTranslation (0.0f, 4.0f, 0.0f)),
					NSValue.FromCATransform3D (CATransform3D.MakeTranslation (0.0f, -2.0f, 0.0f)),
					NSValue.FromCATransform3D (CATransform3D.MakeTranslation (0.0f, 0.0f, 0.0f))
				};
				animation.AutoReverses = false;
				animation.RepeatCount = 1.0f;
				animation.Duration = 0.5f;
				animation.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseInEaseOut) ;
				animation.AnimationStopped += (sender, e) => Count = count;

				View.Layer.AddAnimation (animation, null);	
			}
			else
			{
				Count = count;
				CountLabel.SizeToFit ();
			}
		}

		#endregion
	}
}

