using LagoVista.IoT.Logging.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class InstanceLogger : LoggerBase, IInstanceLogger
    {
        public InstanceLogger(ILogWriter writer, string hostId, string instanceId) : base(writer)
        {
            HostId = hostId;
            InstanceId = instanceId;
        }

        public string DeviceInstrumentationKey => throw new NotImplementedException();

        public string HostId { get; private set; }

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

            await InsertEventAsync(logRecord);
        }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
            log.HostId = HostId;
            log.InstanceId = InstanceId;
        }
    }
}
