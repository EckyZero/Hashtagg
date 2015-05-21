using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Provider;
using Android.Text;
using Android.Text.Method;
using Android.Util;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Java.Lang;

namespace Droid.Controls
{
    public class PinCodeEventArgs : EventArgs
    {
        public string PIN { get; private set; }

        public PinCodeEventArgs(string pin)
        {
            PIN = pin;
        }
    }

    public delegate void PinCodeFinishedEventHandler(object sender, PinCodeEventArgs args);

    public class Pincode : FrameLayout
    {
        public event PinCodeFinishedEventHandler PinCodeComplete;

        public string PIN
        {
            get { return _editText.Text; }
            set
            {
                _editText.Text = value;
                _editText_TextChanged(this, null);
            }
        }

        private LinearLayout _pinLL;

        private EditText _editText;

        private ImageView _digit1_default;

        private ImageView _digit1_current;

        private ImageView _digit1_done;

        private ImageView _digit2_default;

        private ImageView _digit2_current;

        private ImageView _digit2_done;

        private ImageView _digit3_default;

        private ImageView _digit3_current;

        private ImageView _digit3_done;

        private ImageView _digit4_default;

        private ImageView _digit4_current;

        private ImageView _digit4_done;

        private Drawable _defaultIcon;

        private Drawable _currentIcon;

        private Drawable _doneIcon;

        private Drawable DefaultIcon
        {
            get
            {
                if (_defaultIcon == null)
                {
                    _defaultIcon = Resources.GetDrawable(Resource.Drawable.pin_default);
                    _defaultIcon.SetColorFilter(Color.Rgb(197, 197, 185), PorterDuff.Mode.Multiply);
                }
                return _defaultIcon;
            }
        }

        private Drawable CurrentIcon
        {
            get
            {
                if (_currentIcon == null)
                {
                    _currentIcon = Resources.GetDrawable(Resource.Drawable.pin_current);
                    _currentIcon.SetColorFilter(Color.Rgb(239, 75, 36), PorterDuff.Mode.Multiply);
                }
                return _currentIcon;
            }
        }

        private Drawable DoneIcon
        {
            get
            {
                if (_doneIcon == null)
                {
                    _doneIcon = Resources.GetDrawable(Resource.Drawable.pin_done);
                    _doneIcon.SetColorFilter(Color.Rgb(197, 197, 185), PorterDuff.Mode.Multiply);
                }
                return _doneIcon;
            }
        }

        private Context _context;
        private readonly object _lock = new object();

        public Pincode(Context context)
            : base(context)
        {
            _context = context;
            Init();
        }

        public Pincode(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            _context = context;
            SetAttributes(attrs);
            Init();
        }

        public Pincode(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            _context = context;
            SetAttributes(attrs);
            Init();
        }

        public void OnIncorrectPIN()
        {
            Shake();
            ResetPin();
        }

        public void ClearPIN()
        {
            ResetPin();
        }

        private void SetAttributes(IAttributeSet attrs)
        {
        }

        private void Init()
        {
            _pinLL = new LinearLayout(_context);

            var blank1 = new View(_context);

            _digit1_default = new ImageView(_context);
            _digit1_current = new ImageView(_context);
            _digit1_done = new ImageView(_context);
            _digit2_default = new ImageView(_context);
            _digit2_current = new ImageView(_context);
            _digit2_done = new ImageView(_context);
            _digit3_default = new ImageView(_context);
            _digit3_current = new ImageView(_context);
            _digit3_done = new ImageView(_context);
            _digit4_default = new ImageView(_context);
            _digit4_current = new ImageView(_context);
            _digit4_done = new ImageView(_context);

            _digit1_default.SetImageDrawable(DefaultIcon);
            _digit1_current.SetImageDrawable(CurrentIcon);
            _digit1_done.SetImageDrawable(DoneIcon);
            _digit2_default.SetImageDrawable(DefaultIcon);
            _digit2_current.SetImageDrawable(CurrentIcon);
            _digit2_done.SetImageDrawable(DoneIcon);
            _digit3_default.SetImageDrawable(DefaultIcon);
            _digit3_current.SetImageDrawable(CurrentIcon);
            _digit3_done.SetImageDrawable(DoneIcon);
            _digit4_default.SetImageDrawable(DefaultIcon);
            _digit4_current.SetImageDrawable(CurrentIcon);
            _digit4_done.SetImageDrawable(DoneIcon);

            FrameLayout.LayoutParams frameLP = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WrapContent, FrameLayout.LayoutParams.WrapContent);
            frameLP.Gravity = (GravityFlags.CenterHorizontal | GravityFlags.CenterVertical);

            FrameLayout digit1 = new FrameLayout(_context);
            FrameLayout digit2 = new FrameLayout(_context);
            FrameLayout digit3 = new FrameLayout(_context);
            FrameLayout digit4 = new FrameLayout(_context);

            digit1.AddView(_digit1_default, frameLP);
            digit1.AddView(_digit1_current, frameLP);
            digit1.AddView(_digit1_done, frameLP);

            digit2.AddView(_digit2_default, frameLP);
            digit2.AddView(_digit2_current, frameLP);
            digit2.AddView(_digit2_done, frameLP);

            digit3.AddView(_digit3_default, frameLP);
            digit3.AddView(_digit3_current, frameLP);
            digit3.AddView(_digit3_done, frameLP);

            digit4.AddView(_digit4_default, frameLP);
            digit4.AddView(_digit4_current, frameLP);
            digit4.AddView(_digit4_done, frameLP);

            var blank2 = new View(_context);

            LinearLayout.LayoutParams pinLP = new LinearLayout.LayoutParams(0, LayoutParams.WrapContent, 1f);
            pinLP.Gravity = (GravityFlags.CenterHorizontal | GravityFlags.CenterVertical);

            LinearLayout.LayoutParams blankLP = new LinearLayout.LayoutParams(0, CurrentIcon.IntrinsicHeight, 1f);

            LayoutParams lp = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);

            _editText = new EditText(_context);
            _editText.KeyPress += _editText_KeyPress;
            _editText.InputType = InputTypes.ClassNumber;
            _editText.SetCursorVisible(false);
            _editText.SetBackgroundColor(Color.Transparent);
            _editText.SetTextColor(Color.Transparent);

            _pinLL.AddView(blank1, 0, blankLP);
            _pinLL.AddView(digit1, 1, pinLP);
            _pinLL.AddView(digit2, 2, pinLP);
            _pinLL.AddView(digit3, 3, pinLP);
            _pinLL.AddView(digit4, 4, pinLP);
            _pinLL.AddView(blank2, 5, blankLP);
            AddView(_pinLL, 0, lp);
            AddView(_editText, 1, lp);

            _editText.TextChanged += _editText_TextChanged;
            _editText.Text = string.Empty;
            _editText.RequestFocus();
        }

        void _editText_KeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.KeyCode == Keycode.NumpadEnter || e.KeyCode == Keycode.Enter)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void _editText_TextChanged(object sender, TextChangedEventArgs e)
        {
            int len = _editText.Text.Length;
            lock (_lock)
            {
                if (len == 0)
                {
                    _digit1_default.Visibility = ViewStates.Gone;
                    _digit1_current.Visibility = ViewStates.Visible;
                    _digit1_done.Visibility = ViewStates.Gone;

                    _digit2_default.Visibility = ViewStates.Visible;
                    _digit2_current.Visibility = ViewStates.Gone;
                    _digit2_done.Visibility = ViewStates.Gone;

                    _digit3_default.Visibility = ViewStates.Visible;
                    _digit3_current.Visibility = ViewStates.Gone;
                    _digit3_done.Visibility = ViewStates.Gone;

                    _digit4_default.Visibility = ViewStates.Visible;
                    _digit4_current.Visibility = ViewStates.Gone;
                    _digit4_done.Visibility = ViewStates.Gone;
                }

                else if (len == 1)
                {
                    _digit1_default.Visibility = ViewStates.Gone;
                    _digit1_current.Visibility = ViewStates.Gone;
                    _digit1_done.Visibility = ViewStates.Visible;

                    _digit2_default.Visibility = ViewStates.Gone;
                    _digit2_current.Visibility = ViewStates.Visible;
                    _digit2_done.Visibility = ViewStates.Gone;

                    _digit3_default.Visibility = ViewStates.Visible;
                    _digit3_current.Visibility = ViewStates.Gone;
                    _digit3_done.Visibility = ViewStates.Gone;

                    _digit4_default.Visibility = ViewStates.Visible;
                    _digit4_current.Visibility = ViewStates.Gone;
                    _digit4_done.Visibility = ViewStates.Gone;
                }

                else if (len == 2)
                {
                    _digit1_default.Visibility = ViewStates.Gone;
                    _digit1_current.Visibility = ViewStates.Gone;
                    _digit1_done.Visibility = ViewStates.Visible;

                    _digit2_default.Visibility = ViewStates.Gone;
                    _digit2_current.Visibility = ViewStates.Gone;
                    _digit2_done.Visibility = ViewStates.Visible;

                    _digit3_default.Visibility = ViewStates.Gone;
                    _digit3_current.Visibility = ViewStates.Visible;
                    _digit3_done.Visibility = ViewStates.Gone;

                    _digit4_default.Visibility = ViewStates.Visible;
                    _digit4_current.Visibility = ViewStates.Gone;
                    _digit4_done.Visibility = ViewStates.Gone;
                }

                else if (len == 3)
                {
                    _digit1_default.Visibility = ViewStates.Gone;
                    _digit1_current.Visibility = ViewStates.Gone;
                    _digit1_done.Visibility = ViewStates.Visible;

                    _digit2_default.Visibility = ViewStates.Gone;
                    _digit2_current.Visibility = ViewStates.Gone;
                    _digit2_done.Visibility = ViewStates.Visible;

                    _digit3_default.Visibility = ViewStates.Gone;
                    _digit3_current.Visibility = ViewStates.Gone;
                    _digit3_done.Visibility = ViewStates.Visible;

                    _digit4_default.Visibility = ViewStates.Gone;
                    _digit4_current.Visibility = ViewStates.Visible;
                    _digit4_done.Visibility = ViewStates.Gone;
                }

                else if (len == 4)
                {
                    _digit1_default.Visibility = ViewStates.Gone;
                    _digit1_current.Visibility = ViewStates.Gone;
                    _digit1_done.Visibility = ViewStates.Visible;

                    _digit2_default.Visibility = ViewStates.Gone;
                    _digit2_current.Visibility = ViewStates.Gone;
                    _digit2_done.Visibility = ViewStates.Visible;

                    _digit3_default.Visibility = ViewStates.Gone;
                    _digit3_current.Visibility = ViewStates.Gone;
                    _digit3_done.Visibility = ViewStates.Visible;

                    _digit4_default.Visibility = ViewStates.Gone;
                    _digit4_current.Visibility = ViewStates.Gone;
                    _digit4_done.Visibility = ViewStates.Visible;

                    // there is a race condition when the debugger is attached that kills the app
                    // attempting to fix that by just waiting when in debug
                    #if DEBUG
                    Thread.Sleep(100);
                    #endif

                    if (PinCodeComplete != null)
                    {
                        PinCodeComplete(this, new PinCodeEventArgs(_editText.Text));
                    }
                }
            }
        }

        private void ResetPin()
        {
            _editText.Text = string.Empty;
        }

        private void Shake()
        {
            AnimatorSet animation = new AnimatorSet();
            animation.PlaySequentially(
                GenerateXAnimation(-20),
                GenerateXAnimation(40),
                GenerateXAnimation(-40),
                GenerateXAnimation(40),
                GenerateXAnimation(-30),
                GenerateXAnimation(20),
                GenerateXAnimation(-10),
                GenerateXAnimation(0)
                );
            animation.Start();
        }

        private ObjectAnimator GenerateXAnimation(int location)
        {
            ObjectAnimator trans = ObjectAnimator.OfFloat(_pinLL, "translationX", location);
            trans.SetDuration(65);
            return trans;
        }
    }
}