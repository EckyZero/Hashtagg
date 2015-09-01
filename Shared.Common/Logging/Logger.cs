using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin;
using System.Collections;


namespace Shared.Common
{
    public class Logger : ILogger
    {
        #region Variables

        private static Dictionary<string, ITracker> _timedEvents = new Dictionary<string, ITracker>();

        #endregion

        #region Methods

        public void Log(Exception exception = null, LogType severity = LogType.WARNING, IDictionary extraData = null)
        {
            if(Insights.IsInitialized)
            {
                Insights.Report(exception, extraData, LogTypeToInsight(severity));   
            }
        }

        public void Track(string identifier, IDictionary<string,string> extraData = null)
        {
            if(Insights.IsInitialized)
            {
                Insights.Track(identifier, extraData);   
            }
        }

        public void StartTrackTime(string eventName, IDictionary<string,string> extraData = null)
        {
            StopTrackTime(eventName);

            var tracker = TrackTime(eventName, extraData);
            tracker.Start();

            _timedEvents.Add(eventName, tracker);
        }

        public void StopTrackTime(string eventName)
        {
            if (_timedEvents.ContainsKey(eventName))
            {
                _timedEvents[eventName].Stop();
                _timedEvents.Remove(eventName);
            }
        }

        private ITracker TrackTime(string identifier, IDictionary<string,string> extraData)
        {
            return new Tracker(Insights.TrackTime(identifier, extraData));
        }

        private Insights.Severity LogTypeToInsight(LogType severity)
        {
            var insightsSeverity = Insights.Severity.Warning;

            if (severity == LogType.ERROR)
            {
                insightsSeverity = Insights.Severity.Error;
            }
            else if (severity == LogType.CRITICAL)
            {
                insightsSeverity = Insights.Severity.Critical;
            }

            return insightsSeverity;
        }

        #endregion
    }
}
