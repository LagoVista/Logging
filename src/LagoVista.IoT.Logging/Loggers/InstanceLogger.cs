// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 7dc2c254243f7d0af62c84783a959c086d25672daccee6925e45e969d12b52cf
// IndexVersion: 0
// --- END CODE INDEX META ---
using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class InstanceLogger : LoggerBase, IInstanceLogger
    {
        public InstanceLogger(ILogWriter writer, string hostId, string version, string instanceId) : base(writer)
        {
            HostId = hostId;
            InstanceId = instanceId;
            Version = version;
        }

        public string DeviceInstrumentationKey => throw new NotImplementedException();

        public string HostId { get; private set; }

        public string Version { get; private set; }

        public string InstanceId { get; private set; }

        public void InitAsync(string hostId, string instanceId)
        {
            HostId = hostId;
            InstanceId = instanceId;
        }

        public async void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                ErrorCode = errorCode.Code,
                Message = errorCode.Message,
            };

            logRecord.AddKVPs(args);

            await InsertErrorAsync(logRecord);
        }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
            log.HostId = HostId;
            log.InstanceId = InstanceId;
            log.Version = Version;
        }
    }
}
