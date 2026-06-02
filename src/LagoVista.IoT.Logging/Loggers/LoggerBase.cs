// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: cb04e21c82ab6ac134fcff5159e9328064445beb858c47cf5a17df6ffead5100
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.Core.Compare;
using LagoVista.Core.Interfaces;
using LagoVista.Core.PlatformSupport;
using LagoVista.IoT.Logging.Models;
using Newtonsoft.Json;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var logRecord = new LogRecord()
            {
                LogLevel = "Exception",
                Tag = tag,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };

            if(ex.InnerException != null)
            {
                logRecord.Message = $"{ex.Message}\r\nInner Exception: {ex.InnerException.Message}";
            }

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

        private static void ProbeStringProperties(object value, string path = "$", int depth = 0)
        {
            if (value == null || depth > 8)
                return;

            if (value is string text)
            {
                if (text.Contains("what modes are available"))
                {
                    Console.WriteLine($"[TESTMESSAGE] FOUND STRING at {path}");
                    Console.WriteLine(text);
                    Console.WriteLine(JsonConvert.SerializeObject(text, Formatting.None));
                }

                return;
            }

            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                var index = 0;

                foreach (var item in enumerable)
                {
                    ProbeStringProperties(item, $"{path}[{index++}]", depth + 1);
                }

                return;
            }

            var type = value.GetType();

            if (type.IsPrimitive || type.IsEnum || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Guid))
                return;

            foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (!property.CanRead)
                    continue;

                object propertyValue;

                try
                {
                    propertyValue = property.GetValue(value);
                }
                catch
                {
                    continue;
                }

                ProbeStringProperties(propertyValue, $"{path}.{property.Name}", depth + 1);
            }
        }

        private static string NormalizeTypographyInSerializedJsonForDiagnostics(string value)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            return value
                .Replace("\u201C", "\\\"")
                .Replace("\u201D", "\\\"")
                .Replace("\u2018", "'")
                .Replace("\u2019", "'")
                .Replace("\u2013", "-")
                .Replace("\u2014", "-");
        }

        public void WriteJson<T>(string name, T data)
        {
            var probeId = Guid.NewGuid().ToString("N").Substring(0, 8);;

            ProbeStringProperties(data, $"[TESTMESSAGE:{probeId}]");
            var settings = new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.Default
            };
            var json = JsonConvert.SerializeObject(data, Formatting.None, settings);

            // if anyone ever cracks this open again, somehwere in the console "smart-quotes" were getting converted to starndard quotes
            // and messing up the JSON strcuture. We found them by B64 encoding, hopefully no one will ever run into that again
            var bytes = Encoding.UTF8.GetBytes(json);
            var base64 = Convert.ToBase64String(bytes);
          
            json = NormalizeTypographyInSerializedJsonForDiagnostics(json);

            var message = $"[JSON.{name}]={json}";

            InsertEvent(new LogRecord()
            {
                EscapeCRLF = false,
                LogLevel = "JSON",
                Message = message,
            });
        }

        public void WriteJson(string name, string json)
        {
            try
            {
                var logRecord = new LogRecord()
                {
                    EscapeCRLF = false,
                    LogLevel = "JSONS",
                    Message = json,
                };


                InsertEvent(logRecord);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR IN TRACE: {ex.Message} - original {json}");
            }
        }
    }
}
