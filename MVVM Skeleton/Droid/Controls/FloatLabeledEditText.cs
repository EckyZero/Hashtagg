//  The MIT License (MIT)
//
//  Copyright (c) 2013 Greg Shackles
//  Original implementation by Jared Verdi
//	https://github.com/jverdi/JVFloatLabeledTextField
//  Original Concept by Matt D. Smith
//  http://dribbble.com/shots/1254439--GIF-Mobile-Form-Interaction?list=users
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to
//  use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
//  the Software, and to permit persons to whom the Software is furnished to do so,
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
//  FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
//  COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//  IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//  CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Android.Animation;
using Android.Content;
using Android.Content.Res;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Locations;
using Android.Support.V4.Widget;
using Android.Support.V7.Internal.Widget;
using Android.Text.Method;
using Shared.Common;
using Java.Nio.Channels;

namespace Droid.Controls
{
    public delegate void FocusEventHandler(object sender, View.FocusChangeEventArgs e);

    public class FloatLabeledEditText : FrameLayout
    {
        public event EventHandler Touched;

        public event EventHandler ToolTipInvoked;

        public event EventHandler TextChanged;

        public event FocusEventHandler TextFocusedChanged;

        private List<object> _spinnerValues = new List<object>{};

        private ImageView _imageView;

        private Drawable _toolTipIcon;

        private Drawable _clearIcon;

        private Drawable _showIcon;

        private Drawable _hideIcon;

        public List<object> SpinnerValues
        {
            get { return _spinnerValues; }
        }

        private Drawable ToolTipIcon
        {
            get
            {
                _toolTipIcon = _toolTipIcon ?? Resources.GetDrawable(Resource.Drawable.ic_tooltip);

                return _toolTipIcon;
            }
        }

        private Drawable ClearIcon
        {
            get
            {
                _clearIcon = _clearIcon ?? Resources.GetDrawable(Resource.Drawable.ic_clear);

                return _clearIcon;
            }
        }

        private Drawable ShowIcon
        {
            get
            {
                _showIcon = _showIcon ?? Resources.GetDrawable(Resource.Drawable.show);

                return _showIcon;
            }
        }

        private Drawable HideIcon
        {
            get
            {
                _hideIcon = _hideIcon ?? Resources.GetDrawable(Resource.Drawable.hide);


                return _hideIcon;
            }
        }

        private static float DEFAULT_PADDING_LEFT = 2;

        private bool _displayToolTip = false;

        private bool _datePicker = false;

        private bool _customPicker = false;

        private bool _passwordField = false;

        private bool _pickShowing = false;

        private bool _calShowing = false;

        private TextView mHintTextView;
        
        private EditText mEditText;

        private Context mContext;

        private bool ShowHint { get; set; }

        public DateTime InitDate { get; set; }

        public DateTime? MaxDate { get; set; }

        public DateTime? MinDate { get; set; }

        public string Text
        {
            get { return mEditText.Text; }
            set
            {
                mEditText.Text = value;
            }
        }

        public FloatLabeledEditText(Context context)
            : base(context)
        {
            mContext = context;
        }

        public FloatLabeledEditText(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            mContext = context;
            SetAttributes(attrs);
        }

        public FloatLabeledEditText(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            mContext = context;
            SetAttributes(attrs);
        }

        private void SetAttributes(IAttributeSet attrs)
        {
            mHintTextView = new TextView(mContext);

            var typedArray = mContext.ObtainStyledAttributes(attrs, Resource.Styleable.FloatLabeledEditText);

            int padding = typedArray.GetDimensionPixelSize(Resource.Styleable.FloatLabeledEditText_fletPadding, 0);
            int defaultPadding = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, DEFAULT_PADDING_LEFT, Resources.DisplayMetrics);
            int paddingLeft = typedArray.GetDimensionPixelSize(Resource.Styleable.FloatLabeledEditText_fletPaddingLeft, defaultPadding);
            int paddingTop = typedArray.GetDimensionPixelSize(Resource.Styleable.FloatLabeledEditText_fletPaddingTop, 0);
            int paddingRight = typedArray.GetDimensionPixelSize(Resource.Styleable.FloatLabeledEditText_fletPaddingRight, 0);
            int paddingBottom = typedArray.GetDimensionPixelSize(Resource.Styleable.FloatLabeledEditText_fletPaddingBottom, 0);
            _displayToolTip = typedArray.GetBoolean(Resource.Styleable.FloatLabeledEditText_fletDisplayToolTip,false);
            _datePicker = typedArray.GetBoolean(Resource.Styleable.FloatLabeledEditText_fletDatePicker, false);
            _customPicker = typedArray.GetBoolean(Resource.Styleable.FloatLabeledEditText_fletCustomPicker, false);

            if (padding != 0)
            {
                mHintTextView.SetPadding(padding, padding, padding, padding);
            }
            else
            {
                mHintTextView.SetPadding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }

            mHintTextView.SetTextAppearance(mContext, typedArray.GetResourceId(Resource.Styleable.FloatLabeledEditText_fletTextAppearance, global::Android.Resource.Style.TextAppearanceSmall));

            SetHintFont();

            //Start hidden
            mHintTextView.Visibility = Android.Views.ViewStates.Invisible;

            AddView(mHintTextView, LayoutParams.WrapContent, LayoutParams.WrapContent);

            typedArray.Recycle();
        }

        public override void AddView(View child, int index, ViewGroup.LayoutParams layoutParams)
        {
            if (child is EditText)
            {
                if (mEditText != null)
                {
                    throw new IllegalArgumentException("Can only have one Edittext subview");
                }

                LayoutParams lp = new LayoutParams(layoutParams);
                lp.Gravity = GravityFlags.Center;
                int margin = (int)(mHintTextView.TextSize + mHintTextView.PaddingBottom + mHintTextView.PaddingTop);
                int rightPadding = Resources.GetDrawable(Resource.Drawable.show).IntrinsicWidth;
                lp.TopMargin = margin/2+1;
                lp.BottomMargin = margin/2+1;
                child.SetPadding(mHintTextView.PaddingLeft, 0, (int)(rightPadding*1.3), 0);
                layoutParams = lp;

                SetEditText((EditText)child);
            }
            else if (child is ImageView)
            {
                if (_imageView != null)
                {
                    throw new IllegalArgumentException("Can only have one ImageView subview");
                }

                SetImageView((ImageView)child);
            }

            base.AddView(child, index, layoutParams);
        }

        private void SetEditText(EditText editText)
        {
            mEditText = editText;
            if (_datePicker)
            {
                mEditText.Touch += HandleDatePickerTouched;
            }
            else if (_customPicker)
            {
                mEditText.Touch += HandleCustomPickerTouched;
            }
            else
            {
                mEditText.Touch += HandleEditTextTouched;
            }
            mEditText.AfterTextChanged += mEditTextAfterTextChanged;
            mEditText.BeforeTextChanged += mEditTextBeforeTextChanged;
            mEditText.TextChanged += mEditTextTextChanged;
            mEditText.FocusChange += mEditText_FocusChange;
            mEditText.Visibility = ViewStates.Visible;

            if (mEditText.InputType == (InputTypes.ClassText | InputTypes.TextVariationPassword) || mEditText.InputType == (InputTypes.ClassNumber | InputTypes.NumberVariationPassword))
            {
                _passwordField = true;
            }

            mHintTextView.Text = mEditText.Hint;
            
            if (!TextUtils.IsEmpty(mEditText.Text))
            {
                mHintTextView.Visibility = ViewStates.Visible;
            }

            SetEditFont();
        }
 
        private void HandleEditTextTouched(object sender, View.TouchEventArgs args)
        {
            args.Handled = false;
            if (args.Event.Action == MotionEventActions.Up)
            {
                if (Touched != null)
                {
                    args.Handled = true;
                    Touched(this, new EventArgs());
                }
            }
        }
 
        private void HandleCustomPickerTouched(object sender, View.TouchEventArgs args)
        {
            if (args.Event.Action == MotionEventActions.Up)
            {
                ShowPicker();
            }
        }
 
        private void HandleDatePickerTouched(object sender, View.TouchEventArgs args)
        {
            if (args.Event.Action == MotionEventActions.Up)
            {
                ShowCalendar();
            }
        }

        private void ShowCalendar()
        {
            if (!_calShowing)
            {
                _calShowing = true;
				var dp = new DatePickerDialog(mContext, SetDate, InitDate.Year, System.Math.Max(0,InitDate.Month-1), InitDate.Day);
                if (MaxDate != null)
                {
                    dp.DatePicker.MaxDate = MaxDate.Value.ToEPOCH();
                }
                if (MinDate != null)
                {
                    dp.DatePicker.MinDate = MinDate.Value.ToEPOCH();
                }
                dp.CancelEvent += CancelDatePicker;
                dp.DismissEvent += DismissDatePicker;
                dp.Show();
            }
        }
 
        private void DismissDatePicker(object sender, EventArgs args)
        {
            _calShowing = false;
        }
 
        private void CancelDatePicker(object sender, EventArgs args)
        {
            _calShowing = false;
        }

        private void SetDate(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            mEditText.Text = e.Date.ToShortDateString();
            _calShowing = false;
        }

        private void ShowPicker()
        {
            if (!_pickShowing)
            {
                _pickShowing = true;
                var spin = new PopupSpinner(mContext, SpinnerValues.ToArray());
                spin.OnItemSelected += SetPicked;
                spin.Cancelled += SpinnerCancelled;
                spin.Show();
            }
        }
 
        private void SpinnerCancelled(object sender, EventArgs args)
        {
            _pickShowing = false;
        }

        public event Action<PopupSpinnerEventArgs> ItemSelected;

        void SetPicked(object sender, PopupSpinnerEventArgs args)
        {
            mEditText.Text = args.ItemSelected.ToString();
            if (ItemSelected != null)
            {
                ItemSelected(args);
            }
            _pickShowing = false;

        }



        private void SetImageView(ImageView view)
        {
            _imageView = view;
            _imageView.Click += ImageViewOnClick;

            if (_displayToolTip)
            {
                SetIcon(ToolTipIcon);
            }
        }

        private void ImageViewOnClick(object sender, EventArgs eventArgs)
        {
            if (_imageView.Drawable.Equals(ToolTipIcon))
            {
                if (ToolTipInvoked != null)
                {
                    ToolTipInvoked(this, new EventArgs());
                }
            }
            else if (_imageView.Drawable.Equals(ClearIcon))
            {
                mEditText.Text = string.Empty;
            }
            else if (_imageView.Drawable.Equals(ShowIcon))
            {
                mEditText.TransformationMethod = null;
                mEditText.SetSelection((mEditText.Length()));
                SetIcon(HideIcon);
            }
            else if (_imageView.Drawable.Equals(HideIcon))
            {
                mEditText.TransformationMethod = new PasswordTransformationMethod();
                mEditText.SetSelection((mEditText.Length()));
                SetIcon(ShowIcon);
            }
        }

        private void SetIcon(Drawable icon)
        {
            _imageView.SetImageDrawable(icon);
        }


        private void mEditText_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (e.HasFocus && !string.IsNullOrWhiteSpace(mEditText.Text))
            {
                if (_passwordField)
                {
                    SetIcon(ShowIcon);
                }
                else
                {
                    SetIcon(ClearIcon);
                }
            }
            else
            {
                if (_passwordField)
                {
                    mEditText.TransformationMethod = new PasswordTransformationMethod();
                }

                if (_displayToolTip)
                {
                    SetIcon(ToolTipIcon);
                }
                else
                {
                    SetIcon(null);
                }
            }
            OnFocusChanged(e.HasFocus);

            if (TextFocusedChanged != null)
            {
                TextFocusedChanged(this,e);
            }
        }

        private void mEditTextTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void mEditTextBeforeTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void mEditTextAfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(mEditText.Text))
            {
                if (_passwordField)
                {
                    SetIcon(ShowIcon);
                }
                else
                {
                    SetIcon(ClearIcon);
                }
            }
            else
            {
                if (_displayToolTip)
                {
                    SetIcon(ToolTipIcon);
                }
                else
                {
                    SetIcon(null);
                }
            }
            ShowHint = !TextUtils.IsEmpty(e.Editable);
            SetShowHint();

            // rethrow
            if (TextChanged != null)
            {
                TextChanged(this, new EventArgs());
            }
        }

        private void OnFocusChanged(bool gotFocus)
        {
            if (gotFocus && mHintTextView.Visibility == ViewStates.Visible)
            {
                ObjectAnimator.OfFloat(mHintTextView, "alpha", 0.33f, 1f).Start();
            }
            else if (mHintTextView.Visibility == ViewStates.Visible)
            {
                ObjectAnimator.OfFloat(mHintTextView, "alpha", 1f, 0.33f).Start();
            }
        }

        private void SetShowHint()
        {
            AnimatorSet animation = null;
            if ((mHintTextView.Visibility == ViewStates.Visible) && !ShowHint)
            {
                animation = new AnimatorSet();
                ObjectAnimator moveHint = ObjectAnimator.OfFloat(mHintTextView, "translationY", 0, mHintTextView.Height / 8);
                ObjectAnimator moveText = ObjectAnimator.OfFloat(mEditText, "translationY", mHintTextView.Height / 2, 0);
                ObjectAnimator fade = ObjectAnimator.OfFloat(mHintTextView, "alpha", 1, 0);
                animation.PlayTogether(moveHint,moveText,fade);
            }
            else if (((mHintTextView.Visibility != ViewStates.Visible) && ShowHint))
            {
                animation = new AnimatorSet();
                ObjectAnimator moveHint = ObjectAnimator.OfFloat(mHintTextView, "translationY", mHintTextView.Height / 8, 0);
                ObjectAnimator moveText = ObjectAnimator.OfFloat(mEditText, "translationY", 0, mHintTextView.Height / 2);
                ObjectAnimator fade;
                if (mEditText.IsFocused)
                {
                    fade = ObjectAnimator.OfFloat(mHintTextView, "alpha", 0, 1);
                }
                else
                {
                    fade = ObjectAnimator.OfFloat(mHintTextView, "alpha", 0, 0.33f);
                }
                animation.PlayTogether(moveHint,moveText,fade);
            }

            if (animation != null)
            {
                animation.AnimationStart += AnimationAnimationStart;
                animation.AnimationEnd += AnimationAnimationEnd;
                animation.Start();
            }
        }

        private void AnimationAnimationStart(object sender, EventArgs e)
        {
            base.OnAnimationStart();
            mHintTextView.Visibility = ViewStates.Visible;
        }

        private void AnimationAnimationEnd(object sender, EventArgs e)
        {
            base.OnAnimationEnd();

            mHintTextView.Visibility = ShowHint ? ViewStates.Visible : ViewStates.Invisible;
        }

        public EditText GetEditText()
        {
            return mEditText;
        }

        public void SetHint(string hint)
        {
            mEditText.Hint = hint;
            mHintTextView.Text = hint;
        }

        public string GetHint()
        {
            return mHintTextView.Hint;
        }

        private void SetHintFont()
        {
            try
            {

                // font
                Typeface tf = Typeface.CreateFromAsset(Context.Assets, "Fonts/CenturyGothicBold.ttf");
                var tfStyle = TypefaceStyle.Normal;
                if (null != mHintTextView.Typeface)
                    tfStyle = mHintTextView.Typeface.Style;
                mHintTextView.SetTypeface(tf, tfStyle);
            }
            catch
            {
            }
        }

        private void SetEditFont()
        {
            try
            {
                // font
                Typeface tf = Typeface.CreateFromAsset(Context.Assets, "Fonts/CenturyGothic.ttf");
                var tfStyle = TypefaceStyle.Normal;
                if (null != mEditText.Typeface)
                    tfStyle = mEditText.Typeface.Style;
                mEditText.SetTypeface(tf, tfStyle);
            }
            catch
            {
            }
        }
    }
}
