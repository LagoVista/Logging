// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: cb04e21c82ab6ac134fcff5159e9328064445beb858c47cf5a17df6ffead5100
// IndexVersion: 0
// --- END CODE INDEX META ---
using LagoVista.Core.Compare;
using LagoVista.Core.Interfaces;
using LagoVista.Core.PlatformSupport;
using LagoVista.IoT.Logging.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LagoVista.IoT.Logging.Loggers
{
    public abstract class LoggerBase : ILogger
    {
        protected readonly ILogWriter _writer;

        private bool _paused = false;

        IBackgroundServiceTaskQueue _taskQueue;

        public LoggerBase(ILogWriter writer, IBackgroundServiceTaskQueue backgroundTaskQueue = null)
        {
            _writer = writer;
            _taskQueue = backgroundTaskQueue;
        }

        protected async Task InsertEventAsync(Logging.Models.LogRecord log)
        {
            if (!_paused)
            {
                SetRecordIdentifiers(log);
                await _writer.WriteEvent(log);
            }
        }

        protected async Task InsertErrorAsync(Logging.Models.LogRecord log)
        {
            if (!_paused)
            {
                SetRecordIdentifiers(log);
                await _writer.WriteError(log);
            }
        }

        protected async void InsertEvent(Logging.Models.LogRecord log)
        {
            if (!_paused)
            {
                SetRecordIdentifiers(log);
                if (_taskQueue == null)
                {
                    await _writer.WriteEvent(log);

                }
                else
                {
                    if (!_taskQueue.TryQueueBackgroundWorkItem(async (cancelToken) =>
                    {
                        await _writer.WriteEvent(log);
                    }))
                    {
                        Console.WriteLine($"[LoggerBase__InsertError] - Could not queue work item, original message: [{log.Area}] {log.Message}");
                    }
                }
            }
        }

        protected async void InsertError(Logging.Models.LogRecord log)
        {
            if (!_paused)
            {
                SetRecordIdentifiers(log);
                if (_taskQueue == null)
                {
                    await _writer.WriteError(log);
                }
                else
                {
                    if (!_taskQueue.TryQueueBackgroundWorkItem(async (cancelToken) =>
                    {
                        await _writer.WriteError(log);
                    }))
                    {
                        Console.WriteLine($"[LoggerBase__InsertError] - Could not queue work item, original message: [{log.Area}] {log.Message}");
                    }
                }
            }
        }


        public void AddCustomEvent(LogLevel level, string tag, string customEvent, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = level.ToString(),
                Tag = tag,
                Message = customEvent,
            };

            logRecord.AddKVPs(args);
            if (level == LogLevel.Error || level == LogLevel.ConfigurationError)
            {
                InsertError(logRecord);
            }
            else
            {
                InsertEvent(logRecord);
            }
        }

        protected abstract void SetRecordIdentifiers(LogRecord log);

        public void AddException(string tag, Exception ex, params KeyValuePair<string, string>[] args)
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
            InsertError(logRecord);
        }

        public void AddKVPs(params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord();
            logRecord.AddKVPs(args);
            InsertEvent(logRecord);
        }

        public void EndTimedEvent(TimedEvent evt)
        {
            var duration = DateTime.Now - evt.StartTime;
            var logRecord = new LogRecord()
            {
                LogLevel = "TimedEvent",
                MS = duration.TotalMilliseconds,
                Area = evt.Area,
                Message = evt.Description
            };

            InsertEvent(logRecord);
        }

        public TimedEvent StartTimedEvent(string area, string description)
        {
            var evt = new TimedEvent(area, description);
            return evt;

        }

        public void Trace(string message, params KeyValuePair<string, string>[] args)
        {
            try
            {
                var logRecord = new LogRecord()
                {
                    LogLevel = "Trace",
                    Message = message,
                };

                logRecord.AddKVPs(args);

                InsertEvent(logRecord);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR IN TRACE: {ex.Message} - original {message}");
            }
        }


        public void TrackEvent(string message, Dictionary<string, string> parameters)
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

            InsertEvent(logRecord);
        }

        public void AddConfigurationError(string configurationSetting, string error, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord
            {
                LogLevel = "ConfigErr",
                Area = configurationSetting,
                Message = error
            };
            logRecord.AddKVPs(args);

            InsertError(logRecord);
        }

        public void AddError(string tag, string message, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "Error",
                Tag = tag,
                Message = message,
            };

            logRecord.AddKVPs(args);

            InsertError(logRecord);
        }

        public void AddMetric(string measure, double duration)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "DblMetric",
                Tag = measure,
                Measure = duration
            };

            InsertEvent(logRecord);
        }

        public void AddMetric(string measure, TimeSpan duration)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "TimeSpanMS",
                Tag = measure,
                MS = duration.TotalMilliseconds,
            };

            InsertEvent(logRecord);
        }

        public void AddMetric(string measure, int count = 1)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "IntMetric",
                Tag = measure,
                Measure = count,
            };

            InsertEvent(logRecord);
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

        public void TrackMetric(string kind, string name, MetricType metricType, double count, params KeyValuePair<string, string>[] args)
        {
        }

        public void TrackMetric(string kind, string name, MetricType metricType, int count, params KeyValuePair<string, string>[] args)
        {

        }
    }
}
