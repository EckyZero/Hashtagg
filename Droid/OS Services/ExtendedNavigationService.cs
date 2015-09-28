using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Droid;
using Shared.Common;
using GalaSoft.MvvmLight.Views;
using Android.App;

namespace Droid
{
    public class ExtendedNavigationService : BaseService, IExtendedNavigationService
    {
        public ExtendedNavigationService ():base()
        {
        }

        public void Present(string pageKey, object parameter = null)
        {
            NavigateTo(pageKey, parameter);
        }

        /// <summary>
        /// The key that is returned by the <see cref="CurrentPageKey"/> property
        /// when the current Activiy is the root activity.
        /// </summary>
        public const string RootPageKey = "-- ROOT --";

        private const string ParameterKeyName = "ParameterKey";

        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private readonly Dictionary<string, object> _parametersByKey = new Dictionary<string, object>();

        /// <summary>
        /// The key corresponding to the currently displayed page.
        /// </summary>
        public string CurrentPageKey
        {
            get
            {
                return Activity.ActivityKey ?? RootPageKey;
            }
        }

        /// <summary>
        /// If possible, discards the current page and displays the previous page
        /// on the navigation stack.
        /// </summary>
        public void GoBack()
        {
            Activity.GoBack();
            _activity.OverridePendingTransition(Resource.Animation.pull_in_left, Resource.Animation.push_out_right);
        }


        public void GoBack(AnimationFlag animationFlag)
        {
            Activity.GoBack();
            switch (animationFlag)
            {
                case AnimationFlag.Grow:
                case AnimationFlag.Up:
                case AnimationFlag.Forward:
                    _activity.OverridePendingTransition(Resource.Animation.pull_in_right, Resource.Animation.push_out_left);
                    break;
                case AnimationFlag.Down:
                case AnimationFlag.Back:
                    _activity.OverridePendingTransition(Resource.Animation.pull_in_left, Resource.Animation.push_out_right);
                    break;
            }
        }

        /// <summary>
        /// Displays a new page corresponding to the given key. 
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null, null, AnimationFlag.Forward);
        }

        public void NavigateTo(string pageKey, object parameter, List<NavigationFlag> navigationFlags, AnimationFlag animationFlag = AnimationFlag.Forward )
        {
            Intent intent = null;

            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        "pageKey");
                }

                intent = new Intent(_activity, _pagesByKey[pageKey]);
            }

            if (navigationFlags != null && navigationFlags.Any())
            {
                foreach (var flag in navigationFlags)
                {
                    switch (flag)
                    {
                        case NavigationFlag.ClearStack:
                            intent.AddFlags(ActivityFlags.ClearTop);
                            break;
                        case NavigationFlag.SingleInstance:
                            intent.AddFlags(ActivityFlags.SingleTop);
                            break;
                        case NavigationFlag.ClearTask:
                            intent.AddFlags(ActivityFlags.ClearTask);
                            break;
                        case NavigationFlag.NewTask:
                            intent.AddFlags(ActivityFlags.NewTask);
                            break;
                    }
                }
            }

            if (parameter != null)
            {
                lock (_parametersByKey)
                {
                    var guid = Guid.NewGuid().ToString();
                    _parametersByKey.Add(guid, parameter);
                    intent.PutExtra(ParameterKeyName, guid);
                }
            }

            StartIntentAndAnimate(intent, pageKey, animationFlag);
        }

        /// <summary>
        /// Displays a new page corresponding to the given key,
        /// and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateTo(string pageKey, object parameter)
        {
            NavigateTo(pageKey, parameter, null, AnimationFlag.Forward);
        }

        /// <summary>
        /// Displays a new page corresponding to the given key,
        /// and passes a parameter to the new page.
        /// Make sure to call the <see cref="Configure"/>
        /// method first.
        /// </summary>
        /// <param name="pageKey">The key corresponding to the page
        /// that should be displayed.</param>
        /// <param name="parameter">The parameter that should be passed
        /// to the new page.</param>
        /// <param name="flags">This parameter should be passed if you need intent flags</param>
        /// <exception cref="ArgumentException">When this method is called for 
        /// a key that has not been configured earlier.</exception>
        public void NavigateTo(string pageKey, object parameter, ActivityFlags[] flags)
        {
            if (_activity == null)
            {
                throw new InvalidOperationException("No CurrentActivity found");
            }

            lock (_pagesByKey)
            {
                if (!_pagesByKey.ContainsKey(pageKey))
                {
                    throw new ArgumentException(
                        string.Format(
                            "No such page: {0}. Did you forget to call NavigationService.Configure?",
                            pageKey),
                        "pageKey");
                }

                var intent = new Intent(_activity, _pagesByKey[pageKey]);
                if (flags != null)
                {
                    foreach (var flag in flags)
                    {
                        intent.AddFlags(flag);
                    }
                }
                if (parameter != null)
                {
                    lock (_parametersByKey)
                    {
                        var guid = Guid.NewGuid().ToString();
                        _parametersByKey.Add(guid, parameter);
                        intent.PutExtra(ParameterKeyName, guid);
                    }
                }
                StartIntentAndAnimate(intent, pageKey);
            }
        }

        private void StartIntentAndAnimate(Intent intent, string pageKey = "", AnimationFlag animationFlag = AnimationFlag.Forward)
        {
            Activity.NextPageKey = pageKey;

            _activity.StartActivity(intent);

            switch (animationFlag)
            {
                case AnimationFlag.Grow:
                case AnimationFlag.Up:
                case AnimationFlag.Forward:
                    _activity.OverridePendingTransition(Resource.Animation.pull_in_right, Resource.Animation.push_out_left);
                    break;
                case AnimationFlag.Down:
                case AnimationFlag.Back:
                    _activity.OverridePendingTransition(Resource.Animation.pull_in_left, Resource.Animation.push_out_right);
                    break;
            }
        }

        /// <summary>
        /// Adds a key/page pair to the navigation service.
        /// </summary>
        /// <remarks>For this navigation service to work properly, your Activities
        /// should derive from the <see cref="ActivityBase"/> class.</remarks>
        /// <param name="key">The key that will be used later
        /// in the <see cref="NavigateTo(string)"/> or <see cref="NavigateTo(string, object)"/> methods.</param>
        /// <param name="activityType">The type of the activity (page) corresponding to the key.</param>
        public void Configure(string key, Type activityType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = activityType;
                }
                else
                {
                    _pagesByKey.Add(key, activityType);
                }
            }
        }

        /// <summary>
        /// Allows a caller to get the navigation parameter corresponding 
        /// to the Intent parameter.
        /// </summary>
        /// <param name="intent">The <see cref="Android.App.Activity.Intent"/> 
        /// of the navigated page.</param>
        /// <returns>The navigation parameter. If no parameter is found,
        /// returns null.</returns>
        public object GetAndRemoveParameter(Intent intent)
        {
            if (intent == null)
            {
                throw new ArgumentNullException("intent", "This method must be called with a valid Activity intent");
            }

            var key = intent.GetStringExtra(ParameterKeyName);

            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            lock (_parametersByKey)
            {
                if (_parametersByKey.ContainsKey(key))
                {
                    return _parametersByKey[key];
                }

                return null;
            }
        }

        /// <summary>
        /// Allows a caller to get the navigation parameter corresponding 
        /// to the Intent parameter.
        /// </summary>
        /// <typeparam name="T">The type of the retrieved parameter.</typeparam>
        /// <param name="intent">The <see cref="Android.App.Activity.Intent"/> 
        /// of the navigated page.</param>
        /// <returns>The navigation parameter casted to the proper type.
        /// If no parameter is found, returns default(T).</returns>
        public T GetAndRemoveParameter<T>(Intent intent)
        {
            return GetAndRemoveParameter(intent) is T ? (T) GetAndRemoveParameter(intent) : default(T);
        }
    }
}

