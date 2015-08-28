using System;
using Xamarin;

namespace Shared.Common
{
    public class Tracker : ITracker
    {
        private readonly ITrackHandle _trackHandle;

        public Tracker(ITrackHandle trackHandle)
        {
            _trackHandle = trackHandle;
        }

        public void Start()
        {
            _trackHandle.Start();
        }

        public void Stop()
        {
            _trackHandle.Stop();
        }
    }
}

