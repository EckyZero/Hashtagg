using System;
using UIKit;

namespace iOS
{
	public partial class UIViewFullscreen: UIView
	{
		UIImage iImage;
		UIScrollViewImage sviMain;

		public bool UseAnimation = true;
		public float AnimationDuration = 0.3f;

		public UIViewFullscreen () : base ()
		{
			Initialize();
		}

		public UIViewFullscreen (IntPtr handle) : base (handle)
		{
			Initialize();
		}

		private void Initialize ()
		{
			var cBackground = new UIColor (0.0f, 0.0f, 0.0f, 0.6f);
			BackgroundColor = cBackground;

			sviMain = new UIScrollViewImage ();
			AddSubview (sviMain);
		}

		public void SetImage (UIImage image)
		{
			iImage = image;
		}

		public void Show()
		{
			var window = UIApplication.SharedApplication.Windows [0];
			Frame = window.Frame;
			sviMain.Frame = window.Frame;
			sviMain.SetImage (iImage);
			sviMain.OnSingleTap += () => {
				Hide();
			};

			window.AddSubview (this);

			Alpha = 0f;
			UIView.Animate (AnimationDuration, () => {
				Alpha = 1f;
			});
		}
		
		public void Hide()
		{
			if (Superview != null) {
				if (!UseAnimation) {
					RemoveFromSuperview ();
				} else {
					Alpha = 1f;
					UIView.Animate (AnimationDuration, () => {
						Alpha = 0f;
					}, () => {
						RemoveFromSuperview ();
					});
				}
			}
		}
	}
}

