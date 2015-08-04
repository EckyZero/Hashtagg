using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Common
{
    public class Logger : ILogger
    {
        public void Log(Exception exception = null, LogType severity = LogType.WARNING)
        {
        }
    }
}
