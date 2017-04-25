using LagoVista.Core.PlatformSupport;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging
{
    public class DefaultLogger : ILogger
    {
        public void Log(LogLevel level, string area, string message, params KeyValuePair<string, string>[] args)
        {

        }

        public void LogException(string area, Exception ex, params KeyValuePair<string, string>[] args)
        {

        }

        public void SetKeys(params string[] args)
        {

        }

        public void SetUserId(string userId)
        {

        }

        public void TrackEvent(string message, Dictionary<string, string> parameters)
        {

        }
    }
}