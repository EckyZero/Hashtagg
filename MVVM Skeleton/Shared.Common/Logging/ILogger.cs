using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Common.Logging
{
    public enum LogType
    {
        WARNING,
        ERROR,
        INFO,
        SUCCESS
    }

    public interface ILogger
    {
        void Log(Exception exception = null, LogType severity = LogType.WARNING);
    }
}
