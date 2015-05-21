using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using Shared.Common;
using CoreGraphics;
using System.Linq;

namespace iOS.Phone
{
	[Register("PSPullDownControl"), DesignTimeVisible(true)]
	public partial class PSPullDownControl : UIView
	{
		#region Events

		public event EventHandler<CGPoint> DidClose;
		public event EventHandler<CGPoint> WillClose;

		public event EventHandler<CGPoint> DidOpen;
		public event EventHandler<CGPoint> WillOpen;

		public event EventHandler<CGPoint> DidChange;

		public event EventHandler<CGPoint> DidStretch;

		public event EventHandler<CGPoint> DidBounce;
		public event EventHandler<CGPoint> WillBounce;

		#endregion

		#region Private Variables

		private nfloat _firstY = 0;
		private nfloat _firstX = 0;
		private UIView _tempView = null;
		private nfloat _closedOffset = 0;
		private UIImageView _arrowImageView;
		private NSLayoutConstraint _topConstraint;
		private UIColor _colorForExpandableSection;
		private nfloat _animationDuration = 0.25f;
		private bool _stretchEnabled = true;
//		private UINavigationBar _navigationBar;

		#endregion

		#region Member Properties

		public bool IsClosed { get; private set; }
		public nfloat ClosedOffset 
		{ 
			get { return _closedOffset; } 
			set { _closedOffset = value; }
		}

		public UIColor ColorForExpandableSection
		{
			get { return _colorForExpandableSection; }
			set { _colorForExpandableSection = value;}
		}

		public nfloat AnimationDuration
		{
			get { return _animationDuration; }
			set { _animationDuration = value; }
		}

		public bool StretchEnabled
		{
			get { return _stretchEnabled; }
			set { _stretchEnabled = value; }
		}

		#endregion

		#region Methods

		public PSPullDownControl (IntPtr handle) : base(handle) 
		{
			Initialize ();
		}

		public PSPullDownControl () : base ()
		{
			Initialize ();
		}

		public PSPullDownControl (CGRect frame) : base (frame)
		{
			Initialize ();
		}

		protected virtual void Initialize ()
		{
			AddGestureToView (this);

			IsClosed = false;

			_colorForExpandableSection = BackgroundColor;

			_firstX = Frame.Width / 2;
			_firstY = Frame.Height / 2;

			foreach (NSLayoutConstraint constraint in Superview.Constraints)
			{
				if(constraint.FirstAttribute == NSLayoutAttribute.Top)
				{
					if(constraint.FirstItem != null && constraint.FirstItem.Equals(this))
					{
						_topConstraint = constraint;
					}
				}
			}
		}

		public UIPanGestureRecognizer AddGestureToView (UIView viewToAdd)
		{
			var gesture = new UIPanGestureRecognizer (Animate);

			gesture.MinimumNumberOfTouches = 1;
			gesture.MaximumNumberOfTouches = 1;

			viewToAdd.UserInteractionEnabled = true;
			viewToAdd.AddGestureRecognizer (gesture);

			return gesture;
		}

//		public class Temp : UIGestureRecognizerDelegate
//		{
//			private UINavigationBar _navigationBar;
//
//			public Temp (UINavigationBar navigationBar)
//			{
//				_navigationBar = navigationBar;
//			}

//			public override bool ShouldReceiveTouch (UIGestureRecognizer recognizer, UITouch touch)
//			{
//				var res = false; // (touch.View.Class.GetType () != typeof(UIBarButtonItem));
//				var point = touch.LocationInView (_navigationBar);
//
//				if(_navigationBar.BackItem.LeftBarButtonItem.AccessibilityFrame.Contains(point))
//				{
//					res = false;
//				}
//				else
//				{
//					res = true;
//				}
////				if(_navigationBar.BackItem.Con)
//
//				return res;
//			}
//		}

		public void AddCloseButtonToNavigationItem (UINavigationItem navigationItem)
		{
			// Add tint to bar button
			var image = UIImage.FromBundle ("ic_x_white.png");
			image = image.ScaleToSize (new CGSize (10, 10));

			var closeButton = new UIBarButtonItem (image, UIBarButtonItemStyle.Plain, (object sender, EventArgs args) => {
				Close();
			});

			// Listen to close/open events and add/remove correctly
			navigationItem.SetRightBarButtonItem (closeButton, false);

			WillClose += (object sender, CGPoint e) => {
				navigationItem.SetRightBarButtonItem (null, true);
			};

			DidOpen += (object sender, CGPoint e) => {
				navigationItem.SetRightBarButtonItem (closeButton, true);
			};

			DidStretch += (object sender, CGPoint e) => {
				if(navigationItem.RightBarButtonItem == null)
				{
					navigationItem.SetRightBarButtonItem (closeButton, true);	
				}
			};
		}

		public void Animate (UIPanGestureRecognizer gesture)
		{
			var point = gesture.TranslationInView (this);
			var velocity = gesture.VelocityInView (this);

			point = new CGPoint (_firstX, point.Y + _firstY);
			SetNeedsLayout ();
			Superview.SetNeedsLayout ();
			switch (gesture.State)
			{
			case UIGestureRecognizerState.Began:
				_firstX = Center.X;
				_firstY = Center.Y;
				break;
			case UIGestureRecognizerState.Changed:
				
				if(point.Y < Frame.Height/2)
				{
					TrackTouch (point);

					UIView.Animate (AnimationDuration, () => {
						SetArrowAlpha (0);
					});
				}
				else if(point.Y > Frame.Height/2)
				{
					if(StretchEnabled)
						StretchTouch (point, gesture);
				}
				break;
			case UIGestureRecognizerState.Ended:
				if(IsClosed)
				{
					if(velocity.Y > 0)
					{
						Open ();
					}
					else
					{
						Close ();	
					}
				}
				else
				{ 
					if(point.Y <= (Frame.Height/2 - 10))
					{
						if(velocity.Y > 0)
						{
							Open ();
						}
						else
						{
							Close ();	
						}
					} 
					else
					{
						var newPoint = new CGPoint (_firstX, Frame.Height / 2);

						if(WillBounce != null)
						{
							WillBounce (this, newPoint);	
						}
						// Spring Back in place Animation
						UIView.AnimateNotify (0.5, 0, 0.4f, 1, 0, () => {
							
							CalculateAndSetTopConstraint(newPoint);
						}, (res) => {
							if (DidBounce != null)
							{
								DidBounce(this, newPoint);	
							}
						});
					}
				}
				break;
			}				
		}

		public void Open (bool animated = true)
		{
			// Closed-to-open state
			var point = new CGPoint(_firstX, Frame.Height/2);

			if(WillOpen != null)
			{
				WillOpen (this, point);
			}

			if(animated)
			{
				UIView.Animate (AnimationDuration, () => {
					CalculateAndSetTopConstraint(point);
					SetArrowAlpha(0);
				}, () => {
					Opened(point);
				});		
			}
			else
			{
				CalculateAndSetTopConstraint(point);
				SetArrowAlpha(0);
				Opened (point);
			}
		}

		public void Close (bool animated = true)
		{
			var heightConstraint = this.Constraints.FirstOrDefault ( c => c.FirstItem.Equals(this) && c.FirstAttribute == NSLayoutAttribute.Height);

			var point = new CGPoint (_firstX, -1 * heightConstraint.Constant / 2 + ClosedOffset);

			if(WillClose != null)
			{
				WillClose (this, point);
			}

			if(_tempView != null)
			{
				_tempView.RemoveFromSuperview();
				_tempView = null;
			}

			if(animated)
			{
				UIView.Animate (AnimationDuration, () => {
					CalculateAndSetTopConstraint(point);
					SetArrowAlpha(1);
				}, () => {
					Closed(point);
				});		
			}
			else
			{
				CalculateAndSetTopConstraint(point);
				SetArrowAlpha(1);
				Closed (point);
			}
		}

		public void ShowGestureArrow(UINavigationBar navigationBar)
		{
			var image = UIImage.FromFile ("SwipeDownArrow.png");

			_arrowImageView = new UIImageView (new CGRect (navigationBar.Frame.Width/2 - image.Size.Width/2, navigationBar.Frame.Height - image.Size.Height - 3, image.Size.Width, image.Size.Height));
			_arrowImageView.Image = image;

			navigationBar.AddSubview (_arrowImageView);

			_arrowImageView.Alpha = IsClosed ? 1 : 0;
		}

		public void RemoveGestureArrow(UINavigationBar navigationBar)
		{
			if(_arrowImageView != null)
			{
				UIView.Animate (AnimationDuration, () => {
					_arrowImageView.Alpha = 0.0f;	
				}, () => {
					if(_arrowImageView != null)
					{
						_arrowImageView.RemoveFromSuperview ();	
					}
					_arrowImageView = null;	
				});
			}
		}

		private void Opened (CGPoint point)
		{
			IsClosed = false;

			if(DidOpen != null)
			{
				DidOpen (this, point);
			}
		}

		private void CalculateAndSetTopConstraint(CGPoint point)
		{
			var constant = point.Y - Frame.Height/2;

			_topConstraint.Constant = constant;

			Superview.LayoutIfNeeded();
			LayoutIfNeeded ();
		}

		private void Closed (CGPoint point)
		{
			IsClosed = true;

			if(DidClose != null)
			{
				DidClose (this, point);
			}
		}

		private void SetArrowAlpha (nfloat alpha)
		{
			if(_arrowImageView != null)
			{
				_arrowImageView.Alpha = alpha;
			}
		}

		private void TrackTouch (CGPoint point)
		{
			// Follow touch
			if(_tempView != null)
			{
				_tempView.RemoveFromSuperview();
				_tempView = null;
			}
			_topConstraint.Constant = point.Y - Frame.Height/2;

			if(DidChange != null)
			{
				DidChange (this, point);
			}
		}

		private void StretchTouch (CGPoint point, UIPanGestureRecognizer gesture)
		{
			// Put it temp view to make the view look like it's stretching
			if(_tempView == null)
			{
				_tempView = new UIView (new CGRect(Frame.X, Frame.Y, Frame.Width, Frame.Height/4));
				_tempView.BackgroundColor = _colorForExpandableSection;

				Superview.AddSubview (_tempView);
				Superview.BringSubviewToFront (this);
			}

			// Slow down and expand
			var difference = (point.Y - Frame.Height / 2) * 0.1f;
			var newPoint = new CGPoint(_firstX, Frame.Height/2 + difference);

			CalculateAndSetTopConstraint (newPoint);

			if(DidStretch != null)
			{
				DidStretch (this, point);
			}
		}

		#endregion
	}
}

