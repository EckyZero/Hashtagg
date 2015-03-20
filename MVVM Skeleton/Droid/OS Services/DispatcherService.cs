using System;
using Android.App;
using Shared.Common;

namespace Droid
{
    public class DispatcherService : IDispatcherService
    {
        public void InvokeOnMainThread(Action action)
        {
            Application.SynchronizationContext.Post(state => action.Invoke(),Application.Context);
        }
    }
}