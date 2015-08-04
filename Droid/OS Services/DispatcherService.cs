using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Common;

namespace Droid
{
    public class DispatcherService : BaseService, IDispatcherService
    {
        public void InvokeOnMainThread(Action action)
        {
            Application.SynchronizationContext.Post(state => action.Invoke(),Application.Context);
        }
    }
}