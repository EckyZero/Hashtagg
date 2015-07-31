using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace Droid.Controls
{
    public class OverscrollListView : RelativeLayout
    {

        private OverscrollListeningListView _listView;
        private bool _overscrolling;
        private int _lastHistory;
        private View _header;
        private bool _init;
        private VelocityTracker vT = VelocityTracker.Obtain();
        private bool _firstOverscroll;
        private MotionEvent _historyEvent;

        public OverscrollListView(Context context)
            : base(context)
        {
            InitCustomOverscrollListView();
        }

        protected override void OnFinishInflate()
        {
            if (ChildCount != 0)
            {
                for (int i = 0; i < ChildCount; i++)
                {
                    var child = GetChildAt(i);
                    if (child.GetType() == typeof (OverscrollListeningListView))
                    {
                        if (_listView != null)
                            throw new Exception("Only one ListView can be a child!");
                        _listView = child as OverscrollListeningListView;
                        _listView.OnOverscrolled += ListViewOnOnOverscrolled;
                    }
                    else
                    {
                        _header = GetChildAt(i) as View;
                    }
                }
            } 
            base.OnFinishInflate();
        }


        private void ListViewOnOnOverscrolled(object sender, OverscrollListenerListViewEventArgs overscrollListenerListViewEventArgs)
        {

            if (overscrollListenerListViewEventArgs.DeltaY < 0)
            {
                _overscrolling = true;
                vT.ComputeCurrentVelocity(1);
                if (_header != null && (int)Math.Ceiling(_header.GetY()) != 0 &&_historyEvent.Action == MotionEventActions.Up || _historyEvent.Action == MotionEventActions.Cancel)
                {
                    long duration = (long)Math.Ceiling(Math.Abs(_header.Height) / vT.GetYVelocity(0));
                    _header.Animate().Y(0).SetDuration(duration).SetInterpolator(new DecelerateInterpolator(1.0f)).Start();
                    _listView.Animate().Y(_header.Height).SetDuration(duration).SetInterpolator(new DecelerateInterpolator(1.0f)).Start();
                }
            }
            else
            {
                _overscrolling = false;
            }
        }

        private void InitCustomOverscrollListView()
        {
            ViewTreeObserver.GlobalLayout += (sender, args) =>
            {
                if (_init)
                    return;
                _init = true;
                if (_listView != null)
                {
                    _header.LayoutParameters.Height = _header.Height;
                    _listView.LayoutParameters.Height = _listView.Height;
                    LayoutParameters.Height = Height;

                    RemoveView(_header);
                    (Parent as ViewGroup).AddView(_header);
                    _listView.SetY(_header.Height);
                }
            };
        }

        public OverscrollListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            InitCustomOverscrollListView();
        }

        public OverscrollListView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            InitCustomOverscrollListView();
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            //Log.Info("List", "List got event with action: {0}", ev.Action);
            return true;
            return base.OnInterceptTouchEvent(ev);
        }

        private void UpdateHeaderPosition(float deltaY)
        {
            _header.SetY(_header.GetY() + deltaY);
            //_header.LayoutParameters.Height += (int)Math.Ceiling(deltaY);
            //_header.RequestLayout();
        }

        public override bool OnTouchEvent(MotionEvent ev)
        {
            var hist = 0.0f;
            var deltaY = 0.0f;
            var listY = 0.0f;
            _historyEvent = ev;
            vT.AddMovement(ev);
            if (_overscrolling)
            {
                var firstOverscroll = _firstOverscroll;
                _firstOverscroll = false;
                    if (ev.Action == MotionEventActions.Move && ev.HistorySize >= 1)
                    {
                        hist = ev.GetHistoricalY(0, 0);
                        deltaY = ev.GetY() - hist;
                        listY = _listView.GetY();

                        if (_header != null)
                        {
                            var headerY = _header.GetY();
                            if (deltaY > 0)
                            {
                                
                                if ((int)Math.Ceiling(headerY) == (0))
                                {
                                    return _listView.DispatchTouchEvent(ev);
                                }

                                if (headerY + deltaY <= (0))
                                {
                                    UpdateHeaderPosition(deltaY);
                                    _listView.SetY(_listView.GetY() + deltaY);
                                    _listView.LayoutParameters.Height = (int)Math.Ceiling(_listView.LayoutParameters.Height + deltaY);
                                    _listView.Invalidate();
                                    return true;
                                }
                                if (headerY + deltaY > 0)
                                {
                                    deltaY = 0 - headerY;
                                    UpdateHeaderPosition(0 - headerY);
                                    _listView.SetY(_listView.GetY() + deltaY);
                                    _listView.LayoutParameters.Height = (int)Math.Ceiling(_listView.LayoutParameters.Height + deltaY);
                                    _listView.Invalidate();
                                    return true;
                                }
                            }
                            if (deltaY < 0)
                            {
                                _overscrolling = false;
                            }
                        }
                       

                        if (listY + deltaY <= 0)
                        {
                            deltaY = 0 - listY;
                            _overscrolling = false;
                        }
                        else if (deltaY > 0)
                        {
                            deltaY *= 0.4f;
                        }
                        Log.Info("OverScrollingIntercept", "{0}", deltaY);
                        _listView.SetY(_listView.GetY() + deltaY);
                        _lastHistory = ev.HistorySize;
                    }
                    else if (ev.Action == MotionEventActions.Up)
                    {
                        _overscrolling = false;
                        _lastHistory = 0;
                        _listView.Animate().Y(_header != null ? _header.Height : 0.0f).SetDuration(250).Start();
                    }
                

            Log.Info("Interceptor", "Overscrolling True");
                return true;
            }
            var hitRect = new Rect();
            _header.GetHitRect(hitRect);

            if (hitRect.Contains((int)Math.Ceiling(ev.GetX()), (int)Math.Ceiling(ev.GetY())))
            {
                return _header.DispatchTouchEvent(ev);
            }
            else
            {
                if (_header != null )
                {
                    var headerY = _header.GetY();
                    if (ev.Action == MotionEventActions.Move && ev.HistorySize >= 1)
                    {
                        hist = ev.GetHistoricalY(0, 0);
                        deltaY = ev.GetY() - hist;

                        if (deltaY < 0)
                        {
                            if ((int) Math.Ceiling(headerY) == (0 - _header.Height))
                            {
                                return _listView.DispatchTouchEvent(ev);
                            }

                            if (headerY + deltaY >= (0 - _header.Height))
                            {
                                UpdateHeaderPosition(deltaY);
                                _listView.SetY(_listView.GetY()+deltaY);
                                _listView.LayoutParameters.Height = (int)Math.Ceiling(_listView.LayoutParameters.Height - deltaY);
                                _listView.RequestLayout();
                                return true;

                            }
                            else if (headerY + deltaY < (0 - _header.Height))
                            {
                                deltaY = 0 - ( headerY + _header.Height);
                                UpdateHeaderPosition(deltaY);
                                _listView.SetY(_listView.GetY() + deltaY);
                                _listView.LayoutParameters.Height = (int)Math.Ceiling(_listView.LayoutParameters.Height - deltaY);
                                _listView.RequestLayout();
                                _listView.DispatchTouchEvent(ev);
                                return true;
                            }
                        }
                        _listView.DispatchTouchEvent(ev);
                        return true;
                    }
                    else if(ev.Action == MotionEventActions.Up || ev.Action == MotionEventActions.Cancel)
                    {
                        vT.ComputeCurrentVelocity(1);
                        long duration = Math.Abs((long)Math.Ceiling(Math.Abs(_header.Height) / vT.GetYVelocity(0)));

                        if (duration < 750)
                        {
                            _header.Animate()
                                .Y(0 - _header.Height)
                                .SetDuration(duration)
                                .SetInterpolator(new DecelerateInterpolator(1.0f))
                                .Start();
                            _listView.Animate()
                                .Y(0)
                                .SetDuration(duration)
                                .SetInterpolator(new DecelerateInterpolator(1.0f))
                                .Start();
                        }
                        return _listView.DispatchTouchEvent(ev);
                    }
                }
                return _listView.DispatchTouchEvent(ev);
            }
        }

        protected override void OnScrollChanged(int l, int t, int oldl, int oldt)
        {
            base.OnScrollChanged(l, t, oldl, oldt);
        }

        protected override void OnOverScrolled(int scrollX, int scrollY, bool clampedX, bool clampedY)
        {
            base.OnOverScrolled(scrollX, scrollY, clampedX, clampedY);
        }
    }

    public class OverscrollListeningListView : ListView
    {
        public event EventHandler<OverscrollListenerListViewEventArgs> OnOverscrolled;

        public OverscrollListeningListView(Context context) : base(context) { }

        public OverscrollListeningListView(Context context, IAttributeSet attrs) : base(context, attrs) { }

        public OverscrollListeningListView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { }

        protected override bool OverScrollBy(int deltaX, int deltaY, int scrollX, int scrollY, int scrollRangeX, int scrollRangeY, int maxOverScrollX, int maxOverScrollY, bool isTouchEvent)
        {
            base.OverScrollBy(deltaX, deltaY, scrollX, scrollY, scrollRangeX, scrollRangeY, maxOverScrollX, 0, isTouchEvent);

            if (OnOverscrolled != null)
            {
                OnOverscrolled(this, new OverscrollListenerListViewEventArgs(deltaY, 0));
            }

            return true;
        }

    }
    public class OverscrollListenerListViewEventArgs
    {
        public int DeltaY { get; set; }
        public int MaxOverScrollY { get; set; }

        public OverscrollListenerListViewEventArgs(int deltaY, int maxOverScrollY)
        {
            DeltaY = deltaY;
            MaxOverScrollY = maxOverScrollY;
        }
    }
}