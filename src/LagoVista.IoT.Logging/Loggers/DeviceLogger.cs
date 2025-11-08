// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: f083470991f86da98493a006956405d9071fb3504fbe75b98c16175dd2eb6373
// IndexVersion: 2
// --- END CODE INDEX META ---
using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class DeviceLogger : LoggerBase, IDeviceLogger
    {
        public DeviceLogger(ILogWriter writer, string hostId, string version, string instanceId) : base(writer)
        {
            HostId = hostId;
            InstanceId = instanceId;
            Version = version;
        }

        public string HostId { get; private set; }

        public string InstanceId { get; private set; }
        public string Version { get; private set; }
    
        
        public async void AddConfigurationError(string deviceTypeId, string deviceId, string configurationSetting, string error, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "ConfigErr",
                DeviceTypeId = deviceTypeId,
                DeviceId = deviceId,
                Message = configurationSetting,
            };


            logRecord.AddKVPs(args);

            await InsertErrorAsync(logRecord);
        }

        public async void AddError(string deviceTypeId, string deviceId, string tag, string message, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "Error",
                DeviceTypeId = deviceTypeId,
                DeviceId = deviceId,
                Tag = tag,
                Message = message
            };

            logRecord.AddKVPs(args);

            await InsertErrorAsync(logRecord);
        }

        public async void AddError(string deviceTypeId, string deviceId, ErrorCode errorCode, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "Error",
                DeviceTypeId = deviceTypeId,
                DeviceId = deviceId,
                ErrorCode = errorCode.Code,
                Message = errorCode.Message,
            };

            logRecord.AddKVPs(args);

            await InsertErrorAsync(logRecord);
        }

        public async void AddMetric(string deviceTypeId, string deviceId, string measure, double duration)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "TimeSpanMS",
                DeviceTypeId = deviceTypeId,
                DeviceId = deviceId,
                Tag = measure,
                MS = duration
            };

            await InsertEventAsync(logRecord);
        }

        public async void AddMetric(string deviceTypeId, string deviceId, string measure, TimeSpan duration)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "TimeSpanMS",
                DeviceTypeId = deviceTypeId,
                DeviceId = deviceId,
                Tag = measure,
                MS = duration.TotalMilliseconds
            };

            await InsertEventAsync(logRecord);
        }

        public async void AddMetric(string deviceTypeId, string deviceId, string measure, int count = 1)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "IntMetric",
                DeviceTypeId = deviceTypeId,
                DeviceId = deviceId,
                Tag = measure,
                Measure = count
            };

            await InsertEventAsync(logRecord);
        }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
            log.HostId = HostId;
            log.InstanceId = InstanceId;
            log.Version = Version;
        }
    }
}
