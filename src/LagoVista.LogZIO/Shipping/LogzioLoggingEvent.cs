// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 5a6a09070467c5c0d2612b1adf857271ca5788354bb20b0f0cf77a7d432bc841
// IndexVersion: 2
// --- END CODE INDEX META ---
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