using LagoVista.Core.PlatformSupport;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LagoVista.IoT.Logging
{
    public class DebugLogger : ILogger
    {
        public bool DebugMode { get; set; } = false;

        public TimedEvent StartTimedEvent(string area, string description)
        {
            return new TimedEvent(area, description);
        }

        public void EndTimedEvent(TimedEvent evt)
        {
            var duration = DateTime.Now - evt.StartTime;
            AddCustomEvent(LogLevel.Message, evt.Area, evt.Description, new KeyValuePair<string, string>("Duration", Math.Round(duration.TotalSeconds,4).ToString()));
        }


        /* Want to capture the concept of subscribing to messages from specific devices */

        public void AddCustomEvent(LogLevel level, string area, string message, params KeyValuePair<string, string>[] args)
        {
            Debug.WriteLine("==========================================================");
            Debug.WriteLine("Level               :" + level);
            Debug.WriteLine("Area                :" + area);
            Debug.WriteLine("Message             :" + message);
            foreach (var arg in args)
            {
                Debug.WriteLine($"{arg.Key}\t\t\t:" + arg.Value);
            }
            Debug.WriteLine("==========================================================");
            Debug.WriteLine("");
        }

        public void AddException(string area, Exception ex, params KeyValuePair<string, string>[] args)
        {
            Debug.WriteLine("==========================================================");
            Debug.WriteLine("Area                :" + area);
            Debug.WriteLine("Message             :" + ex.Message);
            Debug.WriteLine("----------------------------------------------------------");
            Debug.WriteLine(ex.StackTrace);
            Debug.WriteLine("");
            foreach (var arg in args)
            {
                Debug.WriteLine($"{arg.Key}\t\t\t:" + arg.Value);
            }
            Debug.WriteLine("==========================================================");
            Debug.WriteLine("");
        }

        public void AddKVPs(params KeyValuePair<string, string>[] args)
        {

        }

        public void SetUserId(string userId)
        {

        }

        public void TrackEvent(string message, Dictionary<string, string> args)
        {
            Debug.WriteLine("==========================================================");
            Debug.WriteLine(message);
            foreach (var arg in args)
            {
                Debug.WriteLine($"{arg.Key}\t\t\t:" + arg.Value);
            }
            Debug.WriteLine("==========================================================");
            Debug.WriteLine("");
        }

        public void TrackMetric(string kind, string name, MetricType metricType, double count, params KeyValuePair<string, string>[] args)
        {
    
        }

        public void TrackMetric(string kind, string name, MetricType metricType, int count, params KeyValuePair<string, string>[] args)
        {
      
        }

        public void Trace(string message)
        {
            Debug.WriteLine($"[TRACE] - {message}");
        }
    }
}