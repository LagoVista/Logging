using System.Collections.Generic;

namespace Logzio.DotNet.Core.Shipping
{
    public class LogzioLoggingEvent
    {
        public Dictionary<string, object> LogData { get; set; }

        public LogzioLoggingEvent(Dictionary<string, object> logData)
        {
            LogData = logData;
        }
    }
}