using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class HostLogger : LoggerBase, IHostLogger
    {
        public HostLogger(string hostId, string version, ILogWriter writer) : base(writer)
        {
            HostId = hostId;
            Version = version;
        }

        public string InstrumentationKey => throw new NotImplementedException();

        public string InstanceInstrumentationKey => throw new NotImplementedException();

        public string DeviceInstrumentationKey => throw new NotImplementedException();

        public string HostId { get; private set; }

        public string Version { get; set; }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
            log.HostId = HostId;
            log.Version = Version;
        }

        public async void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                Id = Guid.NewGuid().ToString(),
                TimeStamp = DateTime.UtcNow,
                LogLevel = "Error",
                ErrorCode = errorCode.Code,
                Message = errorCode.Message,
                HostId = HostId,
                Version = Version,
            };

            logRecord.AddKVPs(args);

            await base.InsertErrorAsync(logRecord);
        }
    }
}
