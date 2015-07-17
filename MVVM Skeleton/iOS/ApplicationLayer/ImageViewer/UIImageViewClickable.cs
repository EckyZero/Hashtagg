using System;
using UIKit;

namespace iOS
{
	public partial class UIImageViewClickable: UIImageView
	{
		UITapGestureRecognizer grTap;

		event Action onCl;
		public event Action OnClick
		{
			add {
				onCl += value;
				UpdateUserInteractionFlag();
			}
			remove {
				onCl -= value;
				UpdateUserInteractionFlag();
			}
		}

		public UIImageViewClickable () : base ()
		{
		}

		public UIImageViewClickable (IntPtr handle) : base (handle)
		{
		}

		void UpdateUserInteractionFlag ()
		{
			UserInteractionEnabled = ((onCl != null) && (onCl.GetInvocationList().Length > 0));
			if (UserInteractionEnabled) {
				if (grTap == null) {
					grTap = new UITapGestureRecognizer(() => {
						if (onCl != null) {
							onCl();
						}
					});
					grTap.CancelsTouchesInView = true;
					AddGestureRecognizer(grTap);
				}
			} else {
				if (grTap != null) {
					RemoveGestureRecognizer(grTap);
					grTap = null;
				}
			}
		}
	}
}

