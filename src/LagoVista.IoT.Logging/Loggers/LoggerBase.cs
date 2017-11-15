using LagoVista.Core.PlatformSupport;
using LagoVista.IoT.Logging.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    public abstract class LoggerBase : ILogger
    {
        ILogWriter _writer;

        private bool _paused;

        public LoggerBase(ILogWriter writer)
        {
            _writer = writer;
        }

        protected async Task InsertEventAsync(Logging.Models.LogRecord log)
        {
            if (!_paused)
            {
                await _writer.WriteEvent(log);
            }
        }

        protected async Task InsertErrorAsync(Logging.Models.LogRecord log)
        {
            if (!_paused)
            {
                await _writer.WriteError(log);
            }
        }

        public async void AddCustomEvent(LogLevel level, string tag, string customEvent, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = level.ToString(),
                Tag = tag,
                Message = customEvent,
            };

            logRecord.AddKVPs(args);

            await InsertEventAsync(logRecord);
        }

        protected abstract void SetRecordIdentifiers(LogRecord log);

        public async void AddException(string tag, Exception ex, params KeyValuePair<string, string>[] args)
        {
            var msg = ex.Message;

            var logRecord = new LogRecord()
            {
                LogLevel = "Exception",
                Tag = tag,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };

            logRecord.AddKVPs(args);
            await InsertErrorAsync(logRecord);
        }

        public async void AddKVPs(params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord();
            logRecord.AddKVPs(args);
            await InsertEventAsync(logRecord);
        }

        public async void EndTimedEvent(TimedEvent evt)
        {
            var duration = DateTime.Now - evt.StartTime;
            var logRecord = new LogRecord()
            {
                LogLevel = "TimedEvent",
                MS = duration.TotalMilliseconds,
                Area = evt.Area,
                Message = evt.Description
            };

            await InsertEventAsync(logRecord);
        }

        public TimedEvent StartTimedEvent(string area, string description)
        {
            var evt = new TimedEvent(area, description);
            return evt;

        }

        public async void TrackEvent(string message, Dictionary<string, string> parameters)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "Event",
                Message = message,
            };

            if (parameters != null)
            {
                logRecord.Details = JsonConvert.SerializeObject(parameters);
            }

            await InsertEventAsync(logRecord);
        }

        public async void AddConfigurationError(string configurationSetting, string error, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord
            {
                LogLevel = "ConfigErr",
                Area = configurationSetting,
                Message = error
            };
            logRecord.AddKVPs(args);

            await InsertErrorAsync(logRecord);
        }

        public async void AddError(string tag, string message, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "Error",
                Tag = tag,
                Message = message,
            };

            logRecord.AddKVPs(args);

            await InsertErrorAsync(logRecord);
        }

        public async void AddMetric(string measure, double duration)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "DblMetric",
                Tag = measure,
                Measure = duration
            };

            await InsertEventAsync(logRecord);
        }

        public async void AddMetric(string measure, TimeSpan duration)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "TimeSpanMS",
                Tag = measure,
                MS = duration.TotalMilliseconds,
            };

            await InsertEventAsync(logRecord);
        }

        public async void AddMetric(string measure, int count = 1)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "IntMetric",
                Tag = measure,
                Measure = count,
            };

            await InsertEventAsync(logRecord);
        }

        public bool DebugMode { get; set; } = false;

        public void Start()
        {
            _paused = false;
        }

        public void Stop()
        {
            _paused = true;
        }
    }
}
