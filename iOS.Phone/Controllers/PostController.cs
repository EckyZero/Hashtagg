// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using UIKit;
using Shared.VM;

namespace iOS.Phone
{
	public partial class PostController : UIViewController
	{
		#region Properties

		public PostViewModel ViewModel { get; set; }

		#endregion

        #region Lifecycle

		public PostController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            InitUI();
            InitBindings();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            TextView.ResignFirstResponder();
        }

        #endregion

        #region Methods

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        private void InitUI()
        {
            TextView.BecomeFirstResponder();
        }

        private void InitBindings ()
        {
            
        }

        #endregion

        #region Actions

        partial void OnCancelButtonTapped(UIKit.UIBarButtonItem sender)
        {
            DismissViewController(true, null);
        }

        #endregion
	}
}
