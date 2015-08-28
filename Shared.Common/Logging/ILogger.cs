using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Shared.Common
{
    public enum LogType
    {
        WARNING,
        ERROR,
        CRITICAL,
        INFO,
        SUCCESS
    }

    public interface ITracker
    {
        void Start();
        void Stop();
    }

    public interface ILogger
    {
        void Log(Exception exception = null, LogType severity = LogType.WARNING, IDictionary extraData = null);
        void Track(string identifier, IDictionary<string,string> extraData = null);
        void StartTrackTime(string eventName, IDictionary<string,string> extraData = null);
        void StopTrackTime(string eventName);
    }

}
