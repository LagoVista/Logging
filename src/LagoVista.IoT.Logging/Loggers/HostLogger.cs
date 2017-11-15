using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class HostLogger : LoggerBase, IHostLogger
    {
        public HostLogger(string hostId, ILogWriter writer) : base(writer)
        {
            HostId = hostId;
        }

        public string InstrumentationKey => throw new NotImplementedException();

        public string InstanceInstrumentationKey => throw new NotImplementedException();

        public string DeviceInstrumentationKey => throw new NotImplementedException();

        public string HostId { get; private set; }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
            log.HostId = HostId;
        }

        public async void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                LogLevel = "Error",
                ErrorCode = errorCode.Code,
                Message = errorCode.Message,
            };

            logRecord.AddKVPs(args);

            await base.InsertErrorAsync(logRecord);
        }
    }
}
