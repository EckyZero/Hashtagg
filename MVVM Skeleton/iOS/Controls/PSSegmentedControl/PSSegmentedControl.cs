using System;
using UIKit;
using Foundation;
using System.ComponentModel;
using ObjCRuntime;
using CoreGraphics;
using Shared.Common;
using System.Collections;
using System.Collections.Generic;

namespace iOS
{
	public partial class PSSegmentedControl : UIView
	{
		#region Private Variables

		private UIView _view;

		#endregion

		#region Member Properties

		public event EventHandler<int> ValueChanged;

		public nint NumberOfSegments 
		{
			get { return SegmentedControl.NumberOfSegments; }
		}
			
		public int SelectedSegment 
		{
			get { return (int)SegmentedControl.SelectedSegment; }
			set { SegmentedControl.SelectedSegment = value; }
		}

		public UIColor BorderColor
		{
			get { return UIColor.FromCGColor (_view.Layer.BorderColor); }
			set { _view.Layer.BorderColor = value.CGColor; }
		}

		public nfloat BorderWidth
		{
			get { return _view.Layer.BorderWidth; }
			set { _view.Layer.BorderWidth = value; }
		}

		public override UIColor TintColor
		{
			get { return SegmentedControl.TintColor; }
			set { SegmentedControl.TintColor = value; }
		}

		public nfloat CornerRadius
		{
			get { return _view.Layer.CornerRadius; }
			set { _view.Layer.CornerRadius = value; }
		}

		#endregion

		private PSSegmentedControl() {}

		public PSSegmentedControl (IntPtr handle) : base (handle)
		{
			var nib = NSBundle.MainBundle.LoadNib("PSSegmentedControl", this, null);

			_view = Runtime.GetNSObject(nib.ValueAt(0)) as UIView;
			_view.Frame = new CGRect(0, 0, Frame.Width, Frame.Height);
			_view.Layer.BorderColor = SharedColors.Gray3.ToUIColor ().CGColor;

			AddSubview (_view);

			var defaultImage = SharedColors.Gray3.ToUIColor ().ToImage (new CGRect(0,0, 1, SegmentedControl.Frame.Height));
			var selectImage = SharedColors.CompassBlue.ToUIColor ().ToImage (new CGRect(0,0, 1, SegmentedControl.Frame.Height));
			var selectedAttributes = new UITextAttributes () { Font = UIFont.FromName ("Futura-Book", 12) };
			var unselectedAttributes = new UITextAttributes () { Font = UIFont.FromName ("Futura-Book", 12) };

			SetNormalDividerImage (defaultImage);
			SetSelectedDividerImage (selectImage);

			SegmentedControl.SetTitleTextAttributes (selectedAttributes, UIControlState.Selected);
			SegmentedControl.SetTitleTextAttributes (unselectedAttributes, UIControlState.Normal);

			SegmentedControl.ValueChanged += OnValueChanged;
		}

		#region Methods

		public UIFont GetFont(UIControlState state)
		{
			var font = SegmentedControl.GetTitleTextAttributes (state).Font;

			return font;
		}

		public void InsertSegment(UIImage image, nint position, bool animated = false)
		{
			SegmentedControl.InsertSegment (image, position, animated);
		}

		public void InsertSegment(string title, nint position, bool animated = false)
		{
			SegmentedControl.InsertSegment (title, position, animated);
		}

		public void InsertSegments (IList<string> titles, bool animated = false)
		{
			for(int i = 0; i < titles.Count; i++)
			{
				var title = titles [i];

				SegmentedControl.InsertSegment (title, SegmentedControl.NumberOfSegments + i,animated);	
			}
		}

		public void RemoveSegmentAtIndex(nint segment, bool animated = false)
		{
			SegmentedControl.RemoveSegmentAtIndex (segment, animated);
		}

		public void RemoveAllSegments ()
		{
			SegmentedControl.RemoveAllSegments ();
		}

		public void ReplaceAllSegments (List<string> titles, bool animated = false)
		{
			SegmentedControl.RemoveAllSegments ();
			InsertSegments (titles, animated);
		}

		public void SetNormalDividerImage (UIImage image)
		{
			SegmentedControl.SetDividerImage (image, UIControlState.Normal, UIControlState.Normal, UIBarMetrics.Default);
		}

		public void SetSelectedDividerImage (UIImage image)
		{
			SegmentedControl.SetDividerImage (image, UIControlState.Normal, UIControlState.Selected, UIBarMetrics.Default);
			SegmentedControl.SetDividerImage (image, UIControlState.Selected, UIControlState.Normal, UIBarMetrics.Default);
		}

		public void SetFont(UIFont font, UIControlState state)
		{
			var attributes = new UITextAttributes () {
					Font = font
			};
			SegmentedControl.SetTitleTextAttributes (attributes, state);
		}

		public void SetImage(UIImage image, nint segment)
		{
			SegmentedControl.SetImage (image, segment);
		}

		public void SetTitle(string title, nint segment)
		{
			SegmentedControl.SetTitle (title, segment);
		}

		public void SetTitleTextAttributes (UITextAttributes attributes, UIControlState state)
		{
			SegmentedControl.SetTitleTextAttributes (attributes, state);
		}

		public void SetWidth (nfloat width, nint segment)
		{
			SegmentedControl.SetWidth (width, segment);
		}

		public void SetDividerImage (UIImage dividerImage, UIControlState leftSegmentState, UIControlState rightSegmentState, UIBarMetrics barMetrics)
		{
			SegmentedControl.SetDividerImage (dividerImage, leftSegmentState, rightSegmentState, barMetrics);
		}

		public void SetEnabled (bool enabled, nint segment)
		{
			SegmentedControl.SetEnabled (enabled, segment);
		}

		public void TitleAt(nint segment)
		{
			SegmentedControl.TitleAt (segment);
		}

		private void OnValueChanged (object sender, EventArgs e)
		{
			if(ValueChanged != null)
			{
				ValueChanged (this, (int)SegmentedControl.SelectedSegment);
			}
		}

		#endregion
	}
}

