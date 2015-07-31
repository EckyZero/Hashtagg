using System;
using UIKit;
using System.Drawing;
using Foundation;
using System.Timers;
using CoreGraphics;

namespace iOS
{
	public partial class UIScrollViewImage: UIScrollView
	{
		/// <summary>
		/// The double tap interval. Measured in ms
		/// </summary>

		const float defaultZoom = 2f;
		const float minZoom = 0.1f;
		const float maxZoom = 3f;
		nfloat sizeToFitZoom = 1f;

		UIImageView ivMain;

		UITapGestureRecognizer grTap;
		UITapGestureRecognizer grDoubleTap;

		public event Action OnSingleTap;

		public UIScrollViewImage (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		public UIScrollViewImage () : base ()
		{
			Initialize ();
		}

		private void Initialize ()
		{
			AutoresizingMask = UIViewAutoresizing.All;

			ivMain = new UIImageView ();
			//			ivMain.AutoresizingMask = UIViewAutoresizing.All;
			ivMain.ContentMode = UIViewContentMode.ScaleAspectFit;
			//			ivMain.BackgroundColor = UIColor.Red; // DEBUG
			AddSubview (ivMain);

			// Setup zoom
			MaximumZoomScale = maxZoom;
			MinimumZoomScale = minZoom;
			ShowsVerticalScrollIndicator = false;
			ShowsHorizontalScrollIndicator = false;
			BouncesZoom = true;
			ViewForZoomingInScrollView += (UIScrollView sv) => {
				return ivMain;
			};

			// Setup gestures
			grTap = new UITapGestureRecognizer (() => {
				if (OnSingleTap != null) {
					OnSingleTap();
				}
			});
			grTap.NumberOfTapsRequired = 1;
			AddGestureRecognizer (grTap);

			grDoubleTap = new UITapGestureRecognizer (() => {
				if (ZoomScale >= defaultZoom) {
					SetZoomScale (sizeToFitZoom, true);
				} else {
					//SetZoomScale (defaultZoom, true);

					// Zoom to user specified point instead of center
					var point = grDoubleTap.LocationInView(grDoubleTap.View);
					var zoomRect = GetZoomRect(defaultZoom, point);
					ZoomToRect(zoomRect, true);
				}
			});
			grDoubleTap.NumberOfTapsRequired = 2;
			AddGestureRecognizer (grDoubleTap);

			// To use single tap and double tap gesture recognizers together. See for reference:
			// http://stackoverflow.com/questions/8876202/uitapgesturerecognizer-single-tap-and-double-tap
			grTap.RequireGestureRecognizerToFail (grDoubleTap);
		}

		public void SetImage(UIImage image)
		{
			ZoomScale = 1;
			ivMain.Image = image;
			ivMain.Frame = new CGRect (new CGPoint(), image.Size);
			ContentSize = image.Size;

			double wScale = Frame.Width / image.Size.Width;
			double hScale = Frame.Height / image.Size.Height;

			MinimumZoomScale = (nfloat)Math.Min(wScale, hScale);
			sizeToFitZoom = MinimumZoomScale;
			ZoomScale = MinimumZoomScale;

			//
			ivMain.Frame = CenterScrollViewContents ();
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			ivMain.Frame = CenterScrollViewContents ();
		}

		public override CGRect Frame
		{
			get
			{
				return base.Frame;
			}
			set
			{
				base.Frame = value;
				
				if (ivMain != null) {
					ivMain.Frame = value;
				}
			}
		}

		public CGRect ImageFrame {
			get { return ivMain.Frame; }
		}

		public CGRect CenterScrollViewContents ()
		{
			var boundsSize = Bounds.Size;
			var contentsFrame = ivMain.Frame;

			if (contentsFrame.Width < boundsSize.Width) {
				contentsFrame.X = (boundsSize.Width - contentsFrame.Width) / 2;
			} else {
				contentsFrame.X = 0;
			}

			if (contentsFrame.Height < boundsSize.Height) {
				contentsFrame.Y = (boundsSize.Height - contentsFrame.Height) / 2;
			} else {
				contentsFrame.Y = 0;
			}

			return contentsFrame;
		}

		// Reference:
		// http://stackoverflow.com/a/11003277/548395
		CGRect GetZoomRect(nfloat scale, CGPoint center)
		{
			var size = new CGSize (ivMain.Frame.Size.Height / scale, ivMain.Frame.Size.Width / scale);

			var center2 = ConvertPointToView(center, ivMain);
			var location2 = new CGPoint(center2.X - (size.Width / 2.0f),
			                           center2.Y - (size.Height / 2.0f)
			                          );

			var zoomRect = new CGRect(location2, size);
			return zoomRect;
		}
	}
}

